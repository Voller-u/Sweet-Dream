using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory_UI : MonoBehaviour
{

    public string inventory_name;

    public List<Slot_UI> slots = new List<Slot_UI>();

    [SerializeField] private Canvas canvas;

    private Inventory inventory;

    private void Start()
    {
        inventory = GameManager.instance.player.inventory.GetInventoryByName(inventory_name);
        SetUpSlots();
        Refresh();
    }

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    // Update is called once per frame
    
   

    //Refresh:更新背包
    public void Refresh()
    {
        //如果slots的数量相等
        if(slots.Count == inventory.slots.Count)
        {
            for(int i = 0; i < slots.Count; i++)
            {
                //如果这个格子有东西
                if(inventory.slots[i].item_name != "")
                {
                    slots[i].SetItem(inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
        }
    }

    public void Remove()
    {
        Item item_to_drop = GameManager.instance.itemManager.GetItemByName(
            inventory.slots[UIManager.dragged_slot.slot_id].item_name);
        if(item_to_drop != null)
        {
            GameManager.instance.player.DropItem(item_to_drop,
                inventory.slots[UIManager.dragged_slot.slot_id].count);
            inventory.Remove(UIManager.dragged_slot.slot_id,
                inventory.slots[UIManager.dragged_slot.slot_id].count);
            Refresh();
        }
        UIManager.dragged_slot = null;
    }
    public void Remove(int slot_id)
    {
        Item item_to_drop = GameManager.instance.itemManager.GetItemByName(
            inventory.slots[slot_id].item_name);
        if (item_to_drop != null)
        {
            GameManager.instance.player.DropItem(item_to_drop);
            inventory.Remove(slot_id);
            Refresh();
        }
    }

    public void SlotBeginDrag(Slot_UI slot)
    {
        UIManager.dragged_slot = slot;

        UIManager.dragged_icon = Instantiate(slot.itemicon);

        UIManager.dragged_icon.transform.SetParent(canvas.transform);

        UIManager.dragged_icon.raycastTarget = false; //防止射线遮挡

        UIManager.dragged_icon.rectTransform.sizeDelta = new Vector2(80, 80);

        MoveToMousePosition(UIManager.dragged_icon.gameObject);

        Debug.Log("Start Drag:" + UIManager.dragged_slot.name);
    }

    public void SlotDrag()
    {
        MoveToMousePosition(UIManager.dragged_icon.gameObject);

        Debug.Log("Dragging:" + UIManager.dragged_slot.name);
    }
    public void SlotEndDrag()
    {
        Destroy(UIManager.dragged_icon.gameObject);
        UIManager.dragged_icon = null;

        Debug.Log("Done Dragging:" + UIManager.dragged_slot.name);
    }

    public void SlotDrop(Slot_UI slot)
    {
        UIManager.dragged_slot.inventory.MoveSlot(UIManager.dragged_slot.slot_id,
            slot.slot_id,slot.inventory,
            UIManager.dragged_slot.inventory.slots[UIManager.dragged_slot.slot_id].count);
        GameManager.instance.uiManager.RefreshAll();
    }

    private void MoveToMousePosition(GameObject to_move)
    {
        if(canvas != null)
        {
            Vector2 position;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform, Input.mousePosition, null, out position);

            to_move.transform.position = canvas.transform.TransformPoint(position);
        }
    }

    void SetUpSlots()
    {
        int count = 0;
        foreach(Slot_UI slot in slots)
        {
            slot.slot_id = count;
            count++;
            slot.inventory = inventory;
        }
    }
}
