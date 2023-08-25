using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [System.Serializable]
    public class Slot
    {
        public string item_name;
        public int count;//����
        public int maxAllowed;//�������
        public Sprite icon;//��Ʒ��ͼ��
        public Slot()
        {
            item_name = "";
            count = 0;
            maxAllowed = int.MaxValue;
        }
        public void AddItem(Item item)
        {
            this.item_name = item.data.item_name;
            this.icon = item.data.icon;
            count++;
        }

        public void RemoveItem()
        {
            if (count > 0)  //�ж��Ƿ���
            {
                count--;
                if(count == 0)  //���ɾ����û�˾�ɾ������
                {
                    this.item_name = "";
                    this.icon = null;
                }
            }
        }
    }

    public List<Slot> slots = new List<Slot>();

    public Inventory(int numSlots)
    {
        for(int i = 0; i < numSlots; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
    }

    public void Add(Item item_to_add)
    {
        foreach(Slot slot in slots)
        {
            if(slot.item_name == item_to_add.data.item_name)
            {
                slot.AddItem(item_to_add);
                return;
            }
        }
        foreach(Slot slot in slots)
        {
            if(slot.item_name == "")
            {
                slot.AddItem(item_to_add);
                break;
            }
        }
    }

    public void Remove(int index)
    {
        slots[index].RemoveItem();
    }
}
