using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(BgmSfxNameEnumGenerator))]
public class GenerateBgmSfxNameEnums : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        StringBuilder bgmEnums = new();
        StringBuilder sfxEnums = new();

        if (GUILayout.Button("Generate Bgm/Sfx Name Enums"))
        {
            var bgms = Resources.LoadAll("Audio/Bgms/");
            var sfxs = Resources.LoadAll("Audio/Sfxs/");

            for (int i = 0; i < bgms.Length; ++i)
                bgmEnums.AppendLine($"\t{bgms[i].name},");
            for (int i = 0; i < sfxs.Length; ++i)
                sfxEnums.AppendLine($"\t{sfxs[i].name},");

            StringBuilder bgmScript = new();
            StringBuilder sfxScript = new();

            bgmScript.AppendLine("public enum EBgm")
                .AppendLine("{")
                .Append(bgmEnums)
                .AppendLine("}");
            sfxScript.AppendLine("public enum ESfx")
                .AppendLine("{")
                .Append(sfxEnums)
                .AppendLine("}");

            StreamWriter swBgm = new(File.Create($"{Application.dataPath}/Audio/EBgm.cs"));
            StreamWriter swSfx = new(File.Create($"{Application.dataPath}/Audio/ESfx.cs"));

            swBgm.Write(bgmScript.ToString());
            swSfx.Write(sfxScript.ToString());
            swBgm.Close();
            swSfx.Close();

            AssetDatabase.Refresh();

            Debug.Log("Generate Bgm/Sfx Name Enums!");
        }
    }
}
#endif

public class BgmSfxNameEnumGenerator : MonoBehaviour
{

}
