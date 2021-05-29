using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace MCPMappingsV2.Mappings.Controls {
    public class ClassMappingViewModel : MappingViewModel {
        public ClassMappingViewModel(string className, string obfuscatedName) {
            this.MCPName = className;
            this.ObfName = obfuscatedName;
            this.SRGName = "-";
            this.Parameters = "-";
        }
    }
}
