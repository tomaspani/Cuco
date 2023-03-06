using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] GameObject[] Lights;
    float LightsLength = 7f;
    [SerializeField] float timer;
    GameObject cuco;
    [SerializeField] Text feedback;
    [SerializeField] bool Lightsoff;

    private void Awake()
    {
        cuco = GameObject.FindGameObjectWithTag("Player");
        feedback = GameObject.FindGameObjectWithTag("feedbackText").GetComponent<Text>();
    }
    void TurnOffLights ()
    {
        timer = LightsLength;
        Lightsoff = true;
        cuco.layer = LayerMask.NameToLayer("CucoInvisible");
        foreach (GameObject L in Lights)
        {
            L.SetActive(false);
            
        }
    }



   
    void TurnOnLights()
    {

        foreach (GameObject L in Lights)
        {
            L.SetActive(true);
        }
        cuco.layer = LayerMask.NameToLayer("Cuco");
        Lightsoff = false;
      
    }

    private void OnTriggerStay(Collider other)
    {
        feedback.text = "Press Left click to turn off";
        Debug.Log("podes tocar esta");

        if (Input.GetMouseButtonDown(0))
        {
            TurnOffLights();
            

        }
    }

    private void OnTriggerExit(Collider other)
    {
        feedback.text = "";
    }

    private void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && Lightsoff) TurnOnLights();
        if (timer >= 0)cuco.layer = LayerMask.NameToLayer("CucoInvisible");
    }

}
