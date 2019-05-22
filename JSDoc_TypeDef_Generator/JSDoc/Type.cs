using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace JSDoc_TypeDef_Generator.JSDoc
{
    class JSDType {
        private const string any = "any";

        public string[] Types { get; }
        public NullableStatus Nullable { get; set; }
        public bool IsArray { get; set; }
        public bool IsAny { get => Types.Length == 1 && Types[0] == any && !IsArray && Nullable == NullableStatus.Unspecified; }
        public bool IsAnyArray { get => Types.Length == 1 && Types[0] == any && IsArray && Nullable == NullableStatus.Unspecified; }

        public override string ToString() {
            string tt = string.Join("|", Types);
            if (Types.Length > 1) tt = $"({tt})";
            if (Nullable == NullableStatus.NonNullable) tt = "!" + tt;
            else if (Nullable == NullableStatus.Nullable) tt = "?" + tt;
            if (IsArray) tt += "[]";
            return $"{{{tt}}}";
        }

        public override bool Equals(object obj) {
            if (!(obj is JSDType)) return false;
            JSDType tobj = (JSDType)obj;
            return Types.OrderBy(x => x).SequenceEqual(tobj.Types.OrderBy(x => x)) &&
                   Nullable == tobj.Nullable &&
                   IsArray == tobj.IsArray;
        }

        public static JSDType Parse(string s) {
            /*s = s.ToLower();
            if (s.EndsWith("[]"))
            if (s == "string") return String;
            if (s == "number") return Number;
            if (s == "boolean") return Boolean;
            if (s == "date") return Date;
            if (s == "any") return Any;*/
            return null;
        }

        public override int GetHashCode() {
            string s = string.Join("", Types.OrderBy(x => x));
            s += (int)Nullable;
            s += IsArray?"A":"X";
            return s.GetHashCode();
        }

        public JSDType(params string[] Types) {
            Nullable = NullableStatus.Unspecified;
            this.Types = Types;
        }

        public static JSDType Date { get => new JSDType("Date"); }
        public static JSDType Boolean { get => new JSDType("boolean"); }
        public static JSDType Number { get => new JSDType("number"); }
        public static JSDType String { get => new JSDType("string"); }
        public static JSDType Any { get => new JSDType(any); }
        public static JSDType AnyArray { get => new JSDType(any) { IsArray = true }; }

        public static JSDType TranslatePrimitive(Type t) {
            if (t == null) return Any;
            if (t.Equals(typeof(string))) return String;
            if (t.Equals(typeof(int))) return Number;
            if (t.Equals(typeof(double))) return Number;
            if (t.Equals(typeof(float))) return Number;
            if (t.Equals(typeof(decimal))) return Number;
            if (t.Equals(typeof(long))) return Number;
            if (t.Equals(typeof(bool))) return Boolean;
            if (t.Equals(typeof(DateTime))) return Date;
            return Any;
        }

        /// <summary>
        /// Tries to merge two JSDTypes together.
        /// </summary>
        /// <param name="t1">A JSDType to merge</param>
        /// <param name="t2">A JSDType to merge</param>
        /// <param name="result">Output parameter for merged JSDType, if merge was not successful this is set to null</param>
        /// <returns>A boolean value indicating whether the merge was successful or not</returns>
        public static bool TryMerge(JSDType t1, JSDType t2, out JSDType result) {
            if (t1.IsAny) { result = t2; return true; }
            if (t2.IsAny) { result = t1; return true; }
            if (t1.IsArray != t2.IsArray) { result = null; return false; }
            string[] rTypes = t1.Types.Concat(t2.Types).Distinct().Where(x => x != any).ToArray();
            result = rTypes.Length > 0 ? new JSDType(rTypes) : Any;
            result.IsArray = t1.IsArray;
            if (t1.Nullable == NullableStatus.Unspecified) result.Nullable = t2.Nullable;
            else if (t2.Nullable == NullableStatus.Unspecified) result.Nullable = t1.Nullable;
            else if (t1.Nullable == t2.Nullable) result.Nullable = t1.Nullable;
            else { result = null; return false; }
            return true;
        }

        public enum NullableStatus {
            Unspecified,
            Nullable,
            NonNullable
        }
    }
}
