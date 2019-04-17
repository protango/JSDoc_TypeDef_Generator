using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pluralize.NET;

namespace JSDoc_TypeDef_Generator.JSDoc { 
    class TypeDef {
        private const string defaultDescription = "Enter Description Here";
        private const string defaultName = "MyType";

        public string Name { get; set; }
        public string Description { get; set; }
        public PropertyList Properties { get; set; }
        public JSDType JSDType { get => new JSDType(Name); }

        public TypeDef(string Name = defaultName, string Description = defaultDescription) {
            this.Name = Name;
            this.Description = Description;
            Properties = new PropertyList();
        }

        public override string ToString() {
            string result = $"{Description}\n@typedef {{Object}} {Name}";
            string props = Properties.ToString();
            if (props != "") result += "\n" + props;
            return result;
        }
    }
}
