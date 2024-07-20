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

    bool isEnter = false;

    private void Update() {
        if (isEnter == false) return;
        if (Keyboard.current.fKey.isPressed) {
            UIManager.Instance.DeactiveUpgradeDescription();
            AbilityManager.Instance.SelectObject(index);
        }
    }

    public void Enter() {
        isEnter = true;
        AbilityManager.Instance.uiPos.position = new Vector3(AbilityManager.Instance.spawnedUpgradeObjects[index].transform.position.x, 3f);
        UIManager.Instance.ActiveUpgradeDescription(_upgradeSO, _descriptionPosition.position);
    }

    public void Exit() {
        isEnter = false;
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
