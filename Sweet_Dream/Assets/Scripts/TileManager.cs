using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap interactable_map;

    [SerializeField] private Tile hidden_interactable_tile;

    [SerializeField] private Tile interacted_tile;
    void Start()
    {
        //������Ƭ��ͼ
        foreach (var position in interactable_map.cellBounds.allPositionsWithin)
        {
            TileBase tile = interactable_map.GetTile(position);

            //THENAME:�һ�û���Ҫ��Ҫ����һ��

            if (tile != null && tile.name == "THENAME")
            {
                //��ָ��λ�õ���Ƭ���ó�ָ������Ƭ
                interactable_map.SetTile(position, hidden_interactable_tile);
            }

        }
    }

    public bool IsInteractable(Vector3Int position)
    {
        TileBase tile = interactable_map.GetTile(position);

        if (tile != null)
        {
            if (tile.name == "Interactable")
            {
                return true;
            }
        }

        return false;
    }

    public void SetInteracted(Vector3Int position)
    {
        interactable_map.SetTile(position, interacted_tile);
    }
}
