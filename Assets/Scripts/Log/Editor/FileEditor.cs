using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FileEditor : Editor
{
    [MenuItem("Log/打开日志文件夹")]
    public static void OpenFilePath()
    {
        string path = Application.persistentDataPath + "/Log";
        if (!System.IO.Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }
        EditorUtility.RevealInFinder(path);
    }  
    
    [MenuItem("Log/打开json路径")]
    public static void OpenstreamingPath()
    {
        string path = Application.streamingAssetsPath;
        if (!System.IO.Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }
        EditorUtility.RevealInFinder(path);
    }
}
