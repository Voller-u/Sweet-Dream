using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Slot : MonoBehaviour,IPointerClickHandler
{
    public Item item;
    //public Text itemNum;
    public TextMeshProUGUI itemNum;
    private Text itemDescription;

    public int slotID;

    private void Start() {
        itemDescription = transform.parent.parent.Find("Item_Description").gameObject.GetComponent<Text>();
    }
    public void SetSlot(Item item){
        this.item = item;
        GetComponent<Image>().sprite = item.itemImage;
        if(item.itemNum>0) itemNum.text = item.itemNum.ToString();//空格子不显示数字
        else itemNum.text = "";
    }

    public void ShowItemDescription(){
        itemDescription.text = item.itemDescription;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //左键点击
        if(eventData.button == PointerEventData.InputButton.Left){
            ShowItemDescription();
        }
        //右键点击
        else if(eventData.button == PointerEventData.InputButton.Right){
            //使用物品
            if(item.itemNum>0){
                Inventory.instance.RemoveItem(slotID,1);
            }
        }
    }
}
