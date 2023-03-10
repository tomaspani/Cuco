using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertFeedback : MonoBehaviour
{
    public Image feedback;
    public Sprite[] images;

    EnemySightSensor ess;
    SoundManager soundManager;
    CameraShake cs;

    void Start()
    {
        ess = GetComponent<EnemySightSensor>();
        soundManager = FindObjectOfType<SoundManager>();
        cs = FindObjectOfType<CameraShake>();
    }

    //----------------------------------------CHEQUEO SFX-----------------------------------------------------------------------------------

    bool checkitSus;
    bool checkitAlert;

    // Update is called once per frame
    void Update()
    {
        /*if (fov.canSeePlayer == true && fov.seesPlayer == false)
        {
            var tempColor = feedback.color;
            tempColor.a = 255f;
            feedback.color = tempColor;
            feedback.sprite = images[0];
        }
        else*/ if (ess.GetDetected() == true)
        {
            var tempColor = feedback.color;
            tempColor.a = 255f;
            feedback.color = tempColor;
            feedback.sprite = images[1];
        }
        else
        {

            var tempColor = feedback.color;
            tempColor.a = 0f;
            feedback.color = tempColor;
            feedback.sprite = null;
        }

        if (ess.GetDetected() != checkitAlert)
        {
            checkitAlert = ess.GetDetected();

            if (ess.GetDetected() == true)
            {
                soundManager.PlaySound("alert");
                cs.StartCoroutine(cs.Shaking(2));
            }
            else
                soundManager.StopSound("alert");
        }

        /*if (fov.canSeePlayer == true && fov.seesPlayer == false)
        {
            var tempColor = feedback.color;
            tempColor.a = 255f;
            feedback.color = tempColor;
            feedback.sprite = images[0];
        }
        else if (fov.canSeePlayer == true && fov.seesPlayer == true)
        {
            var tempColor = feedback.color;
            tempColor.a = 255f;
            feedback.color = tempColor;
            feedback.sprite = images[1];
        }
        else
        {

            var tempColor = feedback.color;
            tempColor.a = 0f;
            feedback.color = tempColor;
            feedback.sprite = null;
        }*/


    }
}
