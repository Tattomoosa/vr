using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorLock : MonoBehaviour
{
    public UnityEvent onUnlock;
    private bool isLocked = true;
    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag("key"))
            onUnlock.Invoke();
    }

    private void OnBalloonPop()
    {
        onUnlock.Invoke();
    }
}
