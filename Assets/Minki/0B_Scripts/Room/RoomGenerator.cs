using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private Room[] _roomPrefabs;
    private List<Room> rooms = new List<Room>();

    [SerializeField] private int _roomAmount = 9;
    [SerializeField] private float _roomOffset = 28.5f;

    [ContextMenu("Generate")]
    public void Generate() {
        Room startRoom = Instantiate(_roomPrefabs[0], Vector2.zero, Quaternion.identity, transform);
        rooms.Add(startRoom);

        Room beforeRoom = startRoom;

        bool createRestRoom = false;
        for(int i = 1; i < _roomAmount - 1; ++i) {
            Vector2 position = new Vector2(_roomOffset * i, 0);

            Room room;

            if(!createRestRoom && i > 3 && Random.Range(0, 2) == 1) {
                room = Instantiate(_roomPrefabs[2], position, Quaternion.identity, transform);
                createRestRoom = true;
            }
            else room = Instantiate(_roomPrefabs[1], position, Quaternion.identity, transform);

            rooms.Add(room);
            
            if(beforeRoom != null)  beforeRoom._nextRoom = room;
            beforeRoom = room;
        }
        
        Room bossRoom = Instantiate(_roomPrefabs[3], new Vector2(_roomOffset * (_roomAmount - 1), 0f), Quaternion.identity, transform);
        rooms.Add(bossRoom);

        rooms[0].Active();
    }
}
