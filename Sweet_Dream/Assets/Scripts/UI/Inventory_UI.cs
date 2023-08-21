using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventory_panel;
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
            inventory_panel.SetActive(true);
        }
    }
}
