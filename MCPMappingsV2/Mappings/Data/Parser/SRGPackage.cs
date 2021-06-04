using System;

namespace MCPMappingsV2.Mappings.Data.Parser {
    public struct SRGPackage : IComparable {
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

        public int CompareTo(object obj) {
            if (obj is SRGPackage pkg) {
                if (pkg.Name.Length == 0) {
                    return -1;
                }

                if (this.Name.Length == 0) {
                    return 1;
                }

                char a = pkg.Name[0];
                char b = this.Name[0];
                if (a > b) {
                    return 1;
                }

                if (b < 1) {
                    return -1;
                }

                return 0;
            }

            return 0;
        }
    }
}
