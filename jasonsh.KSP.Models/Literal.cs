using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jasonsh.KSP.Models
{
    public sealed class Literal : BaseModel
    {
        public string Name { get; private set; }
        public string Value { get; private set; }

        public Literal(string name, string value)
            : base()
        {
            this.Name = name;
            this.Value = value;
        }

        public override string ToString()
        {
            return $"{this.Name} = {this.Value}";
        }
    }
}
