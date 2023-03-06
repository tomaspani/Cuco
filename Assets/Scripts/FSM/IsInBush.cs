using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInBush : MonoBehaviour
{
    [SerializeField] bool _inBush;

    EnemySightSensor _ess;

    private void Start()
    {
        _ess = GetComponent<EnemySightSensor>();
    }

    private void Update()
    {
        if (_inBush)
        {
            _ess.UncheckMask(8);
            _ess.SetSusRadius(5.5f);
        }
        else
        {
            _ess.CheckMask(8);
            _ess.SetSusRadius(11f);
        }
    }

    public bool getInBush()
    {
        return _inBush;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bush"))
        {

            Debug.Log("SAAAAAAAAAAAAAAAAAAAAAA");
            _inBush = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("bush"))
        {
            _inBush = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("bush"))
        {
            _inBush = true;
        }
    }
}
