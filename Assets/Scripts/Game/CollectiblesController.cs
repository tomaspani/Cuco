using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectiblesController : MonoBehaviour
{
    [SerializeField] public int Bears;
    [SerializeField] int TotalBears;
    [SerializeField] Text feedbackText;
    public bool HasGotAllBears;

    [SerializeField] string[] lyrics;

    public void PickUpBear (int pickedUpBears)
    {
        Bears += pickedUpBears;
        switch (Bears)
        {
            case 1:
                feedbackText.text = Bears + "/" + TotalBears + ". " + lyrics[0];
                break;
            case 2:
                feedbackText.text = Bears + "/" + TotalBears + ". " + lyrics[1];
                break;
            case 3:
                feedbackText.text = Bears + "/" + TotalBears + ". " + lyrics[2];
                break;
            case 4:
                feedbackText.text = Bears + "/" + TotalBears + ". " + lyrics[3];
                break;
            case 5:
                feedbackText.text = Bears + "/" + TotalBears + ". " + lyrics[4];
                break;
            case 6:
                feedbackText.text = Bears + "/" + TotalBears + ". " + lyrics[5];
                break;
            case 7:
                feedbackText.text = Bears + "/" + TotalBears + ". " + lyrics[6];
                break;
            case 8:
                feedbackText.text = Bears + "/" + TotalBears + ". " + lyrics[7];
                break;
        }
        Invoke(nameof(ResetText), 3);
        if (Bears == TotalBears)
        {
            Invoke(nameof(UnlockSecretEnding), 2);
        }
    }

    void ResetText ()
    {
        feedbackText.text = "";
    }

    void UnlockSecretEnding ()
    {
        HasGotAllBears = true;
        feedbackText.text = "Secret ending unlocked";
    }
}
