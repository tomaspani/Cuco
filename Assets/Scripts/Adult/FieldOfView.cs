using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float susRadius;
    public float alertRadius;
    [Range(0,360)]
    public float angle;

    SoundManager soundManager;
    

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer, seesPlayer = false;

    bool isPlayerHidden;
    [Header("CameraShake")]
    CameraShake cs;
    

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        cs = GameObject.FindGameObjectWithTag("CameraHolder").GetComponent<CameraShake>();
        StartCoroutine(FOVRoutine());
    }


    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    

    private void FieldOfViewCheck()
    {
        Collider[] susRangeChecks = Physics.OverlapSphere(transform.position, susRadius, targetMask);

        if (susRangeChecks.Length != 0)
        {
            Transform susTarget = susRangeChecks[0].transform;
            Vector3 directionToTarget = (susTarget.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, susTarget.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask) && isPlayerHidden == false)
                {
                    canSeePlayer = true;
                    if (alertRadius < susRadius * 0.9f)
                    {
                        alertRadius += 35f * Time.deltaTime;
                    }
                    alertFieldOfViewCheck();
                }
                else
                {
                    canSeePlayer = false;
                    seesPlayer = false;
                    alertRadius = 2.5f;
                }
            }
            else
            {
                canSeePlayer = false;
                seesPlayer = false;
                alertRadius = 2.5f;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
            seesPlayer = false;
            alertRadius = 2.5f;
        }
 
    }


    private void alertFieldOfViewCheck()
    {
        Collider[] alertRangeChecks = Physics.OverlapSphere(transform.position, alertRadius, targetMask);
        if (alertRangeChecks.Length != 0)
        {
            Transform alertTarget = alertRangeChecks[0].transform;
            Vector3 directionToTarget = (alertTarget.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, alertTarget.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask) && isPlayerHidden == false )
                {
                    seesPlayer = true;
                }

                else
                    seesPlayer = false;
            }
            else
               seesPlayer = false;
        }
        else if (seesPlayer)
            seesPlayer = false;

    }


    public Vector3 LastSeenPosition()
    {
        Vector3 lastSeenPosition;
        bool checkitIsHidden = false;
        bool isHidden = playerRef.GetComponent<PlayerController>().isHiddenBush;

        if (isHidden != checkitIsHidden)
        {
            checkitIsHidden = isHidden;

            print("isHidden has changed to: " + isHidden);
            if (isHidden == true)
            {
                lastSeenPosition = playerRef.GetComponent<PlayerController>().lastPosition();
                return lastSeenPosition;
            }
        }

        return playerRef.GetComponent<PlayerController>().lastPosition();
    }

        //----------------------------------------CHEQUEO SFX-----------------------------------------------------------------------------------

    bool checkitSus;
    bool checkitAlert;
    
    void Update()
    {
        isPlayerHidden = playerRef.GetComponent<PlayerController>().isHiddenBush;

        if (canSeePlayer != checkitSus)
        {
            checkitSus = canSeePlayer;

            if (canSeePlayer == true && seesPlayer == false)
            {
                soundManager.PlaySound("sus");
                cs.StartCoroutine(cs.Shaking(1));
            }
            else
                soundManager.StopSound("sus");
        }

        if (seesPlayer != checkitAlert)
        {
            checkitAlert = seesPlayer;

            if (seesPlayer == true && canSeePlayer == true)
            {
                soundManager.PlaySound("alert");
                cs.StartCoroutine(cs.Shaking(2));
            }
            else
                soundManager.StopSound("alert");
        }
    }
}
