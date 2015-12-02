using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System.IO;
using System.Windows.Input;

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
            private set { _fullPath = value; this.RaisePropertyChanged(); this.RaisePropertyChanged(() => this.Filename); }
        }
        public string Filename { get { return Path.GetFileName(this.FullPath ?? ""); } }

        private ICommand _openFile = null;
        public ICommand OpenFile { get { return _openFile ?? (_openFile = new RelayCommand(OnOpenFile)); } }

        public SaveEditorViewModel()
        {
            if (IsInDesignMode)
            {
                this.FullPath = @"C:\foo\bar\baz.sfs";
            }
            else
            {
            }
        }

        protected virtual void OnOpenFile()
        {
            if (this.OpenFileDialog.ShowDialog().GetValueOrDefault(false))
            {
                this.FullPath = this.OpenFileDialog.FileName;

                this.ParseFile(this.FullPath);
            }
        }

        protected virtual void ParseFile(string filename)
        {

        }
    }
}