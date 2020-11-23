using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = true;
    public float duration = 2.0f;
    public GameObject doorObject;
    public Vector3 openPosition;
    public Vector3 closedPosition;

    private void Start()
    {
        doorObject.transform.localPosition = isOpen ? closedPosition : openPosition;
    }

    public void Open()
    {
        isOpen = false;
        StartCoroutine(SlideDoorObjectTo(openPosition));
    }

    public void Close()
    {
        isOpen = true;
        StartCoroutine(SlideDoorObjectTo(closedPosition));
    }

    private IEnumerator SlideDoorObjectTo(Vector3 position)
    {
        // const float step = 0.01f;
        float currentTime = 0;
        Transform doorTransform = doorObject.transform;
        Vector3 startPosition = doorTransform.localPosition;
        
        while (currentTime < duration)
        {
            float openPercent = Mathf.InverseLerp(0, duration, currentTime);
            doorTransform.localPosition = Vector3.Lerp(startPosition, position, openPercent);
            currentTime += Time.deltaTime;
            yield return 0;
        }
        // yield return new WaitForSeconds(step);

        /*
        for (float time = 0f; time <= duration; time += step)
        {
           // get ratio between ft and duration
           float openPercent = Mathf.InverseLerp(0, duration, time);
           doorTransform.localPosition = Vector3.Lerp(startPosition, position, openPercent);
           yield return new WaitForSeconds(step);
        }
        */
    }

}
