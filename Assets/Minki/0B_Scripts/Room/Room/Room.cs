using System;
using UnityEngine;
using Cinemachine;
using System.Collections;

public class Room : MonoBehaviour
{
    public CinemachineVirtualCamera v_cam;
    [SerializeField] private Portal _portal;
    [SerializeField] private GameObject _frontWall;
    [SerializeField] private GameObject _backWall;

    [SerializeField] private string _stageName;

    public Room nextRoom;

    protected event Action OnActive;
    protected event Action OnCameraMoveEnd;

    private void Start() {
        _portal?.SetOwner(this);
    }

    public void Active() {
        _backWall.SetActive(true); 
        v_cam.Priority = 15;

        StartCoroutine(CameraMoveEndCoroutine());

        OnActive?.Invoke();
    }

    private IEnumerator CameraMoveEndCoroutine() {
        yield return new WaitForSeconds(2f);

        UIManager.Instance.ShowStageLabel(_stageName);
        OnCameraMoveEnd?.Invoke();
    }

    public virtual void Clear() {
        _frontWall.SetActive(false);
    }

    public void GotoNextStage() {
        if(nextRoom == null) return;

        nextRoom.Active();

        v_cam.Priority = 10;
    }

    [ContextMenu("Test")]
    private void Test() {
        Clear();
        GotoNextStage();
    }
}
