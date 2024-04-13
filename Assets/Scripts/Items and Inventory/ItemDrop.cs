using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private int possibleItemDrop;
    [SerializeField] private ItemData[] possibleDrop;
    private List<ItemData> dropList = new List<ItemData>();

    [SerializeField] private GameObject dropPrefab;

    //public virtual void GenerateDrop()
    //{
    //    if (possibleDrop.Length <= 0)
    //    {
    //        return;
    //    }

    //    for (int i = 0; i < possibleDrop.Length; ++i)
    //    {
    //        if (Random.Range(0, 100) < possibleDrop[i].dropChance)
    //        {
    //            dropList.Add(possibleDrop[i]);
    //        }
    //    }

    //    for (int i = 0; i < possibleItemDrop; ++i)
    //    {
    //        if (dropList.Count <= 0)
    //        {
    //            return;
    //        }

    //        ItemData randomItem = dropList[Random.Range(0, dropList.Count)];

    //        dropList.Remove(randomItem);
    //        DropItem(randomItem);
    //    }
    //}

    public virtual void GenerateDrop()
    {
        if (possibleDrop.Length <= 0)
        {
            return;
        }

        for (int i = 0; i < possibleDrop.Length; ++i)
        {
            if (Random.Range(0, 100) < possibleDrop[i].dropChance)
            {
                dropList.Add(possibleDrop[i]);
            }
        }

        foreach (var dropItem in dropList.ToArray())
        {
            if (possibleItemDrop <= 0)
                break;

            DropItem(dropItem);
            dropList.Remove(dropItem);
            possibleItemDrop--;
        }
    }

    protected void DropItem(ItemData _itemData)
    {
        GameObject newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);

        Vector2 randomVelocity = new Vector2(Random.Range(-5, 6), Random.Range(12, 16));

        newDrop.GetComponent<ItemObject>().SetupItem(_itemData, randomVelocity);
    }
}
