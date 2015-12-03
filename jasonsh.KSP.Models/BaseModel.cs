using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jasonsh.KSP.Models
{
    public class BaseModel
    {
        public string Original { get; private set; }

        protected BaseModel(string original)
        {
            this.Original = original;
        }

        public override string ToString()
        {
            return this.Original;
        }
    }
}
