using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Plant : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("收获")]
    public float harvestTime;//收获需要的时间
    public float pastTime;//种植后经过的时间
    public float startTime;//种植开始的时间
    public float remainTime;//收获剩余的时间

    [HideInInspector]
    public int spriteNum;
    public bool isPlanted;

    public List<Sprite> sprites;
    public List<GameObject> plants;
    //public Text timeText;

    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        pastTime += Time.deltaTime;
        remainTime = Mathf.Clamp(harvestTime-pastTime,0,harvestTime);
        if(remainTime<=0) Mature();
        //timeText.text = remainTime.ToString();
    }

    public void Mature(){
        if(isPlanted){
            RefreshSprites();
            isPlanted = false;
        }
    }

    public void RefreshSprites(){
        spriteNum = 1 - spriteNum;
        foreach(var plant in plants){
            plant.GetComponent<SpriteRenderer>().sprite = sprites[spriteNum];
        }
    }

    public void Planted(){
        isPlanted = true;
    }

    public void Harvest(){
        RefreshSprites();
    }

    protected void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            ShowText(other.gameObject);
        }
    }
    
    protected void ShowText(GameObject player){
        TextMeshProUGUI plantText = player.GetComponent<Player>().plantText;
        GameObject plantTextPanel = player.GetComponent<Player>().textPanel;
        plantTextPanel.SetActive(true);
        Debug.Log(((int)remainTime%60).ToString());
        if(remainTime<=0){//如果已经成熟了
            plantText.text = "作物已经成熟~";
        }else{
            plantText.text = "距离作物成熟还剩："+((int)remainTime/60).ToString()+"分"+((int)remainTime%60).ToString()+"秒";
        }
    } 

}
