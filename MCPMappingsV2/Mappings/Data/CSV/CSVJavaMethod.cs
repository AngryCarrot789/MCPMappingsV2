using CsvHelper.Configuration.Attributes;

namespace MCPMappingsV2.Mappings.Data.CSV {
    public struct CSVJavaMethod {
        /// <summary>
        /// The searge method name
        /// </summary>
        [Index(0)]
        public string SeargeName { get; set; }

        /// <summary>
        /// The MCP (aka readable) method name
        /// </summary>
        [Index(1)]
        public string MCPName { get; set; }

        /// <summary>
        /// The side the code should be executed on (client, server)
        /// </summary>
        [Index(2)]
        public int Side { get; set; }

        /// <summary>
        /// The description of the method
        /// </summary>
        [Index(3)]
        public string Description { get; set; }
    }
}
