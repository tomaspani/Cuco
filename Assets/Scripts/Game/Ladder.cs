using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ladder : MonoBehaviour
{
    [SerializeField] Text feedback;


    private void OnTriggerEnter(Collider other)
    {
        feedback.text = "Press Left click to exit the tunnels";
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButton(0)) SceneManager.LoadScene("Final Level_Camping Zone");
    }

    private void OnTriggerExit(Collider other)
    {
        feedback.text = "";
    }
}
