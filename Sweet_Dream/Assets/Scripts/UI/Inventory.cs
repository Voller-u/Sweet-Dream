using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[SerializeField]
public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public GameObject inventory;
    private bool inventoryOn;
    //背包中的物品类
    [SerializeField]
    
    public List<Item> itemList = new List<Item>();
    private void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ControlInventory();
    }

    public void ControlInventory(){
        if(Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.I)){
            inventoryOn = !inventoryOn;
            inventory.SetActive(inventoryOn);
        }
    }

    //Contain函数：判断物体是否已存在，存在则返回list中的索引，否则返回-1
    public int Contain(Item item){
        for(int i=0;i < itemList.Count ;i++){
            //如果背包中已经有了该种物体
            if(itemList[i].itemName.CompareTo(item.itemName)==0){
                return i;
            }
        }
        return -1;
    }

    public void AddItem(Item item){
        int index = Inventory.instance.Contain(item);
        if(index > -1){
            Inventory.instance.itemList[index].itemNum++;
        }
        else{
            //新的ITEM
            Inventory.instance.itemList.Add(null);
            Inventory.instance.itemList[Inventory.instance.itemList.Count-1] = new Item(item);
        }
    }
    
}
