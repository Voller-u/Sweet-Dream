using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEditor;
using System.Linq.Expressions;

[SerializeField]
public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public GameObject inventory;
    public GameObject slots;
    public GameObject slotPrefab;//格子的预制件
    private bool inventoryOn;

    public Sprite EmptySlot;
    //背包中的物品类
    
    [SerializeField]
    private List<Item> itemList;
    private void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        itemList = new List<Item>(35);
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        CallInventory();
    }

    public void CallInventory(){
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
            Inventory.instance.itemList.Add(item);
        }
        RefreshInventory();
    }

    public void RemoveItem(int index,int num){
        Inventory.instance.itemList[index].itemNum -= num;
        RefreshInventory();
    }

    //初始化35个slot
    private void Init(){
        for(int i=0;i<35;i++){
            GameObject slot = Instantiate(slotPrefab);
            slot.transform.SetParent(slots.transform);
            slot.transform.localScale = new Vector3(1f,1f,1f);
            slot.GetComponent<Slot>().slotID = i;
        }
    }

    public void RefreshInventory(){
        //去除用完了的物品
        for(int i=0;i<itemList.Count;){
            if(itemList[i].itemNum <=0){
                itemList.RemoveAt(i);
            }
            else i++;
        }
        //重新排布  
        for(int i=0;i<35;i++){
            if(i<itemList.Count) slots.transform.GetChild(i).gameObject.GetComponent<Slot>().SetSlot(itemList[i]);
            else{
                Item item = new Item();
                item.itemImage = EmptySlot;
                slots.transform.GetChild(i).gameObject.GetComponent<Slot>().SetSlot(item);
            }
        }
    }
}
