using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BaseItem : MonoBehaviour, IInteractableItem
{
    [SerializeField] protected GameObject highlight;
    [SerializeField] protected int id;
    [SerializeField] private Material normalMaterial;
    protected SpriteRenderer spriteRenderer;
    protected Material NormalMaterial => normalMaterial;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void ShowInteractUI()
    {
        HighlightItem(true);
    }

    private void HighlightItem(bool isHighlight)
    {
        highlight.SetActive(isHighlight);
    }

    public virtual void Interact()
    {
       
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (IsPlayer(other))
        {
            ShowInteractUI();   
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
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
