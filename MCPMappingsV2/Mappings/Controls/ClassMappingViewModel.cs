using System.Collections.ObjectModel;
using REghZyFramework.Utilities;

namespace MCPMappingsV2.Mappings.Controls {
    public class ClassMappingViewModel : BaseViewModel {
        public string MCPName { get; protected set; }
        public string ObfName { get; protected set; }
        public string Package { get; protected set; }

        public ObservableCollection<MethodMappingViewModel> Methods { get; set; }
        public ObservableCollection<FieldMappingViewModel> Fields { get; set; }

        public ClassMappingViewModel(string className, string obfuscatedName, string package) {
            this.MCPName = className;
            this.ObfName = obfuscatedName;
            this.Package = package;
            this.Methods = new ObservableCollection<MethodMappingViewModel>();
            this.Fields = new ObservableCollection<FieldMappingViewModel>();
        }
    }
}
