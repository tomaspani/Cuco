using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BearTrap : MonoBehaviour
{
    [SerializeField] float trapDuration;
    PlayerMovement pm;
    [SerializeField] bool hasBeenUsed;
    AudioSource trapAudio;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] GameObject closedTrap;

    private void Awake()
    {
        trapAudio = GetComponent<AudioSource>();
        pm = FindObjectOfType<PlayerMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (!hasBeenUsed) StartCoroutine(TrapPlayer());

    }

    IEnumerator TrapPlayer ()
    {
        hasBeenUsed = true;
        trapAudio.Play();
        pm.ChangeSpeed(0);
        meshRenderer.enabled = false;
        closedTrap.SetActive(true);
        yield return new WaitForSeconds(trapDuration);
        pm.ChangeSpeed(pm.baseSpeed);        
    }

    private void OnTriggerExit(Collider other)
    {
        pm.ChangeSpeed(pm.baseSpeed);
    }
}
