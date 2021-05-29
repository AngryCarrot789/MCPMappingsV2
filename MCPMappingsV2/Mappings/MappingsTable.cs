using MCPMappingsV2.Mappings.Data.CSV;
using MCPMappingsV2.Mappings.Types;
using MCPMappingsV2.Utils;

namespace MCPMappingsV2.Mappings {
    public class MappingsTable {
        public const string CSV_PATH = "F:\\MinecraftUtils\\modding\\forge\\1.6.4\\Forge1.6.4-9.11.1.965-complete\\mcp\\conf\\";
        public const string CSV_PATH_FIELDS = CSV_PATH + "fields.csv";
        public const string CSV_PATH_METHODS = CSV_PATH + "methods.csv";
        public const string CSV_PATH_PACKAGES = CSV_PATH + "packages.csv";
        public const string CSV_PATH_PARAMS = CSV_PATH + "params.csv";
        public const string CSV_PATH_OBFTOCLASS = CSV_PATH + "joined.csv";

        // The actual raw loaded values
        public CSVLoader<CSVJavaClass> CSVClassLoader { get; }
        public CSVLoader<CSVJavaClass> CSVMethodLoader { get; }
        public CSVLoader<CSVJavaClass> CSVFieldLoader { get; }

        // Cached and built-up values
        public HashSetMultiMap<string, JavaClass> ObfToMCP_Classes { get; }
        public HashSetMultiMap<string, JavaClass> MCPToObf_Classes { get; }

        public MappingsTable() {
            this.CSVClassLoader = new CSVLoader<CSVJavaClass>(CSV_PATH_PACKAGES, 5000);
            this.CSVMethodLoader = new CSVLoader<CSVJavaClass>(CSV_PATH_METHODS, 10000);
            this.CSVFieldLoader = new CSVLoader<CSVJavaClass>(CSV_PATH_FIELDS, 5000);

            this.ObfToMCP_Classes = new HashSetMultiMap<string, JavaClass>();
        }

        public void Load() {
            this.CSVClassLoader.LoadRecords();
            this.CSVMethodLoader.LoadRecords();
            this.CSVFieldLoader.LoadRecords();


        }
    }
}
