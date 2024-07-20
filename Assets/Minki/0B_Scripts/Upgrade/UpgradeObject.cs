using System;
using UnityEngine;

public class UpgradeObject : MonoBehaviour, IInteractable
{
    public event Action OnInteractEnd;

    [SerializeField] private UpgradeSO _upgradeSO; 
    [SerializeField] private Transform _descriptionPosition;

    public void Interact() {
        OnInteractEnd?.Invoke();
    }

    public void Enter() {
        UIManager.Instance.ActiveUpgradeDescription(_upgradeSO, _descriptionPosition.position);
    }

    public void Exit() {
        UIManager.Instance.DeactiveUpgradeDescription();
    }
}
