using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public GameObject controlsUI;
    public GameObject generalUI;


    public void Play()
    {
        PlayerPrefs.SetFloat("sensX", 200);
        PlayerPrefs.SetFloat("sensY", 200);
        SceneManager.LoadScene("Camping Zone");
    }

    public void HowToPlay()
    {
        controlsUI.SetActive(true);
        generalUI.SetActive(false);
    }

    public void Atras()
    {
        controlsUI.SetActive(false);
        generalUI.SetActive(true);
    }


    public void Exit()
    {
        Application.Quit();
    }
}
