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

        public bool IsEmpty
        {
            get
            {
                if (item_name == "" && count == 0)
                {
                    return true;
                }
                return false;
            }
        }

        public void AddItem(Item item)
        {
            this.item_name = item.data.item_name;
            this.icon = item.data.icon;
            count++;
        }
        public void AddItem(string item_name, Sprite icon, int max_allowed)
        {
            this.item_name = item_name;
            this.icon = icon;
            count++;
            this.maxAllowed = max_allowed;
        }

        public bool CanAddItem(string item_name)
        {
            if (this.item_name == item_name && count < maxAllowed)
            {
                return true;
            }
            return false;
        }

        public void RemoveItem()
        {
            if (count > 0)  //�ж��Ƿ���
            {
                count--;
                if (count == 0)  //���ɾ����û�˾�ɾ������
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
        for (int i = 0; i < numSlots; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
    }

    public void Add(Item item_to_add)
    {
        foreach (Slot slot in slots)
        {
            if (slot.item_name == item_to_add.data.item_name && slot.CanAddItem(item_to_add.data.item_name))
            {
                slot.AddItem(item_to_add);
                return;
            }
        }
        foreach (Slot slot in slots)
        {
            if (slot.item_name == "")
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

    public void Remove(int index, int num_to_remove)
    {
        if (slots[index].count >= num_to_remove)
        {
            for (int i = 0; i < num_to_remove; i++)
            {
                Remove(index);
            }
        }
    }

    public void MoveSlot(int from_index, int to_index, Inventory to_inventory, int num_to_move)
    {
        Slot from_slot = slots[from_index];
        Slot to_slot = to_inventory.slots[to_index];

        if (to_slot.IsEmpty || to_slot.CanAddItem(from_slot.item_name))
        {
            for (int i = 0; i < num_to_move; i++)
            {
                to_slot.AddItem(from_slot.item_name, from_slot.icon, from_slot.maxAllowed);
                from_slot.RemoveItem();
            }
        }
    }
}
