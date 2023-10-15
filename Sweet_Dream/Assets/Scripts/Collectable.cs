using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public Item property;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        //被玩家拾取
        if(other.CompareTag("Player")){
            Inventory.instance.AddItem(property);
            Destroy(gameObject);
        }
    }
}
