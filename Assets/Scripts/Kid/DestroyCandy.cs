using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCandy : MonoBehaviour
{
    [SerializeField] float DestroyCD;


    private void OnTriggerStay(Collider other)
    {
        Debug.Log("entre al trigger");
        DestroyCD -= Time.deltaTime;

    }
    void Update()
    {
       if (DestroyCD <= 0)
        {
            Destroy(gameObject);
        } 
    }
}
