using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private Room[] _roomPrefabs;
    private List<Room> rooms = new List<Room>();

    [SerializeField] private int _roomAmount = 9;
    [SerializeField] private float _roomOffset = 28.5f;

    private Room beforeRoom;

    [ContextMenu("Generate")]
    private void GenerateAll() {
        for(int i = 0; i < 3; ++i) {
            Generate(new Vector2(_roomOffset * _roomAmount * i, 0));
        }
        rooms[0].Active();
        _currentRoom = rooms[0];
    }

    public void Generate(Vector2 startPosition) {
        Room startRoom = Instantiate(_roomPrefabs[0], startPosition, Quaternion.identity, transform);
        rooms.Add(startRoom);

        if(beforeRoom != null) beforeRoom.nextRoom = startRoom;
        beforeRoom = startRoom;

        bool createRestRoom = false;
        for(int i = 1; i < _roomAmount - 1; ++i) {
            Vector2 position = new Vector2(_roomOffset * i + startPosition.x, startPosition.y);

            Room room;

            if(!createRestRoom && i > 3 && Random.Range(0, 2) == 1) {
                room = Instantiate(_roomPrefabs[2], position, Quaternion.identity, transform);
                createRestRoom = true;
            }
            else room = Instantiate(_roomPrefabs[1], position, Quaternion.identity, transform);

            rooms.Add(room);
            
            beforeRoom.nextRoom = room;
            beforeRoom = room;
        }
        
        Room bossRoom = Instantiate(_roomPrefabs[3], new Vector2(_roomOffset * (_roomAmount - 1) + startPosition.x, 0f), Quaternion.identity, transform);

        rooms.Add(bossRoom);
        
        beforeRoom.nextRoom = bossRoom;
        beforeRoom = bossRoom;
    }

    //Test
    private Room _currentRoom;
    private void Update() {
        if(Input.GetKeyDown(KeyCode.P)) {
            _currentRoom.Clear();
            _currentRoom.GotoNextStage();
            _currentRoom = _currentRoom.nextRoom;
        }
    }
}
