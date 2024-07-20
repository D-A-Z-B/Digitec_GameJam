using UnityEngine;

public class GasPump : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject _boomEffect;
    [SerializeField] private Sprite _brokenGasPump;
    [SerializeField] private Fire _firePrefab;

    private SpriteRenderer _spriteRenderer;

    private void Awkae() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ApplyDamage(int damage, Transform dealer) {
        Instantiate(_boomEffect, transform.position, Quaternion.identity);

        _spriteRenderer.sprite = _brokenGasPump;
        
        Instantiate(_firePrefab, transform.position, Quaternion.identity);
    }
}
