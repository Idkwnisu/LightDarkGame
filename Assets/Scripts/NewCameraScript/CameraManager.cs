using System;
using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public Transform player;
    public RoomsManager roomsManager;
    public float changingRoomSpeed;

    private float cameraWidth;

    private void Awake()
    {
        cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
        if (player.position.x < roomsManager.CurrentLeftSidePosition() + cameraWidth )
        {
            Vector3 newCameraPos = transform.position;
            newCameraPos.x = roomsManager.CurrentLeftSidePosition() + cameraWidth;
            transform.position = newCameraPos;
        }
    }

    void FixedUpdate()
    {
        if ( player.position.x < roomsManager.beginningPoint )
        {
            throw new Exception("Player should not stay before beginning point");
        }
        else if ( player.position.x < roomsManager.CurrentLeftSidePosition() )
        {
            GoToPreviousRoom();
        }
        else if (
            player.position.x > roomsManager.CurrentLeftSidePosition() + cameraWidth &&
            player.position.x < roomsManager.CurrentRightSidePosition() - cameraWidth
        )
        {
            Vector3 newCameraPos = player.position;
            newCameraPos.z = transform.position.z;
            transform.position = newCameraPos;
        }
        else if ( player.position.x > roomsManager.CurrentRightSidePosition() )
        {
            GoToNextRoom();
        }
        else
        {
            Vector3 newCameraPos = transform.position;
            newCameraPos.y = player.position.y;
            transform.position = newCameraPos;
        }
    }

    private void GoToPreviousRoom()
    {
        if ( roomsManager.currentRoomIndex <= 0 )
        {
            throw new Exception("Can not go to a room after the last one");
        }
        roomsManager.currentRoomIndex--;
        StartCoroutine(PreviousRoomCameraAnimation());

    }
    private IEnumerator PreviousRoomCameraAnimation()
    {
        float endPosition = roomsManager.roomsEnd[roomsManager.currentRoomIndex] - cameraWidth;
        while ( transform.position.x > endPosition )
        {
            transform.Translate(Vector3.left * Time.deltaTime * changingRoomSpeed);
            yield return null;
        }
    }

    private void GoToNextRoom()
    {
        if ( roomsManager.currentRoomIndex >= roomsManager.roomsEnd.Length - 1 )
        {
            throw new Exception("Can not go to a room after the last one");
        }
        roomsManager.currentRoomIndex++;
        StartCoroutine(NextRoomCameraAnimation());
    }
    private IEnumerator NextRoomCameraAnimation()
    {
        float endPosition = roomsManager.roomsEnd[roomsManager.currentRoomIndex - 1] + cameraWidth;
        while ( transform.position.x < endPosition )
        {
            transform.Translate(Vector3.right * Time.deltaTime * changingRoomSpeed);
            yield return null;
        }
    }

}
