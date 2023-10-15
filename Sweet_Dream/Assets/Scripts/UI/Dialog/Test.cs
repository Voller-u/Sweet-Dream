using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using JetBrains.Annotations;
public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    private Text text;
    private Tweener tweener;
    void Start()
    {
        
        text = GetComponent<Text>();

        tweener =  text.DOText("啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊",20);
    }

    // Update is called once per frame
    void Update()
    {
        ShowFullText();
    }

    void ShowFullText(){
        if(Input.GetKeyDown(KeyCode.Space)){
            tweener.Complete();
        }
    }
    
    // 随机振动相机位置
    // transform.DOShakePosition(5,0.3f);

    // 指定时间输出文字
    // text.DOText("啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊",6);

    // 提前结束tween动画
    // tweener =  text.DOText("啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊",20);
    // tweener.Complete();
}
