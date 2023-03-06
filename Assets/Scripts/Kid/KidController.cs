using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KidController : MonoBehaviour
{
    //public Text feedback;
    public bool isKidnapable;
    public bool isKidnapableee;
    public GameObject candy;
    public GameObject candyDrop;
    [SerializeField] Text feedback;

    public float susValue;

    private FOVKid _fov;
    private bool _canSeeCuco;
    private SoundManager _soundMan;
    private SeeThrough st;
    Pausa _pause;

    private Animator _myAnim;


    private void Start()
    {
        feedback = GameObject.FindGameObjectWithTag("feedbackText").GetComponent<Text>();
        _fov = GetComponent<FOVKid>();
        _soundMan = FindObjectOfType<SoundManager>();
        st = GetComponentInChildren<SeeThrough>();
        _myAnim = GetComponent<Animator>();
        _pause = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<Pausa>();
    }

    private void FixedUpdate()
    {
        //feedback.text = "";
        _canSeeCuco = _fov.getBoolean();

        if (_canSeeCuco == true)
            SeeCuco();
        //else
        //cantSeeCuco();


        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && _canSeeCuco == false && _pause.GameIsPaused == false)
        {
            isKidnapable = true;
            isKidnapableee = true;
            feedback.text = "Press Left Click to Kidnap";
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && _canSeeCuco == false && _pause.GameIsPaused == false)
        {
            isKidnapable = true;
            isKidnapableee = true;
            //feedback.text = "Press Left Click to Kidnap";
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" )
        {
            isKidnapable = false;
            isKidnapableee = false;
            feedback.text = "";
        }
        
    }

    public void SeeCuco()
    {

        var targetRotation = Quaternion.LookRotation(_fov.player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        _fov.player.addKidSuspicion(susValue);
        //transform.LookAt(_fov.player.transform.position);
        //Debug.Log("omg");
    }

    public void cantSeeCuco()
    {
        _fov.player.LooseSuspicion(susValue);

    }
    public void kidnap()
    {
        if (isKidnapable && _pause.GameIsPaused == false && isKidnapableee)
        {
            _soundMan.PlaySound("kidnap");
            Debug.Log("me secuestraron lol");
            feedback.text = "";
            if (_fov._kidType == KidType.Glutton) { SpawnCandy(); }
            Destroy(this.gameObject);
        }
        
    }

    public void SpawnCandy()
    {
        Instantiate(candy, candyDrop.transform.position, Quaternion.identity);
    }

}
