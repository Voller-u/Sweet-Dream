using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private static TimeManager  instance;
    [SerializeField]
    private int time;//从"2023-10-1"到现在经过的秒数
    private int lastTime;//上次退出的时候的时间
    private void Awake() {
        instance = this;
    }

    private void Start() {
        StartCoroutine(TimeIncrease());
    }

    IEnumerator TimeIncrease(){
        while(true){
            //获得当前从"2023-10-1"经过的秒数
            time = (int)(DateTime.Now - DateTime.Parse("2023-10-1")).TotalSeconds;
            lastTime = time;
            yield return new WaitForSeconds(1);//停一秒
        }
    }

}
