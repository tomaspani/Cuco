using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpFakeBear : PickUpBear
{
    PlayerController pc;
    [SerializeField] float susAmount;

    private void Awake()
    {
        pc = FindObjectOfType<PlayerController>();
    }
    protected override void Loot()
    {
        if (isPickable && Input.GetMouseButtonDown(0))
        {
            //ui.addCandy(lootAmount);           
            sm.PlaySound("squeak");
            sm.PlaySound("alert");
            pc.addKidSuspicion(susAmount);
        }
    }
}
