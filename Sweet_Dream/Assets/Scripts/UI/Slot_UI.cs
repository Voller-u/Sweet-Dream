using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Slot_UI : MonoBehaviour
{
    public Image itemicon;//物品的图标
    public TextMeshProUGUI quantity_text;//物品的数量

    //设置物品
    public void SetItem(Inventory.Slot slot)
    {
        if(slot != null)
        {
            itemicon.sprite = slot.icon;
            itemicon.color = new Color(1, 1, 1, 1);
            quantity_text.text = slot.count.ToString();

        }
    }
    //格子为空时
    public void SetEmpty()
    {
        itemicon.sprite = null;
        itemicon.color = new Color(1, 1, 1, 0);//设置透明
        quantity_text.text = "";
    }
}
