using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NeckParent : MonoBehaviour {
    private List<Neck> neckList = new List<Neck>();
    private Head head;
    private void Awake() {
        neckList = GetComponentsInChildren<Neck>().ToList<Neck>();
        head = PlayerManager.Instance.Head;
    }

    private void Update() {
        foreach(var iter in neckList) {
            if (iter.Index > head.attackRange) {
                iter.gameObject.SetActive(false);
            }
            else {
                iter.gameObject.SetActive(true);    
            }
        }
    }
}