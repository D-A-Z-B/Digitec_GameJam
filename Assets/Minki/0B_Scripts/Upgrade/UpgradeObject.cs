using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UpgradeObject : MonoBehaviour, IInteractable
{
    public event Action OnInteractEnd;

    [SerializeField] private GameObject bigExplosionPrefab;
    [SerializeField] private GameObject smallExplosionPrefab;
    private int index;
    public int Index {
        get => index;
        set {
            index = value;
        }
    }

    [SerializeField] private UpgradeSO _upgradeSO; 
    public UpgradeSO UpgradeSO {
        get => _upgradeSO;
    }
    [SerializeField] private Transform _descriptionPosition;
    public Transform DescriptionPosition {
        get => _descriptionPosition;
        set {
            _descriptionPosition = value;
        }
    }

    public void Interact() {
        OnInteractEnd?.Invoke();
    }

    public void Enter() {
        UIManager.Instance.ActiveUpgradeDescription(_upgradeSO, _descriptionPosition.position);
        if (Keyboard.current.fKey.isPressed) {
            UIManager.Instance.DeactiveUpgradeDescription();
            AbilityManager.Instance.SelectObject(index);
        }
    }

    public void Exit() {
        UIManager.Instance.DeactiveUpgradeDescription();
    }

    private GameObject effect;
    public void BigExplosion() {
        effect = Instantiate(bigExplosionPrefab, transform.position, Quaternion.identity);
    }

    public void SmallExplosion() {
        effect = Instantiate(smallExplosionPrefab, transform.position, Quaternion.identity);
    }
}
