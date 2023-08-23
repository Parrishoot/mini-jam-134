using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItem : ConsumableItem
{
    protected override void Activate()
    {
        AmmoController.GetInstance().Reload();
    }

    protected void Start()
    {
        transform.localScale = Vector3.zero;
    } 
}
