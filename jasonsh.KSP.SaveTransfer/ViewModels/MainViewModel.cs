using GalaSoft.MvvmLight;

namespace jasonsh.KSP.SaveTransfer.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private SaveEditorViewModel _source;
        public SaveEditorViewModel Source { get { return _source ?? (_source = new SaveEditorViewModel()); } }

        private SaveEditorViewModel _destination;
        public SaveEditorViewModel Destination { get { return _destination ?? (_destination = new SaveEditorViewModel()); } }

        public MainViewModel()
        {
            if (IsInDesignMode)
            {
            }
            else
            {
            }
        }
    }
}