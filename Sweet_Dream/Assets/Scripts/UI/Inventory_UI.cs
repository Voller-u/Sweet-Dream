using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory_UI : MonoBehaviour
{
    public GameObject inventory_panel;

    public Player player;

    public List<Slot_UI> slots = new List<Slot_UI>();

    [SerializeField] private Canvas canvas;

    private Slot_UI dragged_slot; //dragged_slot: ����ק��slot

    private Image dragged_icon;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }

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
                if(player.inventory.slots[i].item_name != "")
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

    public void Remove(bool drag_single)
    {
        Item item_to_drop = GameManager.instance.itemManager.GetItemByName(
            player.inventory.slots[dragged_slot.slot_id].item_name);
        if(item_to_drop != null)
        {
            if (!drag_single)
            {
                player.DropItem(item_to_drop,
                player.inventory.slots[dragged_slot.slot_id].count);
                player.inventory.Remove(dragged_slot.slot_id,
                    player.inventory.slots[dragged_slot.slot_id].count);
            }
            else
            {
                player.DropItem(item_to_drop);
                player.inventory.Remove(dragged_slot.slot_id);
            }
            Refresh();
        }
        dragged_slot = null;
    }

    public void SlotBeginDrag(Slot_UI slot)
    {
        dragged_slot = slot;
        
        dragged_icon = Instantiate(slot.itemicon);

        dragged_icon.transform.SetParent(canvas.transform);

        dragged_icon.raycastTarget = false; //��ֹ�����ڵ�
        
        dragged_icon.rectTransform.sizeDelta = new Vector2(80, 80);

        MoveToMousePosition(dragged_icon.gameObject);

        Debug.Log("Start Drag:" + dragged_slot.name);
    }

    public void SlotDrag()
    {
        MoveToMousePosition(dragged_icon.gameObject);

        Debug.Log("Dragging:" + dragged_slot.name);
    }
    public void SlotEndDrag()
    {
        Destroy(dragged_icon.gameObject);
        dragged_icon = null;

        Debug.Log("Done Dragging:" + dragged_slot.name);
    }

    public void SlotDrop(Slot_UI slot)
    {
        Debug.Log("Dropped:" + dragged_slot.name + "on " + slot.name);
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
}
