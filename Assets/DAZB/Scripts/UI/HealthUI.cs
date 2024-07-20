using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObject healthContainer;
    private List<Image> boardList = new List<Image>();

    private void Awake() {
        boardList = healthContainer.GetComponentsInChildren<Image>().ToList();
    }

    private void Update() {
        for (int i = 0; i < boardList.Count; ++i) {
            boardList[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < PlayerManager.Instance.Head.HealthCompo.MaxHealth; ++i) {
            boardList[i].gameObject.SetActive(true);
        }
    }
}
