using UnityEngine;

public class RoomsManager : MonoBehaviour
{
    public float beginningPoint;
    public float[] roomsEnd;
    public bool isKeyObtained;
    public int currentRoomIndex;
    public int reachedRoomIndex;

    public float CurrentLeftSidePosition()
    {
        if (currentRoomIndex  <= 0 )
        {
            return beginningPoint;
        }
        return roomsEnd[currentRoomIndex - 1];
    }

    public float CurrentRightSidePosition()
    {
        return roomsEnd[currentRoomIndex];
    }
}
