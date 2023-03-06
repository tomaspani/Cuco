using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _sensX, _sensY;
    //public float sensX, sensY;
    private float _xRotation, _yRotation;
    [SerializeField] GameObject holder;


    [SerializeField] Slider slider;


    public Transform orientation;

    private void Awake()
    {
        if (PlayerPrefs.GetFloat("sensX") == 0) PlayerPrefs.SetFloat("sensX", 200);
        if (PlayerPrefs.GetFloat("sensY") == 0 && holder.CompareTag("MainCamera")) PlayerPrefs.SetFloat("sensY", 200);
        holder = this.gameObject;
        
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _sensX = PlayerPrefs.GetFloat("sensX");
        if (holder == FindObjectOfType<Camera>()) _sensY = PlayerPrefs.GetFloat("sensY");
    }


    private void FixedUpdate()
    {
        //_sensX = sensX;
        //_sensY = sensY;

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * _sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * _sensY;

        _yRotation += mouseX;
        _xRotation -= mouseY;

        _xRotation = Mathf.Clamp(_xRotation, -90f, 40f);
        

        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, _yRotation, 0);

    }

    public void SensitivitySlider(float valor)
    {
        _sensX = valor;
        PlayerPrefs.SetFloat("sensX", _sensX);
    }

    public void SensitivitySliderY(float valor)
    {
        _sensY = valor;
        PlayerPrefs.SetFloat("sensY", _sensY);
    }

}
