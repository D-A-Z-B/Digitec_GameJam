using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    public List<Image> boardList = new List<Image>();

    private void Update() {
        for (int i = 0; i < AbilityManager.Instance.PlayerAbilityEffectSOList.Count; ++i) {
            Image image = boardList[i].GetComponentInChildren<Image>(false);
            image.color = new Color(1, 1, 1, 1);
            image.sprite = AbilityManager.Instance.PlayerAbilityEffectSOList[i].icon;
        }
    }
}
