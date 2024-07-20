using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Neck : MonoBehaviour
{
    [SerializeField] private int _neckAmount = 6;
    [SerializeField] private int _index = 1;

    private Transform _playerHeadTrm;
    private Transform _playerBodyTrm;

    private Head _head;

    private void Awake() {
        _playerHeadTrm = PlayerManager.Instance.Head.transform;
        _playerBodyTrm = PlayerManager.Instance.Player.transform;

        _head = PlayerManager.Instance.Head;
    }

    private void Update() {
        Vector2 bodyPosition = (Vector2)_playerBodyTrm.position + Vector2.up * 0.4f;
        Vector2 headPosition = (Vector2)_playerHeadTrm.position;

        if(_head.ReturnPositionList.Count > 1) {
            List<Vector2> returnPositionList = _head.ReturnPositionList.ToList();
            transform.position = GetPosition(bodyPosition, returnPositionList[0], headPosition, _index / (_neckAmount + 1f));
        }
        else {
            transform.position = GetPosition(bodyPosition, bodyPosition, headPosition, _index / (_neckAmount + 1f));
        }
    }

    public Vector2 GetPosition(Vector2 startPosition, Vector2 middlePosition, Vector2 endPosition, float percent) {
        float startToMiddleDistance = (middlePosition - startPosition).magnitude;
        float middleToEndDistance = (endPosition - middlePosition).magnitude;
        float sumDistance = startToMiddleDistance + middleToEndDistance;
        float percentDistance = sumDistance * percent;

        if(percentDistance < startToMiddleDistance) {
            return Vector2.Lerp(startPosition, middlePosition, percentDistance / startToMiddleDistance);
        }
        else {
            return Vector2.Lerp(middlePosition, endPosition, (percentDistance - startToMiddleDistance) / middleToEndDistance);
        }
    }
}