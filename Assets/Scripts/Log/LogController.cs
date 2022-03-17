using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LogController : MonoBehaviour
{

    string path;

    // Start is called before the first frame update
    void Start()
    {
        path = Application.persistentDataPath + "/Log";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        Application.logMessageReceivedThreaded += Application_logMessageReceivedThreaded;
    }

    private void Application_logMessageReceivedThreaded(string condition, string stackTrace, LogType type)
    {
        string content=string.Format("[{0:yyyy-MM-dd hh:mm:ss}] {1}<{2}>",System.DateTime.Now.ToString(),condition,stackTrace);
        WriteLog(content);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WriteLog(string content)
    {
        string fileName = string.Format("{0:yyyy-MM-dd}.log", System.DateTime.Now);
        string logFilePath=System.IO.Path.Combine(path, fileName);
        WriteText(logFilePath,content);
    }

    public void WriteText(string path, string content)
    {
        FileStream fileStream = new FileStream(path, FileMode.Append);
        StreamWriter writer = new StreamWriter(fileStream);
        writer.WriteLine(content);
        writer.Close();
        fileStream.Close();
    }
}
