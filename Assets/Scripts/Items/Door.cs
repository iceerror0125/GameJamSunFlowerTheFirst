using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Door : BaseItem
{
    [SerializeField] private int requireId;
    [SerializeField] private BoxCollider2D doorCollider;
    private bool doorOpen = false;
    

    public override void Interact()
    {
        base.Interact();
        OnInteract();
    }
    

    private void OnInteract()
    {
        if (doorOpen)
            return;
        
        bool isExistRequiredItem = Inventory.Instance.IsContainRequiredId(requireId);
        if (isExistRequiredItem)
        {
            Inventory.Instance.RemoveItem(requireId);
            doorCollider.enabled = false;
            OpenDoor();
        }
        else
        {
            // todo: announce can't open
        }
    }
    
    private void OpenDoor()
    {
        doorOpen = true;
        transform.GetChild(0).DORotate(new Vector3(0, 80, 0), 0.5f);
    }
}
