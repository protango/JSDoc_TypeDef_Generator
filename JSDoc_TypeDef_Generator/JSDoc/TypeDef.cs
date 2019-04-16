using System;
using System.Collections.Generic;
using System.Text;

namespace JSDoc_TypeDef_Generator.JSDoc
{
    class TypeDef
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public PropertyList Properties = new PropertyList();

        private Dictionary<string, string> properties = new Dictionary<string, string>();

        public TypeDef(string Name, string Description)
        {
            this.Name = Name;
            this.Description = Description;
        }

        public override string ToString()
        {
            string result = $"{Description}\n@typedef {{Object}} {Name}";
            string props = Properties.ToString();
            if (props != "") result += "\n" + props;
            return result;
        }
    }
}
