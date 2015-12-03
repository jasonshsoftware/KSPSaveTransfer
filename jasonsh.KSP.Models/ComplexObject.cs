using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jasonsh.KSP.Models
{
    public class ComplexObject : BaseModel
    {
        public string Name { get; private set; }

        public ComplexObject(string original, string name)
            : base(original)
        {
            this.Name = name;
        }
    }
}
