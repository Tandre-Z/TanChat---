using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class JsonController : MonoBehaviour
{

    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        Office office = new Office();
        office.id = "admin";
        office.name = "111111";

        string jsonStr=JsonUtility.ToJson(office);
        //Debug.Log(jsonStr);

        string jsonStr0 = "{\"id\":\"1sdger2\",\"name\":\"111111\"}";
        Office office1 = JsonUtility.FromJson<Office>(jsonStr0);
        //Debug.Log(office1.id+" "+office1.name);

        string jsonStr1 = "{\"id\":\"11111111111\",\"name\":\"111111\"}";
        Office office2 = JsonConvert.DeserializeObject<Office>(jsonStr1);
        //Debug.Log(office2.id + " " + office2.name);

        StartCoroutine(GetConfig(SetImage));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //设置图片格式
    public void SetImage(string json)
    {
        Config config = JsonConvert.DeserializeObject<Config>(json);
        image.GetComponent<RectTransform>().sizeDelta = new Vector2(config.Width, config.Height);
        Color newColor;
        if (ColorUtility.TryParseHtmlString(config.Color, out newColor))
        {
            image.color = newColor;
        }

        BinaryFormatter binaryFormatter=new BinaryFormatter();

        FileStream fileStream = File.Create(Application.streamingAssetsPath + "/Config");

        binaryFormatter.Serialize(fileStream, config);
        fileStream.Close();
    }
    //读取配置文件
    public IEnumerator GetConfig(Action<string> callback)
    {
        string path = System.IO.Path.Combine(Application.streamingAssetsPath, "Config.json");
        System.Uri uri = new System.Uri(path);
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(uri.AbsoluteUri);
        yield return unityWebRequest.SendWebRequest();
        if(unityWebRequest.isHttpError||unityWebRequest.isNetworkError)
        {
            Debug.LogError(unityWebRequest.error);
        }
        else
        {
            string json = unityWebRequest.downloadHandler.text;
            callback(json);
        }
    }
}
