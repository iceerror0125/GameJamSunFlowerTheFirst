using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class YOrderController : MonoBehaviour
{
    [SerializeField] private int customOffset = 1;
    private SpriteRenderer sprite;
    private Player player;
    private void Start() {
        player = PlayerManager.Instance.Player;
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (this.transform.position.y < player.transform.position.y)
        {
            ChangeOrderLayer(1);
        }
        else
        {
            ChangeOrderLayer(-1);
        }
    }

    private void ChangeOrderLayer(int offset)
    {
        sprite.sortingOrder = offset * 10;
        if (customOffset < 0 && sprite.sortingOrder > 0)
        {
            sprite.sortingOrder = 10 * customOffset;
        }
    }
}
