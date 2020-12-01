using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ComboLock : MonoBehaviour
{
    public UnityEvent comboCheck;
    public UnityEvent incCombo;
    public UnityEvent decCombo;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");
        GameObject obj = other.gameObject;
        if (obj.CompareTag("key"))
            incCombo.Invoke();
            comboCheck.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exit");
        GameObject obj = other.gameObject;
        if(obj.CompareTag("key"))
            decCombo.Invoke();
            comboCheck.Invoke();
    }
}
