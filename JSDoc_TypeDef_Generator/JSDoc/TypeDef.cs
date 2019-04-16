using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSDoc_TypeDef_Generator.JSDoc { 
    class TypeDef {
        public string Name { get; set; }
        public string Description { get; set; }
        public PropertyList Properties { get; set; }

        public TypeDef(string Name, string Description) {
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

        public static TypeDef[] Parse(string str) {
            List<TypeDef> result = new List<TypeDef>();
            object JSON = JsonConvert.DeserializeObject(str);

            TypeDef parseObj(object obj) {
            }
            TypeDef parseArr(object[] arr) {

            }
        }
        
    }
}
