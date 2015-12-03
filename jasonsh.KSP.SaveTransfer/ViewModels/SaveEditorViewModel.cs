using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace jasonsh.KSP.SaveTransfer.ViewModels
{
    public class SaveEditorViewModel : ViewModelBase
    {
        private OpenFileDialog _openFileDialog = null;
        protected OpenFileDialog OpenFileDialog { get { return _openFileDialog ?? (_openFileDialog = new OpenFileDialog() { DefaultExt = ".sfs", Filter = "Kerbal Save File (*.sfs)|*.sfs" }); } }

        private string _fullPath = null;
        public string FullPath
        {
            get { return _fullPath; }
            private set { _fullPath = value; this.RaisePropertyChanged(); this.RaisePropertyChanged(nameof(Filename)); }
        }
        public string Filename { get { return Path.GetFileName(this.FullPath ?? ""); } }

        private ICommand _openFile = null;
        public ICommand OpenFile { get { return _openFile ?? (_openFile = new RelayCommand(OnOpenFile)); } }

        private Models.ComplexObject _game = null;
        public Models.ComplexObject Game
        {
            get { return _game; }
            set { _game = value; this.RaisePropertyChanged(); this.RaisePropertyChanged(nameof(Vessels)); }
        }

        public IEnumerable<VesselViewModel> Vessels
        {
            get
            {
                if (this.Game == null) return Enumerable.Empty<VesselViewModel>();

                return this.Game.Children
                    .OfType<Models.ComplexObject>()
                    .Where(p => p.Name.ToLower() == "flightstate")
                    .SelectMany(p => p.Children)
                    .OfType<Models.ComplexObject>()
                    .Where(p => p.Name.ToLower() == "vessel")
                    .Select(p => new VesselViewModel(p));
            }
        }

        public SaveEditorViewModel()
        {
            if (IsInDesignMode)
            {
                this.FullPath = @"C:\foo\bar\baz.sfs";
                this.Game = new Models.ComplexObject("GAME",
                    new Models.ComplexObject("FLIGHTSTATE",
                        new Models.ComplexObject("VESSEL",
                            new Models.Literal("name", "Vessel 1")),
                        new Models.ComplexObject("VESSEL",
                            new Models.Literal("name", "Vessel 2"))));
            }
            else
            {
            }
        }

        protected virtual void OnOpenFile()
        {
            try
            {
                if (this.OpenFileDialog.ShowDialog().GetValueOrDefault(false))
                {
                    this.FullPath = this.OpenFileDialog.FileName;

                    this.ParseFile(this.FullPath);
                }
            }
            catch
            {
                this.Clear();
                MessageBox.Show($"Error reading file: {this.Filename}");
            }
        }

        protected virtual void ParseFile(string fullPath)
        {
            var content = File.ReadAllText(fullPath);

            var game = Parsers.Parser.ParseModel<Models.ComplexObject>(content);
            if (game == null) throw new ArgumentException($"Could not parse file: {fullPath}", nameof(fullPath));

            this.Game = game;
        }

        protected virtual void Clear()
        {
            this.FullPath = null;
            this.Game = null;
        }
    }
}