using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REghZyFramework.Utilities.String;

namespace MCPMappingsV2.Mappings.Data.Parser {
    public class SRGParser {
        public const string KEY_PACKAGE = "PK";
        public const string KEY_CLASS = "CL";
        public const string KEY_FIELD = "FD";
        public const string KEY_METHOD = "MD";

        public const string SIDE_CLIENT = "#C"; // 0
        public const string SIDE_SERVER = "#S"; // 2

        // return types of obfuscated functions
        public const string RETURN_BOOL = "Z";
        public const string RETURN_VOID = "V";
        public const string RETURN_INT = "I";
        public const string RETURN_STRING = "Ljava/lang/String;";
        public const string RETURN_ITEMSTACK = "Lnet/minecraft/src/ItemStack;";

        public string FilePath { get; set; }

        public Dictionary<SRGPackage, SRGPackage> PackageMapper { get; }
        public Dictionary<string, SRGClass> ObfClassToClass { get; }

        public SRGParser(string path) {
            this.FilePath = path;
            this.PackageMapper = new Dictionary<SRGPackage, SRGPackage>();
            this.ObfClassToClass = new Dictionary<string, SRGClass>();
        }

        public void Load() {
            if (!File.Exists(this.FilePath)) {
                throw new FileNotFoundException($"The SRG file '{this.FilePath}' does not exist!");
            }

            foreach(string line in File.ReadAllLines(this.FilePath)) {
                if (line.Length < 2) {
                    continue;
                } 

                switch (line.Substring(0, 2)) {
                    case KEY_PACKAGE: ParsePackage(line.Substring(4)); break;
                    case KEY_CLASS: ParseClass(line.Substring(4)); break;
                    case KEY_FIELD: ParseField(line.Substring(4)); break;
                    case KEY_METHOD: ParseMethod(line.Substring(4)); break;
                }
            }
        }

        // public void ParsePackage(string line) {
        // 
        // }
        // 
        // public void ParseClass(string line) {
        // 
        // }
        // 
        // public void ParseField(string line) {
        // 
        // }
        // 
        // public void ParseMethod(string line) {
        // 
        // }

        public void ParsePackage(string line) {
            if (line.Length < 3) {
                return;
            }
        
            int whiteSpaceIndex = line.IndexOf(' ');
            if (whiteSpaceIndex == -1 || whiteSpaceIndex >= line.Length) {
                return;
            }
        
            int side;
            string packageA;
            string packageB;
        
            int packageBSideSplit = line.IndexOf(' ', whiteSpaceIndex + 1);
            if (packageBSideSplit == -1) {
                side = 0;
                packageA = line.Substring(0, whiteSpaceIndex);
                packageB = line.Substring(whiteSpaceIndex + 1);
            }
            else {
                packageA = line.Substring(0, whiteSpaceIndex);
                packageB = line.Extract(whiteSpaceIndex + 1, packageBSideSplit);
                side = (line.Substring(packageBSideSplit + 1, 2) == SIDE_CLIENT) ? 0 : 2;
            }
        
            this.PackageMapper.Add(new SRGPackage(packageA, side), new SRGPackage(packageB, side));
        }
        
        public void ParseClass(string line) {
            if (line.Length < 3) {
                return;
            }
        
            int whiteSpaceIndex = line.IndexOf(' ');
            if (whiteSpaceIndex == -1 || whiteSpaceIndex >= line.Length) {
                return;
            }
        
            int side;
            string obfuscated;
            string packageClass;
        
            int secondSplit = line.IndexOf(' ', whiteSpaceIndex + 1);
            if (secondSplit == -1) {
                side = 0;
                obfuscated = line.Substring(0, whiteSpaceIndex);
                packageClass = line.Substring(whiteSpaceIndex + 1);
            }
            else {
                obfuscated = line.Substring(0, whiteSpaceIndex);
                packageClass = line.Extract(whiteSpaceIndex + 1, secondSplit);
                side = (line.Substring(secondSplit + 1, 2) == SIDE_CLIENT) ? 0 : 2;
            }
        
            this.ObfClassToClass.Add(obfuscated, new SRGClass(obfuscated, packageClass, side));
        }
        
        public void ParseField(string line) {
            if (line.Length < 3) {
                return;
            }
        
            int whiteSpaceIndex = line.IndexOf(' ');
            if (whiteSpaceIndex == -1 || whiteSpaceIndex >= line.Length) {
                return;
            }
        
            string obfuscated = line.Substring(0, whiteSpaceIndex);
            int obfClassFieldSplit = obfuscated.IndexOf('/');
            if (obfClassFieldSplit == -1) {
                return;
            }
        
            SRGClass clazz;
            string obfClass = obfuscated.Substring(0, obfClassFieldSplit);
            string obfField = obfuscated.Substring(obfClassFieldSplit + 1);
            if (!this.ObfClassToClass.TryGetValue(obfClass, out clazz)) {
                return;
            }
        
            string packageClassField = line.Substring(whiteSpaceIndex + 1);
            int lastPathSplit = packageClassField.LastIndexOf('/');
            if (lastPathSplit == -1) {
                return;
            }
        
            string seargeField = packageClassField.Substring(lastPathSplit + 1);
        
            int sideSplit = seargeField.IndexOf(' ');
            if (sideSplit == -1) {
                clazz.AddField(new SRGField(clazz, obfField, seargeField, 0));
            }
            else {
                int side = (seargeField.Substring(sideSplit + 1, 2) == SIDE_CLIENT) ? 0 : 2;
                clazz.AddField(new SRGField(clazz, obfField, seargeField.Substring(0, sideSplit), side));
            }
        }
        
        public void ParseMethod(string line) {
            if (line.Length < 3) {
                return;
            }
        
            int whiteSpaceIndex = line.IndexOf(' ');
            if (whiteSpaceIndex == -1 || whiteSpaceIndex >= line.Length) {
                return;
            }

            string obfClassMethod = line.Substring(0, whiteSpaceIndex);
            int obfClassMethodSplit = obfClassMethod.IndexOf('/');
            if (obfClassMethodSplit == -1) {
                return;
            }

            string obfClassName = obfClassMethod.Extract(0, obfClassMethodSplit);

            SRGClass clazz;
            if (!this.ObfClassToClass.TryGetValue(obfClassName, out clazz)) {
                return;
            }
        
            string obfMethodName = obfClassMethod.Substring(obfClassMethodSplit + 1);
        
            string theRest = line.Substring(whiteSpaceIndex + 1);
            int paramsReturnSplit = theRest.IndexOf(' ');
            if (paramsReturnSplit == -1) {
                return;
            }
        
            string paramsReturn = theRest.Substring(0, paramsReturnSplit);
            string afterParams = theRest.Substring(paramsReturnSplit + 1);
            int split = afterParams.IndexOf(' ');
            if (split == -1) {
                return;
            }
        
            string fullPath = afterParams.Substring(0, split);
            int classMethodSplit = fullPath.LastIndexOf('/');
            string className = fullPath.Substring(0, classMethodSplit);
            string methodNameSrg = fullPath.Substring(classMethodSplit + 1);
        
            string afterClassMethod = afterParams.Substring(split + 1);
            string paramsStr = afterClassMethod.Between("(", ")");
            string returnStr = afterClassMethod.After(")", paramsStr.Length);
            List<ISRGObject> parameters = (paramsStr.Length == 0) ? new List<ISRGObject>() : GetParameters(paramsStr);
            ISRGObject returnType;
            if (returnStr.EndsWith(";")) {
                returnType = SRGType.GetOrCreateType(returnStr.Substring(0, returnStr.Length - 1));
            }
            else {
                returnType = SRGType.GetPrimitive(returnStr[0]);
            }
        
            clazz.AddMethod(new SRGMethod(clazz, obfMethodName, methodNameSrg, parameters, returnType));
        }

        public List<ISRGObject> GetParameters(string betweenBrackets) {
            List<ISRGObject> list = new List<ISRGObject>(8);

            char[] chars = betweenBrackets.ToCharArray();

            StringBuilder buffer = new StringBuilder(64);
            for (int i = 0, len = chars.Length; i < len; i++) {
                char c = chars[i];
                if (c == ';') {
                    list.Add(SRGType.GetOrCreateType(GetSimpleTypeName(buffer.ToString())));
                    buffer.Clear();
                }
                else {
                    buffer.Append(chars[i]);
                }
            }

            if (buffer.Length == 0) {
                return list;
            }
            else {
                foreach (char c in buffer.ToString()) {
                    ISRGObject obj = SRGType.GetPrimitive(c);
                    if (obj != null) {
                        list.Add(obj);
                    }
                }
                return list;
            }
        }

        public static string GetSimpleTypeName(string full) {
            // e.g. "Ljava/lang/String;"
            int lastSplitIndex = full.LastIndexOf('/');
            if (lastSplitIndex == -1) {
                return null;
            }

            if (full.Contains(';')) {
                return full.Substring(lastSplitIndex + 1, full.Length - lastSplitIndex - 2);
            }
            else {
                return full.Substring(lastSplitIndex + 1);
            }
        }
    }
}
