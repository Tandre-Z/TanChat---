using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Response
{
    public int Code { get; set; }
    public string Msg{get;set; }
    public List<ReplyText> newslist;
}
