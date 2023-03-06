using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySightSensor : MonoBehaviour
{
    public Transform Player { get; private set; }
    float sus = 0;

    [SerializeField] private bool _detected;

    [SerializeField] private LayerMask _ignoreMask;
    [SerializeField] private LayerMask _targetMask;

    private Ray _ray;

    [SerializeField] private float _cantAdults;
    [SerializeField] private float _susRadius;
    [SerializeField] private float _susValue;
    public float SusRadius { get { return _susRadius; } }

    [Range(0, 360)]
    public float angle;

    public void SetSusRadius(float x)
    {
        _susRadius = x;
    }

    public bool GetDetected() { return _detected; }

    public void UncheckMask(int layerToRemove)
    {
        _ignoreMask &= ~(1 << layerToRemove);
    }

    public void CheckMask(int layerToAdd)
    {
        _ignoreMask |= (1 << layerToAdd);
    }

    private void Awake()
    {
        Player = GameObject.Find("Player").transform;
    }

    public bool Ping()
    {
        Collider[] susRangeChecks = Physics.OverlapSphere(transform.position, _susRadius, _targetMask);

        if (susRangeChecks.Length != 0)
        {
            Transform susTarget = susRangeChecks[0].transform;
            Vector3 directionToTarget = (susTarget.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, susTarget.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _ignoreMask))
                {
                    sus += Time.deltaTime;
                    Player.GetComponent<PlayerController>().addSuspicion(_susValue * Time.deltaTime);
                    _detected = true;
                    return true;
                }
                else
                {
                    Player.GetComponent<PlayerController>().LooseSuspicion(_susValue /(_cantAdults*2) * Time.deltaTime);
                    _detected = false;
                    return false;
                }
            }
            else
            {
                Player.GetComponent<PlayerController>().LooseSuspicion(_susValue / (_cantAdults * 2) * Time.deltaTime);
                _detected = false;
                return false;
            }
        }

        Player.GetComponent<PlayerController>().LooseSuspicion(_susValue / (_cantAdults * 2) * Time.deltaTime);
        _detected = false;
        return false;

    }

}