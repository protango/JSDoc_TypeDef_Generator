using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pluralize.NET.Core;

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

        public static TypeDef[] ParseJSON(string str) {
            List<TypeDef> result = new List<TypeDef>();
            JSDType parseObj(JToken obj, string propName = null) {
                if (obj is JObject) {
                    TypeDef current = new TypeDef();
                    JObject jobj = (JObject)obj;
                    var props = jobj.Properties();
                    foreach (var prop in props) {
                        JSDType jsdt = parseObj(prop.Value, prop.Name);
                        current.Properties.Add(prop.Name, jsdt);
                    }

                    return squashTypes(current, propName).JSDType;
                } else if (obj is JArray)
                    return parseArr((JArray)obj, propName);
                else if (obj is JValue)
                    return JSDType.TranslatePrimitive(((JValue)obj).Value?.GetType());
                throw new Exception("obj must be either a JObject, JArray, or JValue");
            }
            JSDType parseArr(JArray arr, string propName = null) {
                if (arr.Count == 0) return new JSDType() { IsArray = true };
                Type[] ts = arr.Select(x => x.GetType()).ToArray();
                int classCount = ts.Count(x => x.IsClass);

                List<JSDType> arrTypes = new List<JSDType>();
                foreach (var elem in arr) {
                    arrTypes.Add(parseObj(elem, new Pluralizer().Singularize(propName)));
                }
                return new JSDType(arrTypes.SelectMany(x => x.Types).Distinct().ToArray()) { IsArray = true };
            }
            TypeDef squashTypes(TypeDef t, string propName = null) {
                foreach (TypeDef td in result) {
                    if (td.Properties.TrySquash(t.Properties)) {
                        return td;
                    }
                }
                propName = propName ?? defaultName;
                if (result.Any(x => x.Name == propName)) { 
                    int i = 1;
                    while (result.Any(x => x.Name == propName+"_"+i)) i++;
                    propName += "_" + i;
                }
                t.Name = propName;
                result.Add(t);
                return t;
            } 
            JToken JSON = (JToken)JsonConvert.DeserializeObject(str);
            JSDType rootType = parseObj(JSON);

            TypeDef rootTypeDef = result.First(x => x.JSDType.Equals(rootType));
            result.Remove(rootTypeDef);
            result.Add(rootTypeDef);
            result.Reverse();// move root typedef to the front of list

            return result.ToArray();
        }
    }
}
