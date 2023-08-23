using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public ItemManager itemManager;

    public TileManager tileManager;
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

        //让GameManager一直存在，不会因为场景切换而消失
        DontDestroyOnLoad(this.gameObject);

        itemManager = GetComponent<ItemManager>();

        tileManager = GetComponent<TileManager>();
    }
}
