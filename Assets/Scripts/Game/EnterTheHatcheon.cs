using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterTheHatcheon : MonoBehaviour
{
    [SerializeField] Text feedback;
    private void OnTriggerStay(Collider other)
    {
        feedback.text = "Press Left click to enter the tunnels";
        if (Input.GetMouseButtonDown(0)) { SceneManager.LoadScene("Tunnels"); }
    }

    private void OnTriggerExit(Collider other)
    {
        feedback.text = "";
    }
}
