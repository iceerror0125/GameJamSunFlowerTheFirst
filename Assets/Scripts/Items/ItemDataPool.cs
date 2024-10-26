using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractableItemData", menuName = "Interactable Item Data")]
public class ItemDataPool : ScriptableObject
{
    [SerializeField] private List<ItemData> pool = new List<ItemData>();
    public List<ItemData> Pool => pool;
}

[Serializable]
public class ItemData
{
    public int id;
    public string name;
    public Sprite sprite;
    public string description;
    public string detail;
    public bool canCollect;
    public bool isColored;
}
