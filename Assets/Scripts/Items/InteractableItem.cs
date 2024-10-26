using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractableItem
{
    [SerializeField] private GameObject highlight;
    [SerializeField] private int id;
    public void ShowInteractUI()
    {
        HighlightItem(true);
    }

    private void HighlightItem(bool isHighlight)
    {
        highlight.SetActive(isHighlight);
    }

    public void Interact()
    {
        Observer.Instance.Announce(new Message(MessageType.ShowItemDetail, id));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsPlayer(other))
        {
            ShowInteractUI();   
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (IsPlayer(other))
        {
            HighlightItem(false);
        }
    }
    private bool IsPlayer(Collider2D other)
    {
        return other.CompareTag(TagConstant.Player);
    }
}
