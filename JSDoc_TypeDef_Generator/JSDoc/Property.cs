using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace JSDoc_TypeDef_Generator.JSDoc
{
    class PropertyList : List<Property>
    {
        public void Add(string Name, string Type, string Description = "") {
            Add(new Property(Name, Type, Description));
        }
        public void Remove(string Name) {
            RemoveAll(x => x.Name == Name);
        }
        public override string ToString() {
            return string.Join('\n', this.Select(x => x.ToString()));
        }
    }
    class Property
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public Property(string Name, string Type, string Description = "") {
            this.Name = Name;
            this.Type = Type;
            this.Description = Description;
        }
        public override string ToString() {
            return $"@property {{{Type}}} {Name} {Description}";
        }
    }
}
