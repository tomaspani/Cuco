using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AdultController : MonoBehaviour
{

    [SerializeField] float suspcionValue;
    //[SerializeField] Material[] mat;
    private FieldOfView _fov;
    private WaypointMover _WM;

    public Animator _myAnim;
    NavMeshAgent _nvm;


    private void Start()
    {
        //if(_fov == null)
        //{
        //this.gameObject.GetComponent<MeshRenderer>().material = mat[0];
        _fov = this.GetComponentInParent<FieldOfView>();
        _WM = this.GetComponentInParent<WaypointMover>();
        //}
        _nvm = GetComponent<NavMeshAgent>();
        _myAnim = GetComponentInChildren<Animator>();
        //Debug.Log(_myAnim.name);


    }



    /*private void SeeThrough()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            this.gameObject.GetComponent<MeshRenderer>().material = mat[1];
        }
        else
        {
            this.gameObject.GetComponent<MeshRenderer>().material = mat[0];
        }
    }*/

    private void FixedUpdate()
    {
    }

    private void Update()
    {
        float movespeed = _nvm.velocity.magnitude;

        if (movespeed > 0)
        {
            _myAnim.SetTrigger("onRun");
        }
        else
            _myAnim.SetTrigger("onRunEnd");
    }

    public void AddSuspicion(GameObject player)
    {
        player.GetComponent<PlayerController>().addSuspicion(suspcionValue * Time.deltaTime);
    }

    public void AddAlert(GameObject player, float multiplier)
    {
        player.GetComponent<PlayerController>().addSuspicion(25f * Time.deltaTime);
    }

    public void LooseSuspicion(GameObject player)
    {
        player.GetComponent<PlayerController>().LooseSuspicion(suspcionValue / 8f  * Time.deltaTime);
    }


    public void GoToKid(KidController kid)
    {
        _WM.GoToKid(kid);
    }


}
