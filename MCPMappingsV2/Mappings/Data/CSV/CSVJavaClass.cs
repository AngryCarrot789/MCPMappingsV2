using CsvHelper.Configuration.Attributes;

namespace MCPMappingsV2.Mappings.Data.CSV {
    public struct CSVJavaClass {
        [Index(0)]
        public string ClassName { get; set; }

        [Index(1)]
        public string PackageOnly { get; set; }

        public string GetFullPath() {
            return this.PackageOnly + "/" + this.ClassName;
        }
    }
}
