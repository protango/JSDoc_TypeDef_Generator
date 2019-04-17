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
                if (t.IsArray) { return parseArr((object[])obj); }
                TypeDef current = new TypeDef(defaultDescription + result.Count);
                var props = t.GetProperties();
                foreach (var prop in props) {
                    JSDType jsdt;
                    if (prop.PropertyType.IsClass)
                        jsdt = parseObj(prop.GetValue(obj));
                    else if (prop.PropertyType.IsArray)
                        jsdt = parseArr((object[])prop.GetValue(obj));
                    else
                        jsdt = JSDType.TranslatePrimitive(prop.PropertyType);

                    current.Properties.Add(prop.Name, jsdt);
                }

                result.Add(current);
                return current.JSDType;
            }
            JSDType parseArr(object[] arr) {
                if (arr.Length == 0) return new JSDType() { IsArray = true };
                Type[] ts = arr.Select(x => x.GetType()).ToArray();
                int classCount = ts.Count(x => x.IsClass);

                List<string> arrTypes = new List<string>();
                foreach (var elem in arr) {
                    Type ctype = elem.GetType();
                    if ()
                    JSDType tName = JSDType.TranslatePrimitive(ctype);
                    if (tName != null) {
                        if (!arrTypes.Contains(tName))
                            arrTypes.Add(tName);
                    } else {
                        TypeDef cc = parseObj(elem);
                    }
                }

                if (classCount == 0) {
                    //primitive array
                    var tsd = ts.Distinct();
                    string tName = string.Join("|", tsd.Select(x => JSDType.TranslatePrimitive(x)));
                    if (tsd.Count() > 1) tName = $"({tName})";
                    return new TypeDef(tName);
                } else if (classCount == arr.Length) {
                    //object array

                } else {
                    //mixed primitive/object array
                    return new TypeDef("");
                }
                TypeDef current;

            }
            TypeDef squashTypes(TypeDef t) {
                foreach (TypeDef td in result) {
                }
                this.All(x => other.Any(y => y.Equivalent(x)));
            } 
            object JSON = JsonConvert.DeserializeObject(str);
            parseObj(JSON);
            return result.ToArray();
        }
    }
}
