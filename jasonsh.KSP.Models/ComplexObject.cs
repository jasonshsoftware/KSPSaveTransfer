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
            if (String.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            this.Name = name;
            this.Children = children;
        }

        public override string ToString()
        {
            var children = this.Children
                .Select(p => p.ToString())
                .SelectMany(p => p.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                .Select(p => "\t" + p)
                .Aggregate("", (l, r) => !String.IsNullOrEmpty(l) ? $"{l}\r\n{r}" : r);

            return $@"{this.Name}
{{
{children}
}}";
        }
    }
}
