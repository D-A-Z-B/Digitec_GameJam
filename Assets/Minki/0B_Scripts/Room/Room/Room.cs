using System;
using UnityEngine;
using Cinemachine;

public class Room : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera v_cam;
    [SerializeField] private GameObject _frontWall;
    [SerializeField] private GameObject _backWall;

    public Room _nextRoom;

    protected Action OnActive;

    public void Active() {
        _backWall.SetActive(true); 
        v_cam.Priority = 15;

        OnActive?.Invoke();
    }

    public virtual void Clear() {
        _frontWall.SetActive(false);
    }

    public void GotoNextStage() {
        if(_nextRoom == null) return;

        _nextRoom.Active();

        v_cam.Priority = 10;
    }

    [ContextMenu("Test")]
    private void Test() {
        Clear();
        GotoNextStage();
    }
}
