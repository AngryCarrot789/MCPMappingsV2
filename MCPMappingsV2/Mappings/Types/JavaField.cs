namespace MCPMappingsV2.Mappings.Types {
    public class JavaField {
        public string SeargeName { get; }
        public string MCPName { get; }

        public JavaField(string srg, string mcp) {
            this.SeargeName = srg;
            this.MCPName = mcp;
        }
    }
}
