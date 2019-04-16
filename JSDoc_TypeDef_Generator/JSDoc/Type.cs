using System;
using System.Collections.Generic;
using System.Text;

namespace JSDoc_TypeDef_Generator.JSDoc
{
    class Type
    {
        public static string Boolean { get => "boolean"; }
        public static string Number { get => "number"; }
        public static string String { get => "string"; }

        public static string TranslateType(Type t) {
            if (t.Equals(typeof(string))) return String;
            if (t.Equals(typeof(int))) return Number;
            if (t.Equals(typeof(double))) return Number;
            if (t.Equals(typeof(float))) return Number;
            if (t.Equals(typeof(decimal))) return Number;
            if (t.Equals(typeof(bool))) return Boolean;
            return null;
        }
    }
}
