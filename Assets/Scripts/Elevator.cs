using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform transformToMove;
    public FloorInfo[] floors;
    public int currentFloor = 0;

    public float maxSpeed = 5.0f;
    
    private void Start()
    {
        SetFloor(currentFloor);
    }

    public void GoToFloor(int floor)
    {
        if (!ValidFloor(floor))
            throw new Exception("GoToFloor called with invalid floor.");
        StartCoroutine(GoToFloor_(floor));
    }

    public void GoToNextFloor()
    {
        GoToFloor(currentFloor + 1);
    }

    public void GoToPreviousFloor()
    {
        GoToFloor(currentFloor - 1);
    }

    private bool ValidFloor(int floor)
    {
        return floor >= 0 && floor < floors.Length;
    }

    private void SetFloor(int floor)
    {
        currentFloor = floor;
        Transform t = transformToMove;
        Vector3 pos = t.position;
        pos.y = -floors[currentFloor].yPos;
        t.position = pos;
        
    }

    private IEnumerator GoToFloor_(int floor)
    {
        Transform t = transformToMove;
        float startY = t.position.y;
        float endY = -floors[floor].yPos;
        float moveDirection = Mathf.Sign(endY - startY);
        float currentY = startY;
        while (
            (moveDirection > 0 && currentY < endY) ||
            (moveDirection < 0 && currentY > endY)
        )
        {
            currentY += maxSpeed * moveDirection * Time.deltaTime;
            Vector3 pos = t.position;
            pos.y = currentY;
            t.position = pos;
            yield return 0;
        }
        SetFloor(floor);
    }

    [System.Serializable]
    public struct FloorInfo
    {
        public float yPos;
        public string Name;
    }
}
