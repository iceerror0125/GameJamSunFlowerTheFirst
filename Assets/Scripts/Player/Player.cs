using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Component

    public Rigidbody2D rb { get; private set; }
    private Animator anim;

    #endregion

    public PlayerStateHandler stateHandler { get; private set; }
    public PlayerProperties properties { get; private set; }
    private BaseItem interactableItem;

    private void Start()
    {
        GetComponent();
        Init();
    }

    private void GetComponent()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();   
    }

    private void Init()
    {
        properties = new PlayerProperties();
        stateHandler = new PlayerStateHandler();
        
        properties.Init();  
        stateHandler.Init(this, anim);
    }

    private void Update()
    {
        stateHandler.Update();
    }

    private Dictionary<MoveDirection, Vector2> directions = new Dictionary<MoveDirection, Vector2>()
    {
        { MoveDirection.Right, Vector2.right },
        { MoveDirection.Left, Vector2.left },
        { MoveDirection.Up, Vector2.up },
        { MoveDirection.Down, Vector2.down },
        
    };
    public void Move(MoveDirection direction)
    {
        Vector2 directionVector = directions[direction];
        rb.velocity = directionVector * properties.moveSpeed;
        ChangeDirection(directionVector);
    }

    private void ChangeDirection(Vector2 directionVector)
    {
        float currentX = anim.transform.localScale.x;
        float scaleX = directionVector.x != 0 ? directionVector.x *-1: currentX;
        if (!Mathf.Approximately(currentX, scaleX))
         anim.transform.localScale = new Vector3(scaleX, 1, 1);
    }

    public void Idle()
    {
        rb.velocity = Vector2.zero;
    }

    public void AddInteractableItem(BaseItem item)
    {
        this.interactableItem = item;
    }

    public void RemoveInteractableItem()
    {
        interactableItem = null;
    }

    public void InteractWithItem()
    {
        if (interactableItem != null)
        {
            interactableItem.Interact();
        }
    }
}