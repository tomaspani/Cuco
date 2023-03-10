using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private OutlineScript outline;
    private ThrowObject player;
    protected bool isPickable;
    protected SoundManager sm;
    private UIManager ui;
    //private GameObject loot;


    public int lootAmount;

    private void Start()
    {
        outline = GetComponent<OutlineScript>();
        sm = FindObjectOfType<SoundManager>();
        ui = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        Loot();
    }

    protected virtual void Loot()
    {
        if (isPickable && Input.GetMouseButtonDown(0))
        {
            //ui.addCandy(lootAmount);
            player.Candies += lootAmount;
            
            sm.PlaySound("kidnap");
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<ThrowObject>();
            isPickable = true;
            outline.Enable();
            Debug.Log("podes agarrarme");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPickable = false;
            outline.Disable();
            Debug.Log("mefuis");

        }
    }
}