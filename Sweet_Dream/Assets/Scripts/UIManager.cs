using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public GameObject inventory_panel;

    public Dictionary<string, Inventory_UI> inventoryui_by_name = new Dictionary<string, Inventory_UI>();

    public List<Inventory_UI> inventory_UIs;

    public static Slot_UI dragged_slot;
    public static Image dragged_icon;

    private void Awake()
    {
        Init();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventoryUI();
        }
    }

    public void ToggleInventoryUI()
    {
        if (inventory_panel != null)
        {
            if (inventory_panel.activeSelf)
            {
                //������˾͹ر�
                inventory_panel.SetActive(false);

            }
            else
            {
                //���û�򿪾ʹ�
                RefreshInventoryUI("Backpack");
                inventory_panel.SetActive(true);
            }
        }
    }

    public void RefreshInventoryUI(string inventory_name)
    {
        if (inventoryui_by_name.ContainsKey(inventory_name))
        {
            inventoryui_by_name[inventory_name].Refresh();
        }
    }

    public void RefreshAll()
    {
        foreach (KeyValuePair<string, Inventory_UI> keyValuePair in inventoryui_by_name)
        {
            keyValuePair.Value.Refresh();
        }
    }


    public Inventory_UI GetInventory_UIByName(string inventory_name)
    {
        if (inventoryui_by_name.ContainsKey(inventory_name))
        {
            return inventoryui_by_name[inventory_name];
        }
        Debug.Log("There is no inventory_ui for " + inventory_name);
        return null;
    }

    void Init()
    {
        foreach (Inventory_UI ui in inventory_UIs)
        {
            if (!inventoryui_by_name.ContainsKey(ui.inventory_name))
            {
                inventoryui_by_name.Add(ui.inventory_name, ui);
            }
        }
    }
}
