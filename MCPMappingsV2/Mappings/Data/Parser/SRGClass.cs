using System.Collections.Generic;
using MCPMappingsV2.Utils;

namespace MCPMappingsV2.Mappings.Data.Parser {
    public struct SRGClass : ISRGObject {
        public string ObfuscatedName;
        public string Name { get; }
        public string PackageOnly;
        public string FullName;
        public int Side;

        public Dictionary<string, SRGField> ObfToFields { get; }
        public Dictionary<string, SRGField> SrgToField { get; }
        public HashSetMultiMap<string, SRGMethod> ObfToMethods { get; }
        public HashSetMultiMap<string, SRGMethod> SrgToMethod { get; }

        public SRGClass(string obfuscated, string fullName, int side) {
            this.ObfToFields = new Dictionary<string, SRGField>();
            this.SrgToField = new Dictionary<string, SRGField>();
            this.ObfToMethods = new HashSetMultiMap<string, SRGMethod>();
            this.SrgToMethod = new HashSetMultiMap<string, SRGMethod>();
            this.ObfuscatedName = obfuscated;
            this.FullName = fullName;
            this.Side = side;
            int lastPathIndex = fullName.LastIndexOf("/");
            if (lastPathIndex == -1) {
                this.Name = fullName;
                this.PackageOnly = null;
            }
            else {
                this.PackageOnly = fullName.Substring(0, lastPathIndex);
                this.Name = fullName.Substring(lastPathIndex + 1);
            }
        }

        public void AddField(SRGField field) {
            this.ObfToFields.Add(field.ObfuscatedName, field);
            this.SrgToField.Add(field.SeargeName, field);
        }

        public void AddMethod(SRGMethod method) {
            this.ObfToMethods.Add(method.ObfuscatedName, method);
            this.SrgToMethod.Add(method.SeargeName, method);
        }

        public override int GetHashCode() {
            return this.FullName.GetHashCode();
        }

        public override bool Equals(object obj) {
            if (obj is SRGClass srg) {
                return srg.Name.Equals(this.Name) && srg.PackageOnly.Equals(this.PackageOnly) && srg.FullName.Equals(this.FullName) && srg.Side == this.Side;
            }

            return false;
        }
    }
}
