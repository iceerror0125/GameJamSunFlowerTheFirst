using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryData", menuName = "Inventory Data")]
public class InventoryData : ScriptableObject
{
    public List<int> inventoryData = new();
}
