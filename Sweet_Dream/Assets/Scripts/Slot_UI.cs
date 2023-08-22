using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Slot_UI : MonoBehaviour
{
    public Image itemicon;//��Ʒ��ͼ��
    public TextMeshProUGUI quantity_text;//��Ʒ������

    //������Ʒ
    public void SetItem(Inventory.Slot slot)
    {
        if(slot != null)
        {
            itemicon.sprite = slot.icon;
            itemicon.color = new Color(1, 1, 1, 1);
            quantity_text.text = slot.count.ToString();

        }
    }
    //����Ϊ��ʱ
    public void SetEmpty()
    {
        itemicon.sprite = null;
        itemicon.color = new Color(1, 1, 1, 0);//����͸��
        quantity_text.text = "";
    }
}
