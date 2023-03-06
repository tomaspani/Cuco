using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private PlayerController player;
    private Consume consumeeeee;
    public int KidCount;

    public GameObject fenceClosed;
    public GameObject fenceOpen;
    [SerializeField] Text feedback;
    bool HasWon;
    CheckpointManager checkpointManager;
    [SerializeField] GameObject ladder;
    bool hasTextBeenShown;

    private void Awake()
    {
        KidCount = GameObject.FindGameObjectsWithTag("Kid").Length;
        consumeeeee = FindObjectOfType<Consume>();
        QualitySettings.pixelLightCount = 100;
        checkpointManager = FindObjectOfType<CheckpointManager>();
    }
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        StartCoroutine(EraseText());
       
    }

    private void Update()
    {
        if (player.suspicion >= 100f)
        {
            Debug.Log("Perdiste lince");
            player.kidnappedKids = 0;
            checkpointManager.SaveScene();
            SceneManager.LoadScene("DefeatScreen");
        }

        if (consumeeeee.totalConsumedKids >= KidCount && !HasWon)
        {
            Debug.Log("Ganaste pro");
            //SceneManager.LoadScene("VictoryScreen");
            if (SceneManager.GetActiveScene().name == "Camping Zone")
            {
                fenceClosed.SetActive(false);
                fenceOpen.SetActive(true);
                StartCoroutine(VictoryText());
                
            }

            if (SceneManager.GetActiveScene().name == "Tunnels")
            {
                ladder.GetComponent<BoxCollider>().enabled = true;
                feedback.text = "Leave the tunnels the same way you entered them";
                HasWon = true;
                StartCoroutine(EraseText());
            }


        }


    }

    IEnumerator VictoryText()
    {
        yield return new WaitForSeconds(1);
        feedback.text = "The fence behind the house has opened";
        HasWon = true;
        StartCoroutine(EraseText());
    }

    IEnumerator EraseText ()
    {
        yield return new WaitForSeconds(10);
        feedback.text = "";
    }
}