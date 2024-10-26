using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataService : SingletonMono<ItemDataService>
{
    [SerializeField] private ItemDataPool dataPool;
    
    public ItemData FindItemById(int id)
    {
        foreach (var item in dataPool.Pool)
        {
            if (item.id == id)
            {
                return item;
            }
        }

        return null;
    }
}
