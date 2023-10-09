using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using LitJson;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
//大题目的json解析结构
public class Question{
    public string question;
    public string answer;
}

//选择题的json解析结构
public class MultipleChoice{
    public string question;
    public int answer;
}

public class Content : MonoBehaviour
{
    public GameObject content;
    private int questionID;

    public UnityEngine.UI.Image image;
    // Start is called before the first frame update
    void Start()
    {
        image.sprite = Resources.Load<Sprite>("Classroom/Calculus/Limit_and_Continuity/Function/1_ans");
        image.SetNativeSize();
        Debug.Log(Resources.Load<Sprite>("Classroom/Calculus/Limit_and_Continuity/Function/1"));
        JsonParser();
       // content.GetComponent<TEXDraw>().text = "$$函数y=sqrt{1-2x}+sqrt{e-e^\\left(frac{3x-1}{2}\\right)}$$";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void JsonParser(){
        string path = Application.streamingAssetsPath + "/Calculus/test.json";
        //读取文件
        StreamReader streamReader = File.OpenText(path);
        //存入
        string questions = streamReader.ReadToEnd();
        //Debug.Log(content);
        //将json变成物体
        JsonData json = JsonMapper.ToObject(questions);
        //通过键值对获得question数组
        JsonData question = json["question"];

        //Debug.Log(question);

        content.GetComponent<TEXDraw>().text = question[0].ToString();
    }

    public void ShowQuestion(){

    }

    public void ShowAnswer(){

    }

}
