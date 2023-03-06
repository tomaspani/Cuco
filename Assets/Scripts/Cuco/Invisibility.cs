using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : MonoBehaviour
{
    [Header("References")]
    PlayerController _pc;
    Energy energy;


    [SerializeField] GameObject Cuco;
    [SerializeField] Renderer hand1;
    [SerializeField] Renderer hand2;
    [SerializeField] Material transparentMat;
    [SerializeField] Material opaqueMat;
    [SerializeField] ParticleSystem SmokeParticles;
    [SerializeField] GameObject _teddyBear;
    [SerializeField] Transform _teddyBearspawnPosition;

    [Header("Values")]

    public float InvisibilityDuration;
    [SerializeField] int EnergyCost;
    bool isInvisible;
    [SerializeField] bool IsInvisibilityUnlocked;
    

    private void Start()
    {
        energy = GetComponent<Energy>();
        _pc = GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (Input.GetKeyDown("f") && (energy.CurrentEnergy - EnergyCost >= 0) && !isInvisible && IsInvisibilityUnlocked)
        {
            StartCoroutine(Hide());
        }
    }

    IEnumerator Hide ()
    {
        Cuco.layer = LayerMask.NameToLayer("CucoInvisible");
        GameObject NewBear = Instantiate(_teddyBear, _teddyBearspawnPosition.position, Cuco.transform.rotation);
        SmokeParticles.Play();
        Debug.Log(Cuco.layer);
        hand1.material = transparentMat;
        hand2.material = transparentMat;
        isInvisible = true;
        energy.ChangeEnergy(-EnergyCost);
        yield return new WaitForSeconds(InvisibilityDuration);
        BecomeVisible();

    }

    private void BecomeVisible ()
    {
        Cuco.layer = LayerMask.NameToLayer("Cuco"); ;
        hand1.material = opaqueMat;
        hand2.material = opaqueMat;
        isInvisible = false;
    }

    public void UnlockInvisibility ()
    {
        IsInvisibilityUnlocked = true;
    }

}
