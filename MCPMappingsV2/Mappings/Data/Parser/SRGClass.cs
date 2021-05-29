namespace MCPMappingsV2.Mappings.Data.Parser {
    public struct SRGClass : ISRGObject {
        public string ObfuscatedName;
        public string Name { get; }
        public string PackageOnly;
        public string FullName;
        public int Side;

        public SRGClass(string obfuscated, string fullName, int side) {
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
