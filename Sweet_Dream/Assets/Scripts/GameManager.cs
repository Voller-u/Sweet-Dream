using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public ItemManager itemManager;
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        //��GameManagerһֱ���ڣ�������Ϊ�����л�����ʧ
        DontDestroyOnLoad(this.gameObject);

        itemManager = GetComponent<ItemManager>();
    }
}
