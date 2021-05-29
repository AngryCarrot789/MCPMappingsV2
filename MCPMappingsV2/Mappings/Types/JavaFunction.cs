using System.Linq;
using MCPMappingsV2.Mappings.Controls;

namespace MCPMappingsV2.Mappings.Types {
    public class JavaFunction {
        private static int NextID;

        public JavaClass Class { get; }
        public string ObfuscatedName { get; }
        public string SeargeName { get; }
        public string MCPName { get; }
        public string[] Parameters { get; }

        public int ID { get; }

        public JavaFunction(JavaClass clazz, string obf, string srg, string mcp, string[] functionParamTypes) {
            this.ID = NextID++;
            this.Class = clazz;
            this.ObfuscatedName = obf ?? "-";
            this.SeargeName = srg ?? "-";
            this.MCPName = mcp ?? "-";
            this.Parameters = functionParamTypes;
        }

        public FunctionMappingViewModel CreateViewModel() {
            return new FunctionMappingViewModel(this.ObfuscatedName, this.SeargeName, this.MCPName, this.Parameters);
        }

        public override bool Equals(object obj) {
            if (obj is JavaFunction func) {
                return func.Class.Equals(this.Class) &&
                    func.ObfuscatedName.Equals(this.ObfuscatedName) &&
                    func.SeargeName.Equals(this.SeargeName) &&
                    func.MCPName.Equals(this.MCPName) &&
                    func.Parameters.Equals(this.Parameters);
            }

            return false;
        }

        public override int GetHashCode() {
            return this.ID;
        }
    }
}
