using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolPoints : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    private NavMeshAgent _agent;

    public Transform CurrentPoint => _patrolPoints[_currentPoint];

    private int _currentPoint = 0;
    [SerializeField] private bool _isLookingAround = false;

    [SerializeField] private float min, mid, max;
    [SerializeField] private bool b_min, b_mid, b_max;

    /// <summary>
    /// Gets the next point to patrol to
    /// </summary>
    /// <returns></returns>
    public Transform GetNext(NavMeshAgent agent)
    {
        var point = _patrolPoints[_currentPoint];
        _currentPoint = (_currentPoint + 1) % _patrolPoints.Length;
        return point;
    }

    /// <summary>
    /// Checks if destination reached
    /// </summary>
    /// <param name="agent"></param>
    /// <returns></returns>
    /// 

    public bool HasReached(NavMeshAgent agent)
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {

                    _isLookingAround = true;
                    b_min = true;
                    Vector3 minMaxMid = LookAround();
                    min = minMaxMid.x;
                    max = minMaxMid.y;
                    mid = minMaxMid.z;
                    return true;
                }
            }
        }

        return false;
    }

    public Vector3 LookAround()
    {
        Vector3 currentRotation = transform.localRotation.eulerAngles;
        float minRotation = currentRotation.y - 60;
        float maxRotation = currentRotation.y + 120;
        float midRotation = currentRotation.y;

        //max
        if (maxRotation >= 360)
        {
            maxRotation -= 360;
        }
        else if (maxRotation <= -360)
        {
            maxRotation += 360;
        }
        //min
        if (minRotation < 0)
        {
            minRotation += 360;
        }
        else if (minRotation >= 360)
        {
            minRotation -= 360;
        }


        return new Vector3(minRotation, maxRotation, midRotation);
        /*currentRotation.y = Mathf.Clamp(currentRotation.y, minRotation, maxRotation);
        transform.localRotation = Quaternion.Euler(currentRotation);*/

    }
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (_isLookingAround == false)
        {
            _agent.speed = 2f;
        }
        else
        {
            if (b_min == true)
            {
                //Debug.Log("a");
                _agent.speed = 0f;
                transform.Rotate(new Vector3(0f, -10 * Time.deltaTime, 0));
                Vector3 currentRotation = transform.localRotation.eulerAngles;
                //currentRotation.y = Mathf.Clamp(currentRotation.y, min, max);
                transform.localRotation = Quaternion.Euler(currentRotation);
                if (currentRotation.y <= min)
                {
                    b_min = false;
                    b_mid = true;
                }
            }
            else if (b_mid == true)
            {
                //Debug.Log("aa");
                _agent.speed = 0f;
                transform.Rotate(new Vector3(0f, 10 * Time.deltaTime, 0));
                Vector3 currentRotation = transform.localRotation.eulerAngles;
                transform.localRotation = Quaternion.Euler(currentRotation);
                if (currentRotation.y >= mid)
                {
                    b_max = true;
                    b_mid = false;

                }
            }
            else if (b_max == true)
            {
                //Debug.Log("aaa");
                _agent.speed = 0f;
                transform.Rotate(new Vector3(0f, 10 * Time.deltaTime, 0));
                Vector3 currentRotation = transform.localRotation.eulerAngles;
                transform.localRotation = Quaternion.Euler(currentRotation);
                if (currentRotation.y >= max)
                {
                    _isLookingAround = false;
                    b_max = false;
                }
            }



        }
    }
    private void Look(float x)
    {
        //_agent.speed = 0f;
        transform.Rotate(new Vector3(0f, 0.2f * x, 0));
        Vector3 currentRotation = transform.localRotation.eulerAngles;
        //currentRotation.y = Mathf.Clamp(currentRotation.y, min, max);
        transform.localRotation = Quaternion.Euler(currentRotation);
        if (currentRotation.y >= max)
        {
            _isLookingAround = false;
        }

    }

    public void SetBool(bool val)
    {
        _isLookingAround = val;
    }
}