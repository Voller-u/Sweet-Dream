using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [System.Serializable]
    public class Slot
    {
        public CollectableType type;
        public int count;//个数
        public int maxAllowed;//最大数量
        public Sprite icon;//物品的图标
        public Slot()
        {
            type = CollectableType.NONE;
            count = 0;
            maxAllowed = int.MaxValue;
        }
        public void Add_Item(Collectable item)
        {
            this.type = item.type;
            this.icon = item.icon;
            count++;
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

    public void Add(Collectable item_to_add)
    {
        foreach(Slot slot in slots)
        {
            if(slot.type == item_to_add.type)
            {
                slot.Add_Item(item_to_add);
                return;
            }
        }
        foreach(Slot slot in slots)
        {
            if(slot.type == CollectableType.NONE)
            {
                slot.Add_Item(item_to_add);
                break;
            }
        }
    }
}
