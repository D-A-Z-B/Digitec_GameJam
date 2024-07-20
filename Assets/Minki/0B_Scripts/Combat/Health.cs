using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IDamageable
{
    public event Action OnDead;
    public event Action OnHit;

    [Header("Health Settings")]
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
    public int MaxHealth {
        get => _maxHealth;
        set {
            _maxHealth = value;
        }
    }

    [SerializeField] private bool _isPlayer = false;
    [SerializeField] private Material _whiteMat;
    private Material _originMat;

    [HideInInspector] public bool _isInvincible = false;

    [HideInInspector] public Image healthFilled;

    private Agent _owner;

    public int CurrentHealth {
        get => _currentHealth;
        set {
            _currentHealth = Mathf.Clamp(value, 0, _maxHealth);
            healthFilled.fillAmount = (float)_currentHealth / _maxHealth;
        }
    }

    public void SetOwner(Agent owner) {
        _owner = owner;

        _currentHealth = _maxHealth;
        try {
            _originMat = _owner.SpriteRendererCompo.material;
        }
        catch {
            _originMat = PlayerManager.Instance.Player.transform.Find("Torso").GetComponent<SpriteRenderer>().material;
        }
    }

    public void IncreaseHealth(int value) {
        CurrentHealth += value;
    }

    public void ApplyDamage(int damage, Transform dealer) {
        if(_owner.isDead || _isInvincible) return;

        CurrentHealth -= damage;

        OnHit?.Invoke();

        if(_currentHealth == 0) {
            _owner.isDead = true;

            OnDead?.Invoke();
        }
        else Blink();
    }

    [ContextMenu("Blink")]
    private void Blink() {
        if(_isPlayer) StartCoroutine(BlinkCoroutine());
        else {
            _owner.SpriteRendererCompo.material = _whiteMat;
            healthFilled.material = _whiteMat;
            _owner.StartDelayCallback(0.1f, () => {
                _owner.SpriteRendererCompo.material = _originMat;
                healthFilled.material = null;
            });
        }
    }

    private IEnumerator BlinkCoroutine() {
        float timer = Time.deltaTime;

        _owner.SpriteRendererCompo.color = new Color(1, 1, 1, 1);
        _isInvincible = true;

        while(timer < 1.5f) {
            timer += Time.deltaTime;

            float alpha = Mathf.Sin(timer * 12.6f + 1.5f) * 0.35f + 0.65f;
            _owner.SpriteRendererCompo.color = new Color(1, 1, 1, alpha);

            yield return null;
        }
        _owner.SpriteRendererCompo.color = new Color(1, 1, 1, 1);
        _isInvincible = false;
    }
}