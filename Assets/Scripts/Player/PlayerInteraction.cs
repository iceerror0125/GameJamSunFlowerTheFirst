using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private GameObject interactUI;
    private BoxCollider2D col;
    private Player player;
    
    private bool canInteract;

    private void OnEnable()
    {
        Observer.Instance.Subscribe(MessageType.InteractPressed, OnInteractChecking);
    }

    private void OnDisable()
    {
        Observer.Instance.UnSubscribe(MessageType.InteractPressed, OnInteractChecking);
    }

    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
        player = PlayerManager.Instance.Player;
    }

    private void OnInteractChecking(Message msg)
    {
        if (!canInteract)
            return;
        
        Interact();
    }

    private void Interact()
    {
        player.InteractWithItem();
    }
    

    private void ShowUI(bool isShow)
    {
        canInteract = isShow;
        interactUI.SetActive(isShow);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsInteractableItem(other))
        {
            AddItem(other);
            ShowUI(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (IsInteractableItem(other))
        {
            RemoveItem();
            ShowUI(false);
        }
    }

    private void AddItem(Collider2D other)
    {
        BaseItem item = other.GetComponent<BaseItem>();
        if (item == null)
            return;
        player.AddInteractableItem(item);
    }

    private void RemoveItem()
    {
        player.RemoveInteractableItem();
    }

    private bool IsInteractableItem(Collider2D other)
    {
        return other.CompareTag(TagConstant.InteractableItem);
    }
}
