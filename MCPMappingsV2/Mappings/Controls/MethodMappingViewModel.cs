using System.Text;

namespace MCPMappingsV2.Mappings.Controls {
    public class MethodMappingViewModel : MappingViewModel {
        public string ReturnType { get; protected set; }

        public MethodMappingViewModel(string obfuscated, string searge, string mcp, string[] parameterTypes, string returnType) {
            this.ObfName = obfuscated;
            this.SRGName = searge;
            this.MCPName = mcp;
            this.ReturnType = returnType;

            if (parameterTypes == null || parameterTypes.Length == 0) {
                this.Parameters = "()";
            }
            else {
                StringBuilder sb = new StringBuilder(32);
                sb.Append('(');
                for (int i = 0, end = parameterTypes.Length - 1; i < end; i++) {
                    string paramType = parameterTypes[i];
                    sb.Append(paramType).Append(", ");
                }
                sb.Append(parameterTypes[parameterTypes.Length - 1]);
                this.Parameters = sb.Append(')').ToString();
            }
        }
    }
}
