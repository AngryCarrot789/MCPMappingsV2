namespace MCPMappingsV2.Mappings.Data.Parser {
    public struct SRGPackage {
        public string Name;
        public int Side;

        public SRGPackage(string name, int side) {
            this.Name = name;
            this.Side = side;
        }

        public override int GetHashCode() {
            return this.Name.GetHashCode();
        }

        public override bool Equals(object obj) {
            if (obj is SRGPackage srg) {
                return srg.Name.Equals(this.Name) && srg.Side == this.Side;
            }

            return false;
        }
    }
}
