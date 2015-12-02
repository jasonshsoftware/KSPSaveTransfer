using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace jasonsh.KSP.SaveTransfer.Controls
{
    public class SaveEditor : Control
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel",
            typeof(ViewModels.SaveEditorViewModel),
            typeof(SaveEditor),
            new PropertyMetadata(null));
        public ViewModels.SaveEditorViewModel ViewModel
        {
            get { return (ViewModels.SaveEditorViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
            "Label",
            typeof(string),
            typeof(SaveEditor),
            new PropertyMetadata(null));
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public SaveEditor()
        {
            this.DefaultStyleKey = typeof(SaveEditor);
        }
    }
}
