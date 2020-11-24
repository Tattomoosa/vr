using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    // private bool isOpening = false;
    public float duration = 2.0f;
    public GameObject doorObject;
    public Vector3 openPosition;
    public Vector3 closedPosition;

    private void Start()
    {
        doorObject.transform.localPosition = isOpen ? openPosition : closedPosition;
    }

    public void Open()
    {
        isOpen = true;
        StartCoroutine(SlideDoorObjectTo(openPosition));
    }

    public void Close()
    {
        isOpen = false;
        StartCoroutine(SlideDoorObjectTo(closedPosition));
    }

    private IEnumerator SlideDoorObjectTo(Vector3 position)
    {
        // const float step = 0.01f;
        float currentTime = 0;
        Transform doorTransform = doorObject.transform;
        Vector3 startPosition = doorTransform.localPosition;
        // isOpening = true;
        
        while (currentTime < duration)
        {
            float openPercent = Mathf.InverseLerp(0, duration, currentTime);
            doorTransform.localPosition = Vector3.Lerp(startPosition, position, openPercent);
            currentTime += Time.deltaTime;
            yield return 0;
        }

        // isOpening = false;
    }

}
