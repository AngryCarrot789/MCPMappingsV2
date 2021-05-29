using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCPMappingsV2.Mappings.Controls;
using REghZyFramework.Utilities;

namespace MCPMappingsV2.Windows {
    public class MainViewModel : BaseViewModel {
        public ObservableCollection<MappingViewModel> Mappings { get; }

        public MainViewModel() {
            this.Mappings = new ObservableCollection<MappingViewModel>();

            this.Mappings.Add(new ClassMappingViewModel());
            this.Mappings.Add(new FunctionMappingViewModel());
        }
    }
}
