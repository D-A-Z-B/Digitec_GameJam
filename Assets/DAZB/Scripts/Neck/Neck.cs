using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Neck : MonoBehaviour
{
    enum Type
    {
        Increase,
        Decrease
    }

    [SerializeField] private float maxNeckRange;
    [SerializeField] private GameObject neckPrefab;
    public float currentNeckRange { get; private set; }
    private Player player;
    private Head head;
    private GameObject[] groupA;
    private GameObject[] groupB;
    private Type type;

    private void Start() {
        player = PlayerManager.Instance.Player;
        head = PlayerManager.Instance.Head;
        groupA = new GameObject[4];
        groupB = new GameObject[4];
        for (int i = 0; i < 4; ++i) {
            GameObject go = Instantiate(neckPrefab, transform);
            go.SetActive(false);
            groupA[i] = go;
            go = Instantiate(neckPrefab, transform);
            go.SetActive(false);
            groupB[i] = go;
        }
    }

    private void Update() {
        Render();
    }

    private void Render() {
        List<Vector2> tempList = head.ReturnPositionList.ToList();

        if (head.StateMachine.CurrentStateEnum == HeadStateEnum.Return) {
            type = Type.Decrease;
        }
        else {
            type = Type.Increase;
        }

        switch (type)
        {
            case Type.Increase:
                if (head.ReturnPositionList.Count == 0) {
                    GroupARender(new Vector2(player.transform.position.x, player.transform.position.y + 0.8f), head.transform.position);
                }
                else if (head.ReturnPositionList.Count == 1) {
                    GroupARender(tempList[0], head.transform.position);
                }
                else if (head.ReturnPositionList.Count > 1) {
                    GroupARender(new Vector2(player.transform.position.x, player.transform.position.y + 0.8f), tempList[0]);
                    GroupBRender(tempList[0], head.transform.position);
                }
                break;

            case Type.Decrease:
                if (tempList.Count > 1) {
                    GroupARender(tempList[1], tempList[0]);
                    GroupBRender(tempList[0], head.transform.position);
                }
                else {
                    GroupARender(new Vector2(player.transform.position.x, player.transform.position.y + 0.8f), head.transform.position);
                }
                break;
        }
    }

    private void GroupARender(Vector2 startPos, Vector2 endPos) {
        float groupLength = groupA.Length;
        float totalDistance = Vector2.Distance(startPos, endPos);

        for (int i = 0; i < groupA.Length; ++i) {
            groupA[i].SetActive(true);
            float t = (i + 1.0f) / groupLength;
            Vector2 position = Vector2.Lerp(startPos, endPos, t);
            groupA[i].transform.position = position;
        }
    }

    private void GroupBRender(Vector2 startPos, Vector2 endPos) {
        float groupLength = groupB.Length;
        float totalDistance = Vector2.Distance(startPos, endPos);

        for (int i = 0; i < groupB.Length; ++i) {
            groupB[i].SetActive(true);
            float t = (i + 1.0f) / groupLength;
            Vector2 position = Vector2.Lerp(startPos, endPos, t);
            groupB[i].transform.position = position;
        }
    }

    private void Active(string s, bool value) {
        if (s == "A") {
            foreach (var go in groupA)
            {
                go.SetActive(value);
            }
        }
        else if (s == "B")
        {
            foreach (var go in groupB)
            {
                go.SetActive(value);
            }
        }
    }
}
