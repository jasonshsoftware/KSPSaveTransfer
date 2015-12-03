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
        public Models.ComplexObject Vessel { get; private set; }
        public string Name
        {
            get
            {
                return this.Vessel.Children
                    .OfType<Models.Literal>()
                    .Where(p => p.Name.ToLower() == "name")
                    .Select(p => p.Value)
                    .FirstOrDefault();
            }
        }

        public VesselViewModel(Models.ComplexObject vessel)
        {
            if (vessel == null) throw new ArgumentNullException(nameof(vessel));

            this.Vessel = vessel;
        }
    }
}
