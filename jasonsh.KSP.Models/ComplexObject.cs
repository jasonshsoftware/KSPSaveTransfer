using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jasonsh.KSP.Models
{
    public sealed class ComplexObject : BaseModel
    {
        public string Name { get; private set; }

        private IEnumerable<BaseModel> _children = null;
        public IEnumerable<BaseModel> Children { get { return _children ?? Enumerable.Empty<BaseModel>(); } private set { _children = value; } }

        public ComplexObject(string name, params BaseModel[] children) : this(name, children.AsEnumerable()) { }
        public ComplexObject(string name, IEnumerable<BaseModel> children)
            : base()
        {
            this.Name = name;
            this.Children = children;
        }
    }
}
