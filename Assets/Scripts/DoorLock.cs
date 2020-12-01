using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorLock : MonoBehaviour
{
    public UnityEvent onUnlock;
    private bool isLocked = true;
    public GameObject keyPrefab;

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag("key"))
            onUnlock.Invoke();
    }

    private void OnBalloonPop()
    {
        //option 1. Open door on collision b/w arrow and balloon
        //onUnlock.Invoke();
        
        //option 2. Create key when arrow collides with balloon
        GameObject table = GameObject.Find("RoomCTable");
        GameObject roomCKey = GameObject.Find("RoomCKey");

        if (keyPrefab == null)
        {
            return;
        }
        if (roomCKey != null)
        {
            return;
        }
        GameObject key = Instantiate(keyPrefab, new Vector3(table.transform.position.x, table.transform.position.y+1f, table.transform.position.z), table.transform.rotation) as GameObject;
        key.name = "RoomCKey";
    }
}
