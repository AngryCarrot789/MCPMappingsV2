using System.Collections.Generic;
using MCPMappingsV2.Mappings.Data.CSV;
using MCPMappingsV2.Mappings.Data.Parser;
using MCPMappingsV2.Utils;

namespace MCPMappingsV2.Mappings {
    public class MappingsTable {
        public const string CSV_PATH = "F:\\MinecraftUtils\\modding\\forge\\1.6.4\\Forge1.6.4-9.11.1.965-complete\\mcp\\conf\\";
        public const string CSV_PATH_FIELDS = CSV_PATH + "fields.csv";
        public const string CSV_PATH_METHODS = CSV_PATH + "methods.csv";
        public const string CSV_PATH_PACKAGES = CSV_PATH + "packages.csv";
        public const string CSV_PATH_PARAMS = CSV_PATH + "params.csv";
        public const string CSV_PATH_OBFTOCLASS = CSV_PATH + "joined.srg";

        // The actual raw loaded values
        public CSVLoader<CSVJavaClass> CSVClassLoader { get; }
        public CSVLoader<CSVJavaMethod> CSVMethodLoader { get; }
        public CSVLoader<CSVJavaField> CSVFieldLoader { get; }

        public SRGParser SeargeParser { get; }

        // Cached and built-up values
        public ListMultiMap<string, string> FieldSeargeToMCPCache { get; }
        public ListMultiMap<string, string> MethodSeargeToMCPCache { get; }

        public MappingsTable() {
            this.CSVClassLoader = new CSVLoader<CSVJavaClass>(CSV_PATH_PACKAGES, 5000);
            this.CSVMethodLoader = new CSVLoader<CSVJavaMethod>(CSV_PATH_METHODS, 10000);
            this.CSVFieldLoader = new CSVLoader<CSVJavaField>(CSV_PATH_FIELDS, 5000);

            this.FieldSeargeToMCPCache = new ListMultiMap<string, string>();
            this.MethodSeargeToMCPCache = new ListMultiMap<string, string>();

            this.SeargeParser = new SRGParser(CSV_PATH_OBFTOCLASS);
        }

        public List<string> GetMCPFieldFromSearge(string searge) {
            if (this.FieldSeargeToMCPCache.TryGetValue(searge, out List<string> values)) {
                return values;
            }

            return null;
        }

        public List<string> GetMCPMethodFromSearge(string searge) {
            if (this.MethodSeargeToMCPCache.TryGetValue(searge, out List<string> values)) {
                return values;
            }

            return null;
        }

        public void Load() {
            SeargeParser.Load();
            this.CSVClassLoader.LoadRecords();
            this.CSVMethodLoader.LoadRecords();
            this.CSVFieldLoader.LoadRecords();
            BuiltCache();
        }

        public void BuiltCache() {
            foreach(CSVJavaMethod method in this.CSVMethodLoader.Fields) {
                this.MethodSeargeToMCPCache.Add(method.SeargeName, method.MCPName);
            }

            foreach (CSVJavaField field in this.CSVFieldLoader.Fields) {
                this.FieldSeargeToMCPCache.Add(field.SeargeName, field.MCPName);
            }
        }
    }
}
