using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using MCPMappingsV2.Mappings;
using MCPMappingsV2.Mappings.Controls;
using MCPMappingsV2.Mappings.Data.Parser;
using REghZyFramework.Utilities;

namespace MCPMappingsV2.Windows {
    public class MainViewModel : BaseViewModel {
        public ObservableCollection<ClassMappingViewModel> ClassMappings { get; }

        private ClassMappingViewModel _selectedClass;
        public ClassMappingViewModel SelectedClass {
            get => this._selectedClass;
            set => RaisePropertyChanged(ref this._selectedClass, value);
        }

        private FieldMappingViewModel _selectedField;
        public FieldMappingViewModel SelectedField {
            get => this._selectedField;
            set => RaisePropertyChanged(ref this._selectedField, value);
        }

        private MethodMappingViewModel _selectedMethod;
        public MethodMappingViewModel SelectedMethod {
            get => this._selectedMethod;
            set => RaisePropertyChanged(ref this._selectedMethod, value);
        }

        private SearchType _classSearchType;
        public SearchType ClassSearchType {
            get => this._classSearchType;
            set => RaisePropertyChanged(ref this._classSearchType, value);
        }

        private SearchType _fieldSearchType;
        public SearchType FieldSearchType {
            get => this._fieldSearchType;
            set => RaisePropertyChanged(ref this._fieldSearchType, value);
        }

        private SearchType _methodSearchType;
        public SearchType MethodSearchType {
            get => this._methodSearchType;
            set => RaisePropertyChanged(ref this._methodSearchType, value);
        }

        private int _selectedClassIndex;
        public int SelectedClassIndex {
            get => this._selectedClassIndex;
            set => RaisePropertyChanged(ref this._selectedClassIndex, value);
        }

        private int _selectedFieldIndex;
        public int SelectedFieldIndex {
            get => this._selectedFieldIndex;
            set => RaisePropertyChanged(ref this._selectedFieldIndex, value);
        }

        private int _selectedMethodIndex;
        public int SelectedMethodIndex {
            get => this._selectedMethodIndex;
            set => RaisePropertyChanged(ref this._selectedMethodIndex, value);
        }

        private string _classSearch;
        public string ClassSearch {
            get => this._classSearch;
            set => RaisePropertyChanged(ref this._classSearch, value, this.SearchClass);
        }

        private string _fieldSearch;
        public string FieldSearch {
            get => this._fieldSearch;
            set => RaisePropertyChanged(ref this._fieldSearch, value, this.SearchField);
        }

        private string _methodSearch;
        public string MethodSearch {
            get => this._methodSearch;
            set => RaisePropertyChanged(ref this._methodSearch, value, this.SearchMethod);
        }

        private bool _ignoreCases;
        public bool IgnoreCases {
            get => this._ignoreCases;
            set => RaisePropertyChanged(ref this._ignoreCases, value);
        }

        private bool _checkContains;
        public bool CheckContains {
            get => this._checkContains;
            set => RaisePropertyChanged(ref this._checkContains, value);
        }

        private bool _useRegex;
        public bool UseRegex {
            get => this._useRegex;
            set => RaisePropertyChanged(ref this._useRegex, value);
        }

        public IMainViewScroller Scroller { get; }

        private MappingsTable Mappings { get; }

        public Command SearchClassCommand { get; }
        public Command SearchFieldCommand { get; }
        public Command SearchMethodCommand { get; }

        public Command ResetClassSearchCommand { get; }
        public Command ResetFieldSearchCommand { get; }
        public Command ResetMethodSearchCommand { get; }

        public Command OpenGlobalSearcherCommand { get; }

        public Command ClassCopyClipboardMCP     { get; }
        public Command ClassCopyClipboardObfus   { get; }
        public Command ClassCopyClipboardPackage { get; }
        public Command FieldCopyClipboardMCP     { get; }
        public Command FieldCopyClipboardSRG     { get; }
        public Command FieldCopyClipboardObfus   { get; }
        public Command MethodCopyClipboardMCP    { get; }
        public Command MethodCopyClipboardSRG    { get; }
        public Command MethodCopyClipboardObfus  { get; }
        public Command MethodCopyClipboardParams { get; }

        public MainViewModel(IMainViewScroller scroller) {
            this.Scroller = scroller;
            this.ClassMappings = new ObservableCollection<ClassMappingViewModel>();
            this.ClassSearch = "";
            this.FieldSearch = "";
            this.MethodSearch = "";
            this.SearchClassCommand = new Command(this.SearchClass);
            this.SearchFieldCommand = new Command(this.SearchField);
            this.SearchMethodCommand = new Command(this.SearchMethod);
            this.ResetClassSearchCommand = new Command(()=> { this.ClassSearch = ""; this.SelectedClassIndex = 0; });
            this.ResetFieldSearchCommand = new Command(()=> { this.FieldSearch = ""; this.SelectedFieldIndex = 0; });
            this.ResetMethodSearchCommand = new Command(()=> { this.MethodSearch = ""; this.SelectedMethodIndex = 0; });
            this.ClassSearchType = SearchType.MCP;
            this.FieldSearchType = SearchType.MCP;
            this.MethodSearchType = SearchType.MCP;
            this.Mappings = new MappingsTable();
            this.IgnoreCases = true;
            this.CheckContains = false;
            this.UseRegex = false;
            this.ClassCopyClipboardMCP     = new Command(()=> { if (SelectedClass  != null) Clipboard.SetText(SelectedClass.MCPName); });
            this.ClassCopyClipboardObfus   = new Command(()=> { if (SelectedClass  != null) Clipboard.SetText(SelectedClass.ObfName); });
            this.ClassCopyClipboardPackage = new Command(()=> { if (SelectedClass  != null) Clipboard.SetText(SelectedClass.Package); });
            this.FieldCopyClipboardMCP     = new Command(()=> { if (SelectedField  != null) Clipboard.SetText(SelectedField.MCPName); });
            this.FieldCopyClipboardSRG     = new Command(()=> { if (SelectedField  != null) Clipboard.SetText(SelectedField.SRGName); });
            this.FieldCopyClipboardObfus   = new Command(()=> { if (SelectedField  != null) Clipboard.SetText(SelectedField.ObfName); });
            this.MethodCopyClipboardMCP    = new Command(()=> { if (SelectedMethod != null) Clipboard.SetText(SelectedMethod.MCPName); });
            this.MethodCopyClipboardSRG    = new Command(()=> { if (SelectedMethod != null) Clipboard.SetText(SelectedMethod.SRGName); });
            this.MethodCopyClipboardObfus  = new Command(()=> { if (SelectedMethod != null) Clipboard.SetText(SelectedMethod.ObfName); });
            this.MethodCopyClipboardParams = new Command(() => { if (SelectedMethod != null) Clipboard.SetText(SelectedMethod.Parameters); });
            LoadMappings();
        }

        public void LoadMappings() {
            MappingsTable table = this.Mappings;
            table.Load();

            foreach (KeyValuePair<string, SRGClass> classPair in table.SeargeParser.ObfClassToClass) {
                ClassMappingViewModel classMapping = new ClassMappingViewModel(classPair.Value.Name, classPair.Key, classPair.Value.PackageOnly);
                foreach (KeyValuePair<string, HashSet<SRGMethod>> methodPair in classPair.Value.ObfToMethods) {
                    foreach (SRGMethod method in methodPair.Value) {
                        List<string> paramTypesStr = new List<string>(method.Parameters.Count);
                        foreach (ISRGObject paramType in method.Parameters) {
                            paramTypesStr.Add(paramType.Name);
                        }

                        List<string> mcpMethods = table.GetMCPMethodFromSearge(method.SeargeName);
                        if (mcpMethods == null) {
                            classMapping.Methods.Add(new MethodMappingViewModel(methodPair.Key, method.SeargeName, "(???)", paramTypesStr.ToArray(), method.ReturnType.Name));
                        }
                        else {
                            classMapping.Methods.Add(new MethodMappingViewModel(methodPair.Key, method.SeargeName, mcpMethods[0], paramTypesStr.ToArray(), method.ReturnType.Name));
                        }
                    }
                }

                foreach (KeyValuePair<string, SRGField> fieldPair in classPair.Value.ObfToFields) {
                    List<string> mcpFields = table.GetMCPFieldFromSearge(fieldPair.Value.SeargeName);
                    if (mcpFields == null) {
                        classMapping.Fields.Add(new FieldMappingViewModel(fieldPair.Key, fieldPair.Value.SeargeName, "(???)"));
                    }
                    else {
                        classMapping.Fields.Add(new FieldMappingViewModel(fieldPair.Key, fieldPair.Value.SeargeName, mcpFields[0]));
                    }
                }

                this.ClassMappings.Add(classMapping);
            }
        }

        public void SearchClass() {
            if (this.ClassSearch == string.Empty) {
                return;
            }

            for (int i = this.SelectedClassIndex + 1; i < this.ClassMappings.Count; i++) {
                ClassMappingViewModel clazz = this.ClassMappings[i];
                if (MatchClass(clazz)) {
                    this.SelectedClassIndex = i;
                    this.Scroller.ScrollClassToSelected();
                    return;
                }
            }

            this.SelectedClassIndex = 0;
        }

        public void SearchField() {
            if (this.FieldSearch == string.Empty) {
                return;
            }

            ClassMappingViewModel clazz = this.SelectedClass;
            for (int i = this.SelectedFieldIndex + 1; i < clazz.Fields.Count; i++) {
                FieldMappingViewModel field = clazz.Fields[i];
                if (MatchField(field)) {
                    this.SelectedFieldIndex = i;
                    this.Scroller.ScrollFieldToSelected();
                    return;
                }
            }

            this.SelectedFieldIndex = 0;
        }

        public void SearchMethod() {
            if (this.MethodSearch == string.Empty) {
                return;
            }

            ClassMappingViewModel clazz = this.SelectedClass;
            for (int i = this.SelectedMethodIndex + 1; i < clazz.Methods.Count; i++) {
                MethodMappingViewModel method = clazz.Methods[i];
                if (MatchMethod(method)) {
                    this.SelectedMethodIndex = i;
                    this.Scroller.ScrollMethodToSelected();
                    return;
                }
            }

            this.SelectedMethodIndex = 0;
        }

        private bool MatchMethod(MethodMappingViewModel field) {
            string toSearch = null;
            switch (this.MethodSearchType) {
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
            if (this.IgnoreCases) {
                searchValue = this.MethodSearch.ToLower();
                toSearch = toSearch.ToLower();
            }
            else {
                searchValue = this.MethodSearch;
            }

            if (this.CheckContains) {
                return toSearch.Contains(searchValue);
            }
            else {
                return toSearch.StartsWith(searchValue);
            }
        }

        private bool MatchField(FieldMappingViewModel field) {
            string toSearch = null;
            switch (this.FieldSearchType) {
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
            if (this.IgnoreCases) {
                searchValue = this.FieldSearch.ToLower();
                toSearch = toSearch.ToLower();
            }
            else {
                searchValue = this.FieldSearch;
            }

            if (this.CheckContains) {
                return toSearch.Contains(searchValue);
            }
            else {
                return toSearch.StartsWith(searchValue);
            }
        }

        private bool MatchClass(ClassMappingViewModel field) {
            string toSearch = null;
            switch (this.ClassSearchType) {
                case SearchType.MCP:
                    toSearch = field.MCPName;
                    break;
                case SearchType.Obfuscated:
                    toSearch = field.ObfName;
                    break;
                case SearchType.Package:
                    toSearch = field.Package;
                    break;
            }

            if (toSearch == null) {
                return false;
            }

            string searchValue;
            if (this.IgnoreCases) {
                searchValue = this.ClassSearch.ToLower();
                toSearch = toSearch.ToLower();
            }
            else {
                searchValue = this.ClassSearch;
            }

            if (this.CheckContains) {
                return toSearch.Contains(searchValue);
            }
            else {
                return toSearch.StartsWith(searchValue);
            }
        }
    }
}
