using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitCamp : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) WinGame();
    }

    void WinGame()
    {
        SceneManager.LoadScene("VictoryScreen");
    }
}
