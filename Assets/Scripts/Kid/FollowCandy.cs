using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class FollowCandy : MonoBehaviour
{
    public NavMeshAgent Kid;
    [SerializeField] Transform CandyPosition;
    [SerializeField] GameObject TargetedCandy;
    [SerializeField] float Distance;
    FOVKid _fov;

    private Animator _myAnim;
    [SerializeField] private AnimationClip _candy;

    public float FollowDistance;
    private void Start()
    {
        _fov = GetComponent<FOVKid>();
        _myAnim = GetComponentInChildren<Animator>();

    }

    private void Update()
    {
        TargetedCandy = NearestCandy();
        Distance = Vector3.Distance(this.transform.position, TargetedCandy.transform.position);
        if (Distance < FollowDistance && _fov.canSeeCuco == false)
        {ChaseCandy();}
        else { _fov.enabled = true; }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Candy")
            StartCoroutine(crPickUpCandy());
    }

    GameObject NearestCandy ()
    {
        GameObject CandyDetected = GameObject.FindGameObjectWithTag("Candy");
        if (CandyDetected == null)
        {
            Debug.Log("no hay error");
            return null;
        }
        else
            return GameObject.FindGameObjectsWithTag("Candy").Aggregate((o1, o2) => Vector3.Distance(o1.transform.position, this.transform.position) > Vector3.Distance(o2.transform.position, this.transform.position) ? o2 : o1);
    }
    void ChaseCandy ()
    {
        CandyPosition = TargetedCandy.GetComponent<Transform>();
        Kid.SetDestination(CandyPosition.position);
        Kid.GetComponent<KidController>().isKidnapable = true;
        _fov.canSeeCuco = false;
        _fov.enabled = false;
    }

    private IEnumerator crPickUpCandy()
    {
        _myAnim.SetTrigger("candy");
        yield return new WaitForSeconds(_candy.length);
        _myAnim.SetTrigger("candyEnd");
    }
}
