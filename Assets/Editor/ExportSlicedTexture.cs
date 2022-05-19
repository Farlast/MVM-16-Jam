using UnityEngine;
using UnityEditor;

public class ExportSlicedTexture : EditorWindow
{
    private Texture2D texture;
    private string fileName;
    [MenuItem("Window/Export Sliced Texture")]
    public static void ShowWindow()
    {
        GetWindow<ExportSlicedTexture>("Export Sliced Texture");
    }

    private void OnGUI()
    {
        texture = (Texture2D)EditorGUILayout.ObjectField(texture, typeof(Texture2D), allowSceneObjects: false);
        fileName = EditorGUILayout.TextField("File name: ", fileName);
        if (GUILayout.Button("Run Function") && texture != null)
        {
            SaveTextureAsPNG(texture, fileName);
        }
    }

    public static void SaveTextureAsPNG(Texture2D texture, string fileName)
    {
        byte[] _bytes = texture.EncodeToPNG();
        var dirPath = Application.dataPath + "/Sprite Sheets/";
        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }
        System.IO.File.WriteAllBytes(dirPath + fileName + ".png", _bytes);
        Debug.Log(_bytes.Length / 1024 + "Kb was saved as: " + fileName + ".png");
    }
}