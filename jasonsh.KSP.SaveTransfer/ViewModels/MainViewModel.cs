using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace jasonsh.KSP.SaveTransfer.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public const string BackupFolderName = "Backups";

        private SaveEditorViewModel _source;
        public SaveEditorViewModel Source { get { return _source ?? (_source = new SaveEditorViewModel()); } }

        private SaveEditorViewModel _destination;
        public SaveEditorViewModel Destination { get { return _destination ?? (_destination = new SaveEditorViewModel()); } }

        public ICommand _transferVessel = null;
        public ICommand TransferVessel { get { return _transferVessel ?? (_transferVessel = new RelayCommand(OnTransferVessel, CanTransferVessel)); } }

        public MainViewModel()
        {
            if (IsInDesignMode)
            {
            }
            else
            {
            }

            this.Source.PropertyChanged += (s, e) => { if (e.PropertyName == nameof(this.Source.SelectedVessel)) ((RelayCommand)this.TransferVessel).RaiseCanExecuteChanged(); };
            this.Destination.PropertyChanged += (s, e) => { if (e.PropertyName == nameof(this.Destination.SelectedVessel)) ((RelayCommand)this.TransferVessel).RaiseCanExecuteChanged(); };
        }

        protected virtual bool CanTransferVessel()
        {
            return this.Source.SelectedVessel != null
                && this.Destination.SelectedVessel != null;
        }
        protected virtual void OnTransferVessel()
        {
            try
            {
                // Gather source parts to transfer.
                //
                var sourceParts = this.Source.SelectedVessel.Model.Children
                    .OfType<Models.ComplexObject>()
                    .Where(p => p.Name.ToLower() == "part");

                // Gather destination to remove and the starting index.
                //
                var destinationParts = this.Destination.SelectedVessel.Model.Children
                    .Select((p, i) => new { Index = i, Model = p })
                    .Where(p => p.Model is Models.ComplexObject)
                    .Where(p => ((Models.ComplexObject)p.Model).Name.ToLower() == "part");
                var destinationIndex = destinationParts
                    .Select(p => p.Index)
                    .First();

                // Remove destination parts.
                //
                destinationParts
                    .Select(p => p.Model)
                    .ToList().ForEach(p => this.Destination.SelectedVessel.Model.Children.Remove(p));

                // Add source parts
                //
                sourceParts
                    .ToList().ForEach(p => this.Destination.SelectedVessel.Model.Children.Insert(destinationIndex++, p));

                // Backup original file.
                //
                var date = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
                var backupFilename = Path.Combine(Path.GetDirectoryName(this.Destination.FullPath), BackupFolderName, $"{date}.sfs");
                if (!Directory.Exists(Path.GetDirectoryName(backupFilename)))
                    Directory.CreateDirectory(Path.GetDirectoryName(backupFilename));
                File.Copy(this.Destination.FullPath, backupFilename, true);

                try
                {
                    File.WriteAllText(this.Destination.FullPath, this.Destination.Game.ToString());

                    MessageBox.Show("Fly Safe!", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    File.Copy(backupFilename, this.Destination.FullPath, true);
                    File.Delete(backupFilename);

                    throw;
                }
            }
            catch
            {
                MessageBox.Show("An error occurred while transferring vessel.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}