using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEntities : MonoBehaviour
{
    public float radius;
    [SerializeField] bool IsCucoVisionUnlocked;
    [SerializeField] float CDLength;
    [SerializeField] int energyCost;
    [SerializeField] bool CanUseCucoVision;
    Energy energy;

    public LayerMask targetMask;

    private void Awake()
    {
        energy = GetComponent<Energy>();
    }

    private void Start()
    {
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.01f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] FindEntitiesCheck = Physics.OverlapSphere(transform.position, radius, targetMask);

       
        if (FindEntitiesCheck.Length != 0)
        {

            if (Input.GetKeyDown(KeyCode.Tab) && IsCucoVisionUnlocked && energy.CurrentEnergy - energyCost >= 0 && CanUseCucoVision)
            {
                foreach (Collider entity in FindEntitiesCheck)
                {
                    checkEntity(entity).ActivateSeeThrough();
                    //checkEntity(entity).isActivated = false;
                }
                energy.ChangeEnergy(-energyCost);
                CanUseCucoVision = false;
                Invoke(nameof(ResetCucoVision), CDLength);
            }
            
        }
       
    }


    private SeeThrough checkEntity(Collider n)
    {
        if (n.GetComponent<SeeThrough>() != null)
        {
            return n.GetComponent<SeeThrough>();
        }
        else
        {
            return n.GetComponentInChildren<SeeThrough>();
        }
    }


    public void UnlockCucoVision ()
    {
        IsCucoVisionUnlocked = true;
    }

    void ResetCucoVision ()
    {
        CanUseCucoVision = true;
    }
}
