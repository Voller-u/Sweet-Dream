using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
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

    public Item plant;

    [HideInInspector]
    public int spriteNum;
    public bool isPlanted;
    public bool isMature;
    public bool isShown;

    public List<Sprite> sprites;
    public List<GameObject> plants;
    //public Text timeText;

    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(isPlanted && !isMature){
            pastTime += Time.deltaTime;
            remainTime = Mathf.Clamp(harvestTime-pastTime,0,harvestTime);
        }
        if(remainTime < 0.05f) Mature();
        if(isShown) PlantOrHarvest();
    }

    public void Mature(){
        if(!isMature){
            RefreshSprites(1);
            plant.itemNum = 4;
            isMature = true;
            pastTime = 0;
        }
    }

    public void RefreshSprites(int index){
        Debug.Log(index);
        foreach(var plant in plants){
            plant.GetComponent<SpriteRenderer>().sprite = sprites[index];
        }
    }

    public void Planted(){
        isPlanted = true;
        pastTime = 0;
        plant.itemNum = 0;
    }

    public void Harvest(){
        Inventory.instance.AddItem(plant);
        plant.itemNum = 0;
        isMature = false;
        isPlanted = false;
        remainTime = harvestTime;
        RefreshSprites(0);
    }


    protected void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            ShowText(other.gameObject);
        }
    }

    protected void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            ShowText(other.gameObject);
        }
        
    }

    protected void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            CloseText(other.gameObject);
        }
    }

    protected void ShowText(GameObject player){
        TextMeshProUGUI plantText = player.GetComponent<Player>().plantText;
        GameObject plantTextPanel = player.GetComponent<Player>().textPanel;
        plantTextPanel.SetActive(true);
        isShown = true;
        if(isMature){//如果已经成熟了
            plantText.text = "作物已经成熟~";
        }else if(isPlanted){
            plantText.text = "距离作物成熟还剩："+((int)remainTime/60).ToString()+"分"+((int)Mathf.Ceil(remainTime%60)).ToString()+"秒";
        }else{
            //植物还没种下
            plantText.text = "";
        }
    }

    protected void CloseText(GameObject player){
        player.GetComponent<Player>().textPanel.SetActive(false);
        isShown = false;
    } 

    public void PlantOrHarvest(){
        if(Input.GetKeyDown(KeyCode.E)){
            if(isMature){
                Debug.Log("收获了");
                Harvest();
                
            }
            else if(!isPlanted){//如果还没种下
                Debug.Log("种下了");
                Planted();
            }
        }
        
    }
}
