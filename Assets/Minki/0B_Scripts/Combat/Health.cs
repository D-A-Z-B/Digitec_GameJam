using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IDamageable
{
    public event Action OnDead;
    public event Action OnHit;

    [Header("Health Settings")]
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    [SerializeField] private Material _whiteMat;
    private Material _originMat;

    [HideInInspector] public Image healthFilled;

    private Agent _owner;

    public void SetOwner(Agent owner) {
        _owner = owner;

        _currentHealth = _maxHealth;
        _originMat = _owner.SpriteRendererCompo.material;
    }

    public void ApplyDamage(int damage, Transform dealer) {
        if(_owner.isDead) return;

        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
        healthFilled.fillAmount = (float)_currentHealth / _maxHealth;

        OnHit?.Invoke();

        if(_currentHealth == 0) {
            _owner.isDead = true;

            OnDead?.Invoke();
        }
        else Blink();
    }

    private void Blink() {
        _owner.SpriteRendererCompo.material = _whiteMat;
        healthFilled.material = _whiteMat;
        _owner.StartDelayCallback(0.1f, () => {
            _owner.SpriteRendererCompo.material = _originMat;
            healthFilled.material = null;
        });
    }
}