using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] Transform[] floorParents = new Transform[4];
    [SerializeField] List<Room> rooms = new List<Room>();

    void Start()
    {
        PlaceRoom("Entrance Hall", 1, Vector2.zero, "north");
        PlaceRoom("Foyer", 1, new Vector2(-1, 0), "north");
        PlaceRoom("Grand Staircase", 1, new Vector2(-2, 0), "north");
        PlaceRoom("Basement Landing", 0, Vector2.zero, "north");
        PlaceRoom("Upper Landing", 2, Vector2.zero, "north");
        PlaceRoom("Roof Landing", 3, Vector2.zero, "north");

    }
    void PlaceRoom(string roomName, int floor, Vector2 location, string heading)
    {
        if (floor < 0 || floor > 3)
        {
            Debug.LogError("Invalid floor number given");
            return;
        }
        int rotation = 0;
        switch (heading)
        {
            case "north":
                rotation = 0;
                break;
            case "south":
                rotation = 180;
                break;
            case "east":
                rotation = 270;
                break;
            case "west":
                rotation = 90;
                break;
            default:
                Debug.LogError("Invalid heading given");
                return;
        }
        Room roomToPlace = null;
        foreach (Room room in rooms)
        {
            if (room.gameObject.name == roomName)
            {
                roomToPlace = room;
                break;
            }
        }
        if (roomToPlace == null)
        {
            Debug.LogError($"Room {roomName} not found");
            return;
        }
        Instantiate(roomToPlace, location, Quaternion.Euler(new Vector3(0, 0, rotation)), floorParents[floor]);
        MoveFloor(floor);
    }

    void MoveFloor(int floor)
    {
        int coordY = 0;
        int coordX = 0;
        switch (floor)
        {
            case 0:
                //look for lowest x & highest y

                foreach (Room room in floorParents[0].GetComponentsInChildren<Room>())
                {
                    if (room.gameObject.transform.position.y > coordY)
                    {
                        coordY = Mathf.RoundToInt(room.gameObject.transform.position.y);
                    }
                    if (room.gameObject.transform.position.x < coordX)
                    {
                        coordX = Mathf.RoundToInt(room.gameObject.transform.position.x);
                    }
                }
                //move floor to (-(x+1),-(y+1))
                floorParents[0].transform.position = new Vector2(-(coordX + 1), -(coordY + 1));
                break;
            case 1:
                //look for lowest x & y
                foreach (Room room in floorParents[1].GetComponentsInChildren<Room>())
                {
                    if (room.gameObject.transform.position.y < coordY)
                    {
                        coordY = Mathf.RoundToInt(room.gameObject.transform.position.y);
                    }
                    if (room.gameObject.transform.position.x < coordX)
                    {
                        coordX = Mathf.RoundToInt(room.gameObject.transform.position.x);
                    }
                }
                //move floor to (-(x+1),-(y+1))
                floorParents[1].transform.position = new Vector2(-(coordX + 1), -(coordY + 1));
                break;
            case 2:
                //look for highest x & y
                foreach (Room room in floorParents[2].GetComponentsInChildren<Room>())
                {
                    if (room.gameObject.transform.position.y < coordY)
                    {
                        coordY = Mathf.RoundToInt(room.gameObject.transform.position.y);
                    }
                    if (room.gameObject.transform.position.x < coordX)
                    {
                        coordX = Mathf.RoundToInt(room.gameObject.transform.position.x);
                    }
                }
                //move floor to (abs(x+1),-(y+1))
                floorParents[2].transform.position = new Vector2(-(coordX + 1), -(coordY + 1));
                break;
            case 3:
                //look for highest x & lowest y
                foreach (Room room in floorParents[3].GetComponentsInChildren<Room>())
                {
                    if (room.gameObject.transform.position.y < coordY)
                    {
                        coordY = Mathf.RoundToInt(room.gameObject.transform.position.y);
                    }
                    if (room.gameObject.transform.position.x < coordX)
                    {
                        coordX = Mathf.RoundToInt(room.gameObject.transform.position.x);
                    }
                }
                //move floor to (abs(x+1),-(y+1))
                floorParents[3].transform.position = new Vector2(-(coordX + 1), -(coordY + 1));
                break;
        }
    }
}
