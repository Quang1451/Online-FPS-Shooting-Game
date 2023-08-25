using UnityEditor;
using UnityEngine;
using System.IO;
using Unity.Plastic.Newtonsoft.Json.Linq;

public class JSONClassGeneratorEditor : EditorWindow
{
    [MenuItem("Tools/JSON Class Generator")]
    public static void ShowWindow()
    {
        JSONClassGeneratorEditor window = GetWindow<JSONClassGeneratorEditor>();
        window.titleContent = new GUIContent("JSON Class Generator");
        window.Show();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Generate C# Class from JSON"))
        {
            string jsonFilePath = EditorUtility.OpenFilePanel("Select JSON File", "", "json");

            if (!string.IsNullOrEmpty(jsonFilePath))
            {
                string jsonContent = File.ReadAllText(jsonFilePath);

                string className = Path.GetFileNameWithoutExtension(jsonFilePath);
                string classCode = GenerateClassCodeFromJSON(className, jsonContent, jsonFilePath);

                string savePath = EditorUtility.SaveFilePanel("Save C# Class", "", className, "cs");
                if (!string.IsNullOrEmpty(savePath))
                {
                    File.WriteAllText(savePath, classCode);
                    Debug.Log("C# Class generated and saved at: " + savePath);
                }
            }
        }
    }

    private string GenerateClassCodeFromJSON(string className, string jsonContent, string jsonFilepath)
    {
        JObject jsonObject = JObject.Parse(jsonContent);


        string classCode = "using UnityEngine;\nusing System.Collections.Generic;\n\n";

        string structCode = "";
        string structName = $"{className}Struct";
        string keyCode = "";
        string keyCodeNAme = $"{className}Key";

        classCode += $"public class {className}\n";
        classCode += "{\n";

        //Create class
        foreach (var property in jsonObject.Properties())
        {
            string propertyName = property.Name;
            JTokenType propertyType = property.Value.Type;

            switch (propertyType)
            {
                case JTokenType.Array:
                    classCode += $"\tpublic static Dictionary<string,List<{structName}>> dictionary = new Dictionary<string,List<{structName}>>();\n";
                    break;
                case JTokenType.Object:
                    classCode += $"\tpublic static Dictionary<string,{structName}> dictionary = new Dictionary<string,{structName}>();\n";
                    break;
                default:
                    classCode += $"\tpublic static {GetCSharpType(propertyType)} {propertyName};\n";
                    continue;
                    break;
            }
            break;
        }

        classCode += "}\n";

        //Create struct
        foreach (var property in jsonObject.Properties())
        {
            JTokenType propertyType = property.Value.Type;

            if (propertyType == JTokenType.Array)
            {
                structCode += GenerateStructCodeFromJSON(structName, (JArray)property.Value);
                break;
            }

            if (propertyType == JTokenType.Object)
            {
                structCode += GenerateStructCodeFromJSON(structName, (JObject)property.Value);
                break;
            }
        }
       
        classCode += structCode;

        //Create keycode
        keyCode += $"\npublic static class {keyCodeNAme}\n";
        keyCode += "{\n";

        foreach (var property in jsonObject.Properties())
        {
            string propertyName = property.Name;
            JTokenType propertyType = property.Value.Type;

            keyCode += $"\tpublic static string Key{propertyName} = \"{propertyName}\";\n";
        }

        keyCode += "}";
        classCode += keyCode;
        return classCode;
    }

    private string GenerateStructCodeFromJSON(string structName, JArray jsonArray)
    {
        string structCode = $"\npublic struct {structName}\n";
        structCode += "{\n";

        JObject firstObject = (JObject)jsonArray.First;
        foreach (var property in firstObject.Properties())
        {
            string propertyName = property.Name;
            JTokenType propertyType = property.Value.Type;

            structCode += $"\tpublic {GetCSharpType(propertyType)} {propertyName};\n";
        }

        structCode += "}\n";

        return structCode;
    }

    private string GenerateStructCodeFromJSON(string structName, JObject firstObject)
    {
        string structCode = $"\npublic struct {structName}\n";
        structCode += "{\n";

        foreach (var property in firstObject.Properties())
        {
            string propertyName = property.Name;
            JTokenType propertyType = property.Value.Type;

            structCode += $"\tpublic {GetCSharpType(propertyType)} {propertyName};\n";
        }

        structCode += "}\n";

        return structCode;
    }

    private string GetCSharpType(JTokenType jsonType)
    {
        switch (jsonType)
        {
            case JTokenType.Integer:
                return "int";
            case JTokenType.Float:
                return "float";
            case JTokenType.String:
                return "string";
            case JTokenType.Boolean:
                return "bool";
            default:
                return "object";
        }
    }
}