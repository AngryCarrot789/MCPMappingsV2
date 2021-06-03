using System.Collections.Generic;
using REghZyFramework.Utilities.String;

namespace MCPMappingsV2.Mappings.Data.Parser {
    public struct SRGType : ISRGObject {
        public static SRGType SRGByte = new SRGType("byte");
        public static SRGType SRGShort = new SRGType("short");
        public static SRGType SRGChar = new SRGType("char");
        public static SRGType SRGInt = new SRGType("int");
        public static SRGType SRGLong = new SRGType("long");
        public static SRGType SRGFloat = new SRGType("float");
        public static SRGType SRGDouble = new SRGType("double");
        public static SRGType SRGBool = new SRGType("bool");
        public static SRGType SRGVoid = new SRGType("void");

        private static Dictionary<string, ISRGObject> NameToType { get; }

        static SRGType() {
            NameToType = new Dictionary<string, ISRGObject>();
            NameToType.Add(SRGByte.Name, SRGByte);
            NameToType.Add(SRGShort.Name, SRGShort);
            NameToType.Add(SRGChar.Name, SRGChar);
            NameToType.Add(SRGInt.Name, SRGInt);
            NameToType.Add(SRGLong.Name, SRGLong);
            NameToType.Add(SRGFloat.Name, SRGFloat);
            NameToType.Add(SRGDouble.Name, SRGDouble);
            NameToType.Add(SRGBool.Name, SRGBool);
            RegisterType("ItemStack");
            RegisterType("String");
            RegisterType("Block");
            RegisterType("TileEntity");
        }

        public static ISRGObject GetPrimitive(char primitive) {
            switch (primitive) {
                case 'Z': return SRGBool;
                case 'B': return SRGByte;
                case 'C': return SRGChar;
                case 'D': return SRGDouble;
                case 'F': return SRGFloat;
                case 'S': return SRGShort;
                case 'I': return SRGInt;
                case 'J': return SRGLong;
                case 'V': return SRGVoid;
            }

            return null;
        }

        public static ISRGObject GetOrCreateType(string name) {
            if (name.StartsWith("L")) {
                int lastIndex = name.LastIndexOf('/');
                if (lastIndex != -1) {
                    name = name.Extract(lastIndex + 1, name.Length);
                }
            }

            if (NameToType.TryGetValue(name, out ISRGObject obj)) {
                return obj;
            }

            return RegisterType(name);
        }

        private static SRGType RegisterType(string name) {
            SRGType type = new SRGType(name);
            NameToType.Add(name, type);
            return type;
        }

        public string Name { get; }

        private SRGType(string name) {
            this.Name = name;
        }
    }
}
