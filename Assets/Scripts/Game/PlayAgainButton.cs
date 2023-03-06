using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class PlayAgainButton : MonoBehaviour
{
    CheckpointManager checkpointManager;
    [SerializeField] AudioSource victoryMusic;
    [SerializeField] AudioSource secretMusic;
    [SerializeField] TMP_Text TeddyText;
    [SerializeField] GameObject MusicText;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        checkpointManager = FindObjectOfType<CheckpointManager>();
        if (checkpointManager != null)
        {
            
            if (checkpointManager.GetComponent<CollectiblesController>() != null)
            {
                TeddyText.text = "Juntaste " + checkpointManager.GetComponent<CollectiblesController>().Bears.ToString() +"/8 ositos";
                if (checkpointManager.GetComponent<CollectiblesController>().HasGotAllBears)
                {
                    MusicText.SetActive(true);
                    secretMusic.Play();
                }
                else victoryMusic.Play();
            }
            else victoryMusic.Play();
        }
        else victoryMusic.Play();

    }
    public void PlayAgain()
    {
        if (SceneManager.GetActiveScene().name == "DefeatScreen") SceneManager.LoadScene(checkpointManager.PreviousSceneName);
        else SceneManager.LoadScene("Intro");
    }
}
