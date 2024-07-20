using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private Room[] _roomPrefabs;
    private List<Room> _rooms = new List<Room>();

    [SerializeField] private int _roomAmount = 9;
    [SerializeField] private float _roomOffset = 28.5f;

    private Room _beforeRoom;

    public void ActiveFirstRoom() => _rooms[0].Active();

    public void GenerateAll() {
        for(int i = 0; i < 1; ++i) {
            Generate(new Vector2(_roomOffset * _roomAmount * i, 0));
        }
        _rooms[0].v_cam.Priority = 11;
    }

    private void Generate(Vector2 startPosition) {
        Room startRoom = Instantiate(_roomPrefabs[0], startPosition, Quaternion.identity, transform);
        _rooms.Add(startRoom);

        if(_beforeRoom != null) _beforeRoom.nextRoom = startRoom;
        _beforeRoom = startRoom;

        bool createRestRoom = false;
        for(int i = 1; i < _roomAmount - 1; ++i) {
            Vector2 position = new Vector2(_roomOffset * i + startPosition.x, startPosition.y);

            Room room;

            if(!createRestRoom && i > 3 && Random.Range(0, 2) == 1) {
                room = Instantiate(_roomPrefabs[2], position, Quaternion.identity, transform);
                createRestRoom = true;
            }
            else room = Instantiate(_roomPrefabs[1], position, Quaternion.identity, transform);

            _rooms.Add(room);
            
            _beforeRoom.nextRoom = room;
            _beforeRoom = room;
        }
        
        Room bossRoom = Instantiate(_roomPrefabs[3], new Vector2(_roomOffset * (_roomAmount - 1) + startPosition.x, 0f), Quaternion.identity, transform);

        _rooms.Add(bossRoom);
        
        _beforeRoom.nextRoom = bossRoom;
        _beforeRoom = bossRoom;
    }
}
