using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomPathFinding : MonoBehaviour
{
    [Header("References")]
    NavMeshAgent nVM;
    PlayerController player;
    [SerializeField] LayerMask cucoLayer;

    [Header("Values")]
    public Vector3 WalkPoint;
    [SerializeField] bool WalkPointSet;
    public float WalkPointRange;
    [SerializeField] float _detectionRange;
    [SerializeField] bool playerInLight {get { return Physics.CheckSphere(transform.position, _detectionRange, cucoLayer); } }
    [SerializeField] float helicopterSuspicion;

    private void Awake()
    {
        nVM = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerController>();
    }

    private void FixedUpdate()
    {
        //Debug.Log(playerInLight);
        if (!playerInLight || player.isHiddenBush) Patrol();
        if (playerInLight && !player.isHiddenBush) FollowPlayer();
        
    }

    void Patrol ()
    {
        if (!WalkPointSet) SearchWalkPoint();
        if (WalkPointSet) 
        {
            nVM.SetDestination(WalkPoint);
            Invoke(nameof(ResetWalkPoint), 5);
        }


        Vector3 distanceToWalkPoint = transform.position - WalkPoint;
        if (distanceToWalkPoint.magnitude < 1f) WalkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        CancelInvoke(nameof(ResetWalkPoint));
        float randomZ = Random.Range(-WalkPointRange, WalkPointRange);
        float randomX = Random.Range(-WalkPointRange, WalkPointRange);

        WalkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        NavMeshPath navMeshPath = new NavMeshPath();
        if (nVM.CalculatePath(WalkPoint, navMeshPath))
        {
            WalkPointSet = true;
        }

    }

    void FollowPlayer ()
    {
        nVM.SetDestination(player.transform.position);
        player.addSuspicion(helicopterSuspicion * Time.deltaTime);
    }

    void ResetWalkPoint()
    {
        WalkPointSet = false;
    }
}
