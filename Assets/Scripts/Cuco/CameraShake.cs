using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration;
    [SerializeField]Transform cameraPos;
    public AnimationCurve curve;
    CameraFollow CF;

    private void Start()
    {
        CF = GetComponent<CameraFollow>();
    }

    public IEnumerator Shaking (int strenghtMultiplier)
    {
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            CF.enabled = false;
            elapsedTime += Time.deltaTime;
            float strenght = curve.Evaluate(elapsedTime / shakeDuration) * strenghtMultiplier;
            transform.position = cameraPos.position + Random.insideUnitSphere * strenght;
            yield return null;
        }
        CF.enabled = true;
        transform.position = startPos;
    }
}
