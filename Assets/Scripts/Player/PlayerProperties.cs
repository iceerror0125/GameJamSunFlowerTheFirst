using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties
{
    public int moveSpeed { get; private set; }
    public void Init()
    {
        moveSpeed = 5;
    }
}
