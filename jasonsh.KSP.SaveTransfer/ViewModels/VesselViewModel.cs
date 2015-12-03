using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jasonsh.KSP.SaveTransfer.ViewModels
{
    public class VesselViewModel : ViewModelBase
    {
        public Models.ComplexObject Model { get; private set; }
        public string Name
        {
            get
            {
                return this.Model.Children
                    .OfType<Models.Literal>()
                    .Where(p => p.Name.ToLower() == "name")
                    .Select(p => p.Value)
                    .FirstOrDefault();
            }
        }

        public VesselViewModel(Models.ComplexObject model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            this.Model = model;
        }
    }
}
