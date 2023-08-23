using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenTreasureOccupant : DestructableOccupant
{
    private GameObject itemPrefab;

    public void SetItemPrefab(GameObject itemPrefab) {
        this.itemPrefab = itemPrefab;
    }

    protected override void DoDestroy()
    {
        Instantiate(itemPrefab, transform.position, Quaternion.identity).GetComponent<Item>().PickUp();
        base.DoDestroy();
    }
}
