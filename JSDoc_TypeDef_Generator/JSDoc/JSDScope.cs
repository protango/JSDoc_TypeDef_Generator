using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pluralize.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JSDoc_TypeDef_Generator.JSDoc
{
    class JSDScope
    {
        public List<TypeDef> TypeDefinitions { get; set; }

        public JSDScope() {
            TypeDefinitions = new List<TypeDef>();
        }

        public static JSDScope Parse(string input) {
            input = input.Replace("\r\n", "\n"); //Convert to UNIX string
            Regex rx = new Regex(@"\/\*\*(?:([^@\n]*(?:\s*\*\s*(?!\s*@[a-zA-Z]+)[^\r\n]*)*)\s*\*\s*|\s+)@typedef\s+{([^}]+)}\s+(\w+)(.*?)\*\/", RegexOptions.Singleline | RegexOptions.Compiled);
            JSDScope result = new JSDScope();
            MatchCollection matches = rx.Matches(input);
            foreach (Match match in matches) {
                string description = match.Captures[0].Value.Trim();
                string name = match.Captures[2].Value.Trim();
                description = new Regex(@"^\s*\*[ \t\f]*").Replace(description, "");

                TypeDef td = new TypeDef(name, description);

                foreach (Match PropMatch in new Regex(@"@property\s+{([^}]+)}\s+([^\s]+)$").Matches(match.Captures[3].Value)) {
                    td.Properties.Add(PropMatch.Captures[1].Value.Trim(), JSDType.Parse(PropMatch.Captures[0].Value));
                }
                result.TypeDefinitions.Add(td);
            }
            return result;
        }

        public static JSDScope GenerateFrom(JToken obj, JSONParseOptions opts = null) {
            JSDScope scope = new JSDScope();
            JSONParseOptions optsval = opts ?? new JSONParseOptions();
            JSDType rootType = scope.ObjSpec(obj, null, optsval);

            TypeDef rootTypeDef = scope.TypeDefinitions.First(x => x.JSDType.Equals(rootType));
            scope.TypeDefinitions.Remove(rootTypeDef);
            scope.TypeDefinitions.Add(rootTypeDef);
            scope.TypeDefinitions.Reverse();// move root typedef to the front of list

            return scope;
        }

        public override string ToString() {
            string result = "";
            foreach (var td in TypeDefinitions) result += "/**\n * " + td.ToString().Replace("\n", "\n * ") + "\n */\n";
            return result;
        }

        private JArray FakeArrayConvert(JObject obj) {
            var props = obj.Properties();
            if (!props.Any(x => x.Name == "0")) return null;
            var arrprops = props.Where(x => int.TryParse(x.Name, out _)).OrderBy(x=>x.Name);
            return new JArray(arrprops.Select(x=>x.Value));
        }

        private JSDType ObjSpec(JToken obj, string propName, JSONParseOptions opts) {
            if (obj is JObject) {
                TypeDef current = new TypeDef();
                JObject jobj = (JObject)obj;
                if (opts.DetectFakeArrays) {
                    JArray tryArr = FakeArrayConvert(jobj);
                    if (tryArr != null) return ArrSpec(tryArr, propName, opts);
                }
                var props = jobj.Properties();
                foreach (var prop in props) {
                    JSDType jsdt = ObjSpec(prop.Value, prop.Name, opts);
                    current.Properties.Add(prop.Name, jsdt);
                }

                return SquashType(current, propName, opts).JSDType;
            } else if (obj is JArray)
                return ArrSpec((JArray)obj, propName, opts);
            else if (obj is JValue)
                return JSDType.TranslatePrimitive(((JValue)obj).Value?.GetType());
            throw new Exception("obj must be either a JObject, JArray, or JValue");
        }

        private JSDType ArrSpec(JArray arr, string propName, JSONParseOptions opts) {
            if (arr.Count == 0) return JSDType.AnyArray;
            Type[] ts = arr.Select(x => x.GetType()).ToArray();
            int classCount = ts.Count(x => x.IsClass);

            List<JSDType> arrTypes = new List<JSDType>();
            int processed = 0;
            foreach (var elem in arr) {
                var spec = ObjSpec(elem, new Pluralizer().Singularize(propName), opts);
                if (!spec.IsAny) {
                    arrTypes.Add(spec);
                    processed++;
                    if (processed >= opts.MaxArrayAnalysis) break;
                }
            }
            string[] rawTypes = arrTypes.Where(x => !x.IsAny).SelectMany(x => x.Types).Distinct().ToArray();
            if (rawTypes.Length == 0) return JSDType.AnyArray;
            return new JSDType(rawTypes) { IsArray = true };
        }

        private TypeDef SquashType(TypeDef t, string propName, JSONParseOptions opts) {
            foreach (TypeDef td in TypeDefinitions) {
                if (td.Properties.TrySquash(t.Properties, opts.MaxMultiType)) {
                    return td;
                }
            }
            propName = propName ?? "MyType";
            if (TypeDefinitions.Any(x => x.Name == propName)) {
                int i = 1;
                while (TypeDefinitions.Any(x => x.Name == propName + "_" + i)) i++;
                propName += "_" + i;
            }
            t.Name = propName;
            TypeDefinitions.Add(t);
            return t;
        }
    }
}
