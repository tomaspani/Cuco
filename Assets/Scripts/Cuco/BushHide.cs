using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushHide : MonoBehaviour
{
    private int hidden = 6;
    private int notHidden = 8;
    private PlayerController playerRef;
    [SerializeField] private Material[] mat;
    public GameObject bush;


    private void Start()
    {
        playerRef = FindObjectOfType<PlayerController>();
        //Debug.Log(playerRef);
    }

    private void Update()
    {
        if(playerRef.isHiddenBush == true)
        {
            bush.GetComponent<MeshRenderer>().material = mat[0];
        }
        else
            bush.GetComponent<MeshRenderer>().material = mat[1];
    }


    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(playerRef);
        if(other.gameObject.tag == "bush")
        {

            Transform bush = other.gameObject.transform;
            foreach (Transform child in bush)
            {
                foreach(Transform superChild in child)
                {
                    superChild.gameObject.GetComponent<MeshRenderer>().material = mat[1];
                }
               
            }

            playerRef.isHiddenBush = true;
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(playerRef);
        if (other.gameObject.tag == "bush")
        {
            Transform bush = other.gameObject.transform;
            foreach (Transform child in bush)
            {
                foreach (Transform superChild in child)
                {
                    superChild.gameObject.GetComponent<MeshRenderer>().material = mat[0];
                }
            }

            playerRef.isHiddenBush = false;
        }
        
    }
}
