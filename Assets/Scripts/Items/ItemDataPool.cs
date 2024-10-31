using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
    public Sprite secondSprite;
    public string description;
    public string description2;
    public string detail;
    public string detail2;
    public bool canCollect;
    public bool isColored;
    public RiddeType riddleType = RiddeType.None;
}
