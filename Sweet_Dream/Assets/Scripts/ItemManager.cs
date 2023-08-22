using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //collectable_items: �ռ������飬���ڴ��Ԥ�Ƽ�
    public Collectable[] collectable_items;

    //dictionary_items_dict: �ֵ䣬ͨ�����������ӦԤ�Ƽ�
    private Dictionary<CollectableType, Collectable> collectable_items_dict = new Dictionary<CollectableType, Collectable>();

    private void Awake()
    {
        foreach(Collectable item in collectable_items)
        {
            AddItem(item);
        }
    }

    private void AddItem(Collectable item)
    {
        if (!collectable_items_dict.ContainsKey(item.type))
        {
            collectable_items_dict.Add(item.type,item);
        }
    }

    public Collectable GetItemByType(CollectableType type)
    {
        if (collectable_items_dict.ContainsKey(type))
        {
            return collectable_items_dict[type];
        }
        return null;
    }
}
