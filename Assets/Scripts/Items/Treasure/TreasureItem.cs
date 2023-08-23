using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureItem : ConsumableItem
{
    protected override void Activate()
    {
        GameManager gameManager = GameManager.GetInstance();

        gameManager.CollectChest();
        gameManager.EndRound();
    }
}
