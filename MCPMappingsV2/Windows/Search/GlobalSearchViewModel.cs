using MCPMappingsV2.Mappings;
using MCPMappingsV2.Mappings.Controls;
using REghZyFramework.Utilities;

namespace MCPMappingsV2.Windows.Search {
    public class GlobalSearchViewModel : BaseViewModel {
        public MainViewModel MainView { get; }

        private SearchType _searchType;
        public SearchType SearchType {
            get => this._searchType;
            set => RaisePropertyChanged(ref this._searchType, value);
        }

        private string _searchValue;
        public string SearchValue {
            get => this._searchValue;
            set => RaisePropertyChanged(ref this._searchValue, value);
        }

        public Command SearchCommand { get; }

        public GlobalSearchViewModel(MainViewModel mainView) {
            this.MainView = mainView;
            this.SearchValue = "";
            this.SearchType = SearchType.MCP;
            this.SearchCommand = new Command(this.Search);
        }

        public void Search() {
            foreach(ClassMappingViewModel clazz in this.MainView.ClassMappings) {
                foreach(FieldMappingViewModel field in clazz.Fields) {
                    if (MatchSearch(field)) {
                        this.MainView.SelectedClass = clazz;
                        this.MainView.SelectedField = field;
                        this.MainView.Scroller.ScrollClassToSelected();
                        this.MainView.Scroller.ScrollFieldToSelected();
                    }
                }
                foreach(MethodMappingViewModel method in clazz.Methods) {
                    if (MatchSearch(method)) {
                        this.MainView.SelectedClass = clazz;
                        this.MainView.SelectedMethod = method;
                        this.MainView.Scroller.ScrollClassToSelected();
                        this.MainView.Scroller.ScrollMethodToSelected();
                    }
                }
            }
        }

        private bool MatchSearch(FieldMappingViewModel field) {
            string toSearch = null;
            switch (this.SearchType) {
                case SearchType.MCP:
                    toSearch = field.MCPName;
                    break;
                case SearchType.Searge:
                    toSearch = field.SRGName;
                    break;
                case SearchType.Obfuscated:
                    toSearch = field.ObfName;
                    break;
            }

            if (toSearch == null) {
                return false;
            }

            string searchValue;
            if (this.MainView.IgnoreCases) {
                searchValue = this.SearchValue.ToLower();
                toSearch = toSearch.ToLower();
            }
            else {
                searchValue = this.SearchValue;
            }

            if (this.MainView.CheckContains) {
                return toSearch.Contains(searchValue);
            }
            else {
                return toSearch.StartsWith(searchValue);
            }
        }

        private bool MatchSearch(MethodMappingViewModel field) {
            string toSearch = null;
            switch (this.SearchType) {
                case SearchType.MCP:
                    toSearch = field.MCPName;
                    break;
                case SearchType.Searge:
                    toSearch = field.SRGName;
                    break;
                case SearchType.Obfuscated:
                    toSearch = field.ObfName;
                    break;
                case SearchType.Parameters:
                    toSearch = field.Parameters;
                    break;
            }

            if (toSearch == null) {
                return false;
            }

            string searchValue;
            if (this.MainView.IgnoreCases) {
                searchValue = this.SearchValue.ToLower();
                toSearch = toSearch.ToLower();
            }
            else {
                searchValue = this.SearchValue;
            }

            if (this.MainView.CheckContains) {
                return toSearch.Contains(searchValue);
            }
            else {
                return toSearch.StartsWith(searchValue);
            }
        }
    }
}
