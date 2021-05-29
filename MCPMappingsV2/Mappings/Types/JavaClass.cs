namespace MCPMappingsV2.Mappings.Types {
    public class JavaClass {
        private static int NextID;

        public string ObfuscatedName { get; }
        public string ReadableName { get; }
        public int ID { get; }

        public JavaClass(string obf, string readable) {
            this.ID = NextID++;
            this.ObfuscatedName = obf;
            this.ReadableName = readable;
        }

        public override bool Equals(object obj) {
            if (obj is JavaClass clazz) {
                return clazz.ID == this.ID && clazz.ObfuscatedName.Equals(this.ObfuscatedName) && clazz.ReadableName.Equals(this.ReadableName);
            }

            return false;
        }

        public override int GetHashCode() {
            return this.ID;
        }
    }
}
