using REghZyFramework.Utilities;

namespace MCPMappingsV2.Mappings.Controls {
    public class MappingViewModel : BaseViewModel {
        public string MCPName { get; protected set; }
        public string SRGName { get; protected set; }
        public string ObfName { get; protected set; }
        public string Parameters { get; protected set; }

        public MappingViewModel() {
            this.MCPName = "";
            this.SRGName = "";
            this.ObfName = "";
            this.Parameters = "";
        }
    }
}
