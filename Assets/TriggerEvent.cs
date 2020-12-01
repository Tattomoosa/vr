using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public GameObject player;
    public UnityEvent triggerExit;
    public UnityEvent triggerEnter;
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == player)
            triggerEnter.Invoke();
    }
    public void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == player)
            triggerExit.Invoke();
    }
}
