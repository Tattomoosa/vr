using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

public class Elevator : MonoBehaviour
{
    // public Transform transformToMove;
    public FloorInfo[] floors;
    public int currentFloor = 0;
    public Transform player;
    public Door leftDoor;
    public Door rightDoor;

    public float maxSpeed = 1.0f;
    public float waitAfterDoorsClose = 0.5f;
    public UnityEvent onStartMovingUp;
    public UnityEvent onStartMovingDown;
    public UnityEvent onStartMovingAfterDoors;
    public UnityEvent onEndMoving;

    private bool isMoving = false;
    private bool isWaitingToClose = false;

    
    private void Start()
    {
        SetFloor(currentFloor);
    }

    public void GoToFloor(int floor)
    {
        if (floor == currentFloor)
            return;
        if (!ValidFloor(floor))
            Debug.Log("GoToFloor called with invalid floor.");
        if (isMoving)
            Debug.Log("GoToFloor is already moving.");
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
        Transform t = transform;
        Vector3 pos = t.position;
        pos.y = floors[currentFloor].yPos;
        t.position = pos;
    }

    private IEnumerator GoToFloor_(int floor)
    {
        isMoving = true;
        Transform t = transform;
        float startY = t.position.y;
        float endY = floors[floor].yPos;
        float moveDirection = Mathf.Sign(endY - startY);
        float currentY = startY;

        if (moveDirection > 0)
            onStartMovingUp.Invoke();
        else
            onStartMovingDown.Invoke();
        
        leftDoor.Close();
        rightDoor.Close();
        yield return new WaitForSeconds(leftDoor.duration + waitAfterDoorsClose);
        onStartMovingAfterDoors.Invoke();
        
        Transform playerParent = player.parent;
        player.parent = transform;
        
        while (
            (moveDirection > 0 && currentY < endY) ||
            (moveDirection < 0 && currentY > endY)
        )
        {
            if (!isMoving)
                yield break;
            currentY += maxSpeed * moveDirection * Time.deltaTime;
            Vector3 pos = t.position;
            if (moveDirection > 0 && currentY > endY ||
                moveDirection < 0 && currentY < endY)
                pos.y = endY;
            else
                pos.y = currentY;
            t.position = pos;
            yield return null;
        }
        onEndMoving.Invoke();
        
        SetFloor(floor);
        currentFloor = floor;
        isMoving = false;
        player.parent = playerParent;
        leftDoor.Open();
        rightDoor.Open();
        isWaitingToClose = true;
    }

    public void AttemptCloseDoors()
    {
        if (!isWaitingToClose)
            return;
        leftDoor.Close();
        rightDoor.Close();
    }

    [System.Serializable]
    public struct FloorInfo
    {
        public float yPos;
        public string Name;
    }
    
}
