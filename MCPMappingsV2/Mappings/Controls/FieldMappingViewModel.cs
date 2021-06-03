namespace MCPMappingsV2.Mappings.Controls {
    public class FieldMappingViewModel : MappingViewModel {
        public FieldMappingViewModel(string obfuscated, string searge, string mcp) {
            this.ObfName = obfuscated;
            this.SRGName = searge;
            this.MCPName = mcp;
            this.Parameters = "()";
        }
    }
}
