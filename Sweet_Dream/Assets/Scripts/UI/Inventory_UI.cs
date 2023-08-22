using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventory_panel;

    public Player player;

    public List<Slot_UI> slots = new List<Slot_UI>();
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventory();
        }
    }
    public void ToggleInventory()
    {
        if (inventory_panel.activeSelf)
        {
            //������˾͹ر�
            inventory_panel.SetActive(false);
            
        }
        else
        {
            //���û�򿪾ʹ�
            Refresh();
            inventory_panel.SetActive(true);
        }
    }

    //Refresh:���±���
    void Refresh()
    {
        //���slots���������
        if(slots.Count == player.inventory.slots.Count)
        {
            for(int i = 0; i < slots.Count; i++)
            {
                //�����������ж���
                if(player.inventory.slots[i].type != CollectableType.NONE)
                {
                    slots[i].SetItem(player.inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
        }
    }

    public void Remove(int slot_id)
    {
        Collectable item_to_drop = GameManager.instance.itemManager.GetItemByType(
            player.inventory.slots[slot_id].type);
        if(item_to_drop != null)
        {
            player.DropItem(item_to_drop);
            player.inventory.Remove(slot_id);
            Refresh();
        }
        
    }
}
