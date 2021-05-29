using System.Collections.Generic;

namespace MCPMappingsV2.Mappings.Data.Parser {
    public struct SRGMethod {
        public SRGClass Class;
        public string ObfuscatedName;
        public string SeargeName;
        public List<ISRGObject> Parameters;
        public ISRGObject ReturnType;

        public SRGMethod(SRGClass clazz, string obf, string srg, List<ISRGObject> parameters, ISRGObject returnType) {
            this.Class = clazz;
            this.ObfuscatedName = obf;
            this.SeargeName = srg;
            this.Parameters = parameters;
            this.ReturnType = returnType;
        }

        public override int GetHashCode() {
            int hash = 17;
            hash = hash * 31 + this.Class.GetHashCode();
            hash = hash * 31 + this.ObfuscatedName.GetHashCode();
            return hash;
        }

        public override bool Equals(object obj) {
            if (obj is SRGField field) {
                return field.Class.Equals(this.Class) && field.ObfuscatedName == this.ObfuscatedName && field.SeargeName == this.SeargeName;
            }

            return false;
        }
    }
}
