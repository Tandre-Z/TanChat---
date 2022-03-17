using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;

public class HttpController : MonoBehaviour
{ 
    public InputField postText;
    public Text chatText;
    public Scrollbar scollerBar;

    int a = 2;
    public List<Toggle> toggles=new List<Toggle>();

    string APIKEY0= "c71a12a85a697d5ff62b00c40192da13";//天行数据机器人api
    string APIKEY1 = "LL9FY301Z3WV1APAC0K4XZM5PGS7TWPY03267VO9YLCH257NOW";//道翰天琼认知智能机器人api
    string APIKEY2 = "a2762aa5494b4a17985bb6eb4e914b37";//图灵机器人api
    string question="robot";
    string json= "{\"reqType\":0,\"perception\":{\"inputText\":{\"text\":\"北京\"}},\"userInfo\":{\"apiKey\":\"eaf3daedeb374564bfe9db10044bc20b\",\"userId\":\"6789\"}}";
    // Start is called before the first frame update
    
    void start()
    {
        StartCoroutine(chatText2Bottom());
        //chatText.text = "";
        //StartCoroutine(PostText(json));
       //StartCoroutine(GetReply(question,ParseJson));
    }
    void OnEnable()
    {
        StartCoroutine(chatText2Bottom());
    }
    //获取回复结果
    public IEnumerator GetReply(string question,int a, Action<string> callback)
    {
        string url="http://www.tuling123.com/openapi/api?key=" + APIKEY2 + "&info=" + question;//默认为图灵机器人
        switch (a)
        {
            case 0:
                url = "http://api.tianapi.com/robot/index?key=" + APIKEY0 + "&question=" + question;//天行数据机器人请求地址
                break;
            case 1:
                url = "http://www.weilaitec.com/cigirlrobot.cgr?key=" + APIKEY1 + "&msg=" + question + "&ip=2995994479&userid=2995994479&appid=1643178363659";//道翰天琼认知智能机器人请求地址
                break;
            case 2: 
                url = "http://www.tuling123.com/openapi/api?key=" + APIKEY2 + "&info=" + question;//道翰天琼认知智能机器人请求地址
                break ;
            default:
                break;
        }

        UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);

        chatText.text += String.Format("\nTandre:") + "正在请求服务器";
        yield return unityWebRequest.SendWebRequest();
        chatText.text = chatText.text.Substring(0, chatText.text.Length - (String.Format("\nTandre:") + "正在请求服务器").Length);

        if (unityWebRequest.isHttpError || unityWebRequest.isNetworkError)
        {
            chatText.text += String.Format("\nTandre:") + unityWebRequest.error;
            Debug.LogError(unityWebRequest.error);
        }
        else
        {
            //Debug.Log(unityWebRequest2.downloadHandler.text);
            callback.Invoke(unityWebRequest.downloadHandler.text);
        }
    }

    //解析json
    public void ParseJson(string json)
    {
        Response  response=JsonConvert.DeserializeObject<Response>(json);
        chatText.text += String.Format("\n小天:") + response.newslist[0].Reply + String.Format("\n");
        StartCoroutine(chatText2Bottom());
    }

    public void PaarseTulingJson(string json)
    {
        //chatText.text += String.Format("\nTuling:") + json;
        TulingResponse response = JsonConvert.DeserializeObject<TulingResponse>(json);
        chatText.text += String.Format("\n图图:") + response.text + String.Format("\n"); 
        StartCoroutine(chatText2Bottom());
    }

    public void PostText()
    {
        if (postText.text != "")
        {
            StopCoroutine(chatText2Bottom());
            
            chatText.text+= "\nTandre:" + postText.text+String.Format("\n");
            StartCoroutine(chatText2Bottom());
            if(toggles[0]!=null&&toggles[0].isOn)
            {
                StartCoroutine(GetReply(postText.text, 0, ParseJson));
            }
            else if(toggles[1]!=null&&toggles[1].isOn)
            {
                StartCoroutine(GetReply(postText.text, 2, PaarseTulingJson));
            }
            else if(toggles[2]!=null&&toggles[2].isOn)
            {
                StartCoroutine(GetReply(postText.text, 2, PaarseTulingJson));
            }
           
            postText.text = "";
        }
    }

    public IEnumerator chatText2Bottom()
    {
        if (scollerBar.value > 0.0001f)
        {
            scollerBar.value -= scollerBar.value / 10; 
            yield return new WaitForSeconds(0.02f);
            StartCoroutine(chatText2Bottom());
        }
        else
            StopCoroutine(chatText2Bottom());
       
        
    }
    /////post方法/////
    //public IEnumerator PostText(string json)
    //{
    //    //Dictionary<string, string> froms = new Dictionary<string, string>();
    //    //froms.Add("key", APIKEY);
    //    //froms.Add("question", question);

    //    string url = "http://openapi.turingapi.com/openapi/api/v2";

    //    UnityWebRequest unityWebRequest = UnityWebRequest.Post(url, JsonConvert.DeserializeObject(json).ToString());
    //    //UnityWebRequest unityWebRequest = UnityWebRequest.Post(url, froms);

    //    yield return unityWebRequest.SendWebRequest();

    //    if (unityWebRequest.isHttpError || unityWebRequest.isNetworkError)
    //    {
    //        Debug.Log("Error:" + unityWebRequest.error);
    //    }
    //    else
    //    {
    //        Debug.Log(unityWebRequest.downloadHandler.text);
    //    }
    //}
}
