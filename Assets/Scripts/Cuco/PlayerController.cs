using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    const float maxSus = 100f;
    const float maxKidSus = 45f;

    float limitKidSus;

    public float suspicion;
    public int kidnappedKids;
    public int kidsInBag;
    public bool isHiddenBush;

    [SerializeField] Text text;
    public bool isSus;
    [SerializeField] TimeBar TimeBar;
    [SerializeField] bool firstKid;

   
    public void SnatchKid(KidController kid)
    {
       
        kidnappedKids++;
        kidsInBag++;
        if (!firstKid)
        {
            text.text = "Hold down Space bar to consume kid";
            firstKid = true;
            Invoke(nameof(ClearText), 10);
        }
        
        kid.kidnap();
    }

    void ClearText ()
    {
        text.text = "";
    }

    public void SnatchRichKid(RichKidController kid)
    {
        switch (kid._currentRoute)
        {
            case 0:
                if (kid._currentWaypointIndex >= kid.WaypointsRoute0.Length)
                {
                    kidnappedKids++;
                    kidsInBag++;
                    if (TimeBar != null) TimeBar.StopTimer();
                }
                break;
            case 1:
                if (kid._currentWaypointIndex >= kid.WaypointsRoute1.Length)
                {
                    kidnappedKids++;
                    kidsInBag++;
                    if (TimeBar != null) TimeBar.StopTimer();
                }
                break;
            case 2:
                if (kid._currentWaypointIndex >= kid.WaypointsRoute2.Length)
                {
                    kidnappedKids++;
                    kidsInBag++;
                    if (TimeBar != null) TimeBar.StopTimer();
                }
                break;
            case 3:
                if (kid._currentWaypointIndex >= kid.WaypointsRoute3.Length)
                {
                    kidnappedKids++;
                    kidsInBag++;
                    if (TimeBar != null) TimeBar.StopTimer();
                }
                break;

        }

        kid.kidnap();
    }

    public void addSuspicion(float val)
    {
        isSus = true;
        suspicion += val;
    }

    public void addKidSuspicion(float val)
    {
        float x = val * Time.deltaTime;
        isSus = true;

        if (suspicion < (maxSus * 0.9f))
        {
            if(limitKidSus < maxKidSus)
            {
                suspicion += x;
                limitKidSus += x;
                
            }
            else
            {
                
            }
        }
        //else
            //suspicion = maxSus * 0.9f;
        
    }

    public void LooseSuspicion(float val)
    {
        isSus = false;
        suspicion -= val;
        limitKidSus = 0;
        if(suspicion < 0f)
        {
            suspicion = 0f;
        }
    }

    public void Consume()
    {
        kidsInBag = 0;
    }

    public Vector3 lastPosition()
    {
        return this.transform.position;

    }

}
