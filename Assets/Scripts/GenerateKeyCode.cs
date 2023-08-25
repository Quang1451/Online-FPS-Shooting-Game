using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GenerateKeyCode
{
    public static string folderPath = "Assets/Scripts/Keys";
    public static void Generate(List<string> listKey, string fileName)
    {
        string fileKey = Path.Combine(folderPath, $"{fileName}.cs");
        string content = "//THIS CLASS IS GENERATE CODE CLASS\n";
        content += $"public class {fileName}\n";
        content += "{\n";

        foreach (var key in listKey)
        {
            var temp = key;
            content += $"\tpublic static string {temp.ToUpper()} = \"{key}\";\n";
        }
        content += "}\n";
        
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        
        File.WriteAllText(fileKey, content);
        
        Debug.Log($"Generate new class at {fileKey}");
    }
}
