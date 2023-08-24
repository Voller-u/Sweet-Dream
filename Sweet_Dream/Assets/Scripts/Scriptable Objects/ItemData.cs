using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item Data",menuName ="Item Data",order =50)]
public class ItemData : ScriptableObject
{
    public string item_name = "ItemName";
    public Sprite icon;
}
