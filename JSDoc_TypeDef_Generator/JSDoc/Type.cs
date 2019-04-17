using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace JSDoc_TypeDef_Generator.JSDoc
{
    class JSDType
    {
        public string[] Types { get; }
        public NullableStatus Nullable { get; set; }
        public bool IsArray { get; set; }
        public bool IsAny { get => Types.Length == 1 && Types[0] == "any"; }

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
        public static JSDType Any { get => new JSDType("any"); }

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


        public enum NullableStatus {
            Unspecified,
            Nullable,
            NonNullable
        }
    }
}
