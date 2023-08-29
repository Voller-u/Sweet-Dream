using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Dictionary<string, Inventory> inventory_by_name = new Dictionary<string, Inventory>();

    [Header("Backpack")]
    public Inventory backpack;
    public int backpack_slotcount;

    [Header("Toolbar")]
    public Inventory toolbar;
    public int toolbar_slotcount;

    private void Awake()
    {
        backpack = new Inventory(backpack_slotcount);
        toolbar = new Inventory(toolbar_slotcount);

        inventory_by_name.Add("Backpack", backpack);
        inventory_by_name.Add("Toolbar", toolbar);
    }

    public void Add(string inventory_name,Item item)
    {
        if (inventory_by_name.ContainsKey(inventory_name))
        {
            inventory_by_name[inventory_name].Add(item);
        }
    }

    public Inventory GetInventoryByName(string inventory_name)
    {
        if (inventory_by_name.ContainsKey(inventory_name))
        {
            return inventory_by_name[inventory_name];
        }
        return null;
    }

}
