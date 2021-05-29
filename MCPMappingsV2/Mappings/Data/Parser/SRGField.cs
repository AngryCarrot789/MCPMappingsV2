namespace MCPMappingsV2.Mappings.Data.Parser {
    public struct SRGField {
        public SRGClass Class;
        public string ObfuscatedName;
        public string SeargeName;
        public int Side;

        public SRGField(SRGClass clazz, string obfuscatedName, string seargeName, int side) {
            this.Class = clazz;
            this.ObfuscatedName = obfuscatedName;
            this.SeargeName = seargeName;
            this.Side = side;
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
