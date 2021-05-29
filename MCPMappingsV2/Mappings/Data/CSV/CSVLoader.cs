using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper.Configuration;
using CsvHelper;

namespace MCPMappingsV2.Mappings.Data.CSV {
    public class CSVLoader<T> {
        private CsvConfiguration Config { get; }

        public string RecordsPath { get; set; }

        public List<T> Fields { get; }

        public CSVLoader(string recordsPath, int initialSize = 32) {
            this.Config = new CsvConfiguration(CultureInfo.CurrentCulture);
            this.Fields = new List<T>(initialSize);
            this.RecordsPath = recordsPath;
        }

        public void LoadRecords() {
            this.Fields.Clear();

            if (!File.Exists(this.RecordsPath)) {
                throw new FileNotFoundException($"The records file '{this.RecordsPath}' was not found!");
            }

            using (StreamReader reader = new StreamReader(this.RecordsPath)) {
                using (CsvReader csv = new CsvReader(reader, this.Config)) {
                    foreach (T field in csv.GetRecords<T>()) {
                        this.Fields.Add(field);
                    }
                }
            }
        }
    }
}
