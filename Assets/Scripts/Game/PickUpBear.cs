using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBear : PickUp
{
    CollectiblesController cc;


    private void Awake()
    {
        cc = FindObjectOfType<CollectiblesController>();
    }
    protected override void Loot()
    {
        if (isPickable && Input.GetMouseButtonDown(0))
        {
            //ui.addCandy(lootAmount);
            cc.PickUpBear(lootAmount);           
            sm.PlaySound("kidnap");
            Destroy(this.gameObject);
        }
    }
}
