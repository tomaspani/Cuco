using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdultInBush : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            Debug.Log("entro");

            this.gameObject.layer = 13;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 11)
        {

            Debug.Log("sequeda");
            this.gameObject.layer = 13;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 11)
        {

            Debug.Log("sefue");
            this.gameObject.layer = 8;
        }
    }
}
