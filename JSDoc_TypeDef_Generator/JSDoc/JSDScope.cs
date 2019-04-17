using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pluralize.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSDoc_TypeDef_Generator.JSDoc
{
    class JSDScope
    {
        private List<TypeDef> typeDefinitions = new List<TypeDef>();
        public TypeDef[] TypeDefinitions => typeDefinitions.ToArray();

        public static JSDScope ParseJSONToken(JToken obj) {
            JSDScope scope = new JSDScope();
            JSDType rootType = scope.ObjSpec(obj);

            TypeDef rootTypeDef = scope.typeDefinitions.First(x => x.JSDType.Equals(rootType));
            scope.typeDefinitions.Remove(rootTypeDef);
            scope.typeDefinitions.Add(rootTypeDef);
            scope.typeDefinitions.Reverse();// move root typedef to the front of list

            return scope;
        }

        public override string ToString() {
            string result = "";
            foreach (var td in typeDefinitions) result += "/**\n * " + td.ToString().Replace("\n", "\n * ") + "\n */\n";
            return result;
        }

        private JSDType ObjSpec(JToken obj, string propName = null) {
            if (obj is JObject) {
                TypeDef current = new TypeDef();
                JObject jobj = (JObject)obj;
                var props = jobj.Properties();
                foreach (var prop in props) {
                    JSDType jsdt = ObjSpec(prop.Value, prop.Name);
                    current.Properties.Add(prop.Name, jsdt);
                }

                return SquashType(current, propName).JSDType;
            } else if (obj is JArray)
                return ArrSpec((JArray)obj, propName);
            else if (obj is JValue)
                return JSDType.TranslatePrimitive(((JValue)obj).Value?.GetType());
            throw new Exception("obj must be either a JObject, JArray, or JValue");
        }

        private JSDType ArrSpec(JArray arr, string propName = null) {
            if (arr.Count == 0) return new JSDType() { IsArray = true };
            Type[] ts = arr.Select(x => x.GetType()).ToArray();
            int classCount = ts.Count(x => x.IsClass);

            List<JSDType> arrTypes = new List<JSDType>();
            foreach (var elem in arr) {
                arrTypes.Add(ObjSpec(elem, new Pluralizer().Singularize(propName)));
            }
            return new JSDType(arrTypes.SelectMany(x => x.Types).Distinct().ToArray()) { IsArray = true };
        }

        private TypeDef SquashType(TypeDef t, string propName = null) {
            foreach (TypeDef td in typeDefinitions) {
                if (td.Properties.TrySquash(t.Properties)) {
                    return td;
                }
            }
            propName = propName ?? "MyType";
            if (typeDefinitions.Any(x => x.Name == propName)) {
                int i = 1;
                while (typeDefinitions.Any(x => x.Name == propName + "_" + i)) i++;
                propName += "_" + i;
            }
            t.Name = propName;
            typeDefinitions.Add(t);
            return t;
        }
    }
}
