using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public static TypeDef[] Parse(string str) {
            List<TypeDef> result = new List<TypeDef>();
            JSDType parseObj(object obj) {
                Type t = obj.GetType();
                if (t.IsArray) return parseArr((object[])obj);
                if (!t.IsClass) return JSDType.TranslatePrimitive(t);

                TypeDef current = new TypeDef(defaultDescription + result.Count);
                var props = t.GetProperties();
                foreach (var prop in props) {
                    JSDType jsdt = parseObj(prop.GetValue(obj));
                    current.Properties.Add(prop.Name, jsdt);
                }
                
                return squashTypes(current).JSDType;
            }
            JSDType parseArr(object[] arr) {
                if (arr.Length == 0) return new JSDType() { IsArray = true };
                Type[] ts = arr.Select(x => x.GetType()).ToArray();
                int classCount = ts.Count(x => x.IsClass);

                List<JSDType> arrTypes = new List<JSDType>();
                foreach (var elem in arr) {
                    arrTypes.Add(parseObj(elem));
                }
                return new JSDType(arrTypes.SelectMany(x => x.Types).Distinct().ToArray());
            }
            TypeDef squashTypes(TypeDef t) {
                foreach (TypeDef td in result) {
                    if (t.Properties.All(x => td.Properties.Contains(x))) {
                        // equivalent type already exists
                        return td;
                    } else if (td.Properties.All(x => t.Properties.Contains(x))) {
                        // squash this type into existing type
                        td.Properties = t.Properties;
                        return td;
                    }
                }
                result.Add(t);
                return t;
            } 
            object JSON = JsonConvert.DeserializeObject(str);
            JSDType rootType = parseObj(JSON);

            TypeDef rootTypeDef = result.First(x => x.JSDType == rootType);
            result.Remove(rootTypeDef);
            result.Add(rootTypeDef);
            result.Reverse();// move root typedef to the front of list

            return result.ToArray();
        }
    }
}
