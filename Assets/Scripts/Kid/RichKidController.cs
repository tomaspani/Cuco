using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class RichKidController : MonoBehaviour
{
    //public Text feedback;
    [Header("Values")]
    public bool isKidnapable;
    public bool isKidnapableee;
    [SerializeField] public int _currentRoute;
    [SerializeField] float minDistance;

    [SerializeField] public int _currentWaypointIndex;

    [SerializeField] bool IsRunningAway;

    public float susValue;
    [Header("References")]
    [SerializeField] TimeBar timer;
    private FOVKid _fov;
    private bool _canSeeCuco;
    private SoundManager _soundMan;
    private SeeThrough st;
    Pausa _pause;
    NavMeshAgent _nvm;
    [SerializeField] GameObject _timerUI;
    [SerializeField] Text feedback;
    [SerializeField] GameObject Footprints;


    private Animator _myAnim;

    [Header("Routes")]
    public Transform[] WaypointsRoute0;
    public Transform[] WaypointsRoute1;
    public Transform[] WaypointsRoute2;
    public Transform[] WaypointsRoute3;

    private void Start()
    {
        _currentRoute =Random.Range(0, 4);
        _nvm = GetComponent<NavMeshAgent>();
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

        if (isKidnapableee)
        {
            st.enabled = false;

        }
        else
        {
            st.enabled = true;

        }

        FollowPath();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && _canSeeCuco == false && _pause.GameIsPaused == false)
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
            feedback.text = "Press Left Click to Kidnap";
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
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
            Debug.Log("me intentaron secuestrar lol");
            feedback.text = "";
            //Destroy(this.gameObject);

            if (!IsRunningAway)
            {
                _timerUI.SetActive(true);
                timer.StartTimer();

                switch (_currentRoute)
                {
                    case 0:
                        _nvm.SetDestination(WaypointsRoute0[0].position);
                        _currentWaypointIndex = 0;
                        IsRunningAway = true;
                        break;
                    case 1:
                        _nvm.SetDestination(WaypointsRoute1[0].position);
                        _currentWaypointIndex = 0;
                        IsRunningAway = true;
                        break;
                    case 2:
                        _nvm.SetDestination(WaypointsRoute2[0].position);
                        _currentWaypointIndex = 0;
                        IsRunningAway = true;
                        break;
                    case 3:
                        _nvm.SetDestination(WaypointsRoute3[0].position);
                        _currentWaypointIndex = 0;
                        IsRunningAway = true;
                         
                        break;
                }
            }
            switch (_currentRoute)
            {
                case 0:
                    if (_currentWaypointIndex >= WaypointsRoute0.Length) Destroy(this.gameObject);
                    break;
                case 1:
                    if (_currentWaypointIndex >= WaypointsRoute1.Length) Destroy(this.gameObject);
                    break;
                case 2:
                    if (_currentWaypointIndex >= WaypointsRoute2.Length) Destroy(this.gameObject);
                    break;
                case 3:
                    if (_currentWaypointIndex >= WaypointsRoute3.Length) Destroy(this.gameObject);
                    break;

            }
        }

    }

    void FollowPath()
    {
        switch (_currentRoute)
        {
            case 0:
                if (Vector3.Distance(transform.position, WaypointsRoute0[_currentWaypointIndex].position) < minDistance && _currentWaypointIndex <= WaypointsRoute0.Length)
                {
                    Debug.Log("sigo ruta 0");
                    GameObject currentFootprint = Instantiate(Footprints, WaypointsRoute0[_currentWaypointIndex].position, transform.rotation);
                    _currentWaypointIndex++;
                    if (_currentWaypointIndex <= WaypointsRoute0.Length) _nvm.SetDestination(WaypointsRoute0[_currentWaypointIndex].position);
                }
                break;
            case 1:
                if (Vector3.Distance(transform.position, WaypointsRoute1[_currentWaypointIndex].position) < minDistance && _currentWaypointIndex <= WaypointsRoute1.Length)
                {
                    GameObject currentFootprint = Instantiate(Footprints, WaypointsRoute1[_currentWaypointIndex].position, transform.rotation);
                    Debug.Log("sigo ruta 1");
                    _currentWaypointIndex++;
                    if (_currentWaypointIndex <= WaypointsRoute1.Length) _nvm.SetDestination(WaypointsRoute1[_currentWaypointIndex].position);
                }
                break;
            case 2:
                if (Vector3.Distance(transform.position, WaypointsRoute2[_currentWaypointIndex].position) < minDistance && _currentWaypointIndex <= WaypointsRoute2.Length)
                {
                    GameObject currentFootprint = Instantiate(Footprints, WaypointsRoute2[_currentWaypointIndex].position, transform.rotation);
                    Debug.Log("sigo ruta 2 ");
                    _currentWaypointIndex++;
                    if (_currentWaypointIndex <= WaypointsRoute2.Length) _nvm.SetDestination(WaypointsRoute2[_currentWaypointIndex].position);
                }
                break;
            case 3:
                if (Vector3.Distance(transform.position, WaypointsRoute3[_currentWaypointIndex].position) < minDistance )
                {
                    GameObject currentFootprint = Instantiate(Footprints, WaypointsRoute3[_currentWaypointIndex].position, transform.rotation);
                    Debug.Log("sigo ruta 3 ");
                    _currentWaypointIndex++;
                    if (_currentWaypointIndex <= WaypointsRoute3.Length) _nvm.SetDestination(WaypointsRoute3[_currentWaypointIndex].position);

                }
                break;
        }
            

    }
}
