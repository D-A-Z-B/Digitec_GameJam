using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public List<Image> boardList = new List<Image>();

    private void Update() {
        for (int i = 0; i < boardList.Count; ++i) {
            boardList[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < PlayerManager.Instance.Head.HealthCompo.MaxHealth; ++i) {
            boardList[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < boardList.Count; ++i) {
            boardList[i].transform.Find("Red").gameObject.SetActive(false);
            boardList[i].transform.Find("Black").gameObject.SetActive(true);
        }

        for (int i = 0; i < PlayerManager.Instance.Head.HealthCompo.CurrentHealth; ++i) {
            boardList[i].transform.Find("Red").gameObject.SetActive(true);
            boardList[i].transform.Find("Black").gameObject.SetActive(false);
        }
    }
}
