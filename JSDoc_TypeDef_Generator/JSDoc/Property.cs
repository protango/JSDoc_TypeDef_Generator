using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace JSDoc_TypeDef_Generator.JSDoc
{
    class PropertyList : List<Property>
    {
        public void Add(string Name, JSDType Type, string Description = "") {
            Add(new Property(Name, Type, Description));
        }
        public void Remove(string Name) {
            RemoveAll(x => x.Name == Name);
        }
        public override string ToString() {
            return string.Join('\n', this.Select(x => x.ToString()));
        }

        public bool TrySquash(PropertyList otherpl, int maxMultiType = int.MaxValue) {
            var big = (otherpl.Count > Count ? otherpl : this).ToArray();
            var small = (otherpl.Count > Count ? this : otherpl).ToArray();
            bool success = true;
            foreach (var smallmatch in small) {
                Property bigmatch = big.FirstOrDefault(x => smallmatch.Name == x.Name);
                if (bigmatch == null) { success = false; break; }
                if (!bigmatch.Type.Equals(smallmatch.Type)) {
                    JSDType bmt = bigmatch.Type, smt = smallmatch.Type;
                    if (JSDType.TryMerge(bmt, smt, out JSDType merged) && merged.Types.Length < maxMultiType) { 
                        bigmatch.Type = merged;
                    } else {
                        success = false;
                        break;
                    }
                }
            }
            if (success) {
                Clear();
                AddRange(big);
            }
            return success;
        }
    }
    class Property
    {
        public string Name { get; set; }
        public JSDType Type { get; set; }
        public bool Optional { get; set; }
        public string Description { get; set; }

        public Property(string Name, JSDType Type, string Description = "") {
            this.Name = Name;
            this.Type = Type;
            this.Description = Description;
        }
        public override string ToString() {
            string tName = Name;
            if (Optional) tName = $"[{Name}]";
            return $"@property {Type} {tName} {Description}";
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Property)) return false;
            Property other = (Property)obj;
            return Name == other.Name &&
                   Type.Equals(other.Type) && 
                   Description == other.Description;
        }
        public override int GetHashCode() {
            return (Name + Type.GetHashCode() + Description).GetHashCode();
        }
    }
}
