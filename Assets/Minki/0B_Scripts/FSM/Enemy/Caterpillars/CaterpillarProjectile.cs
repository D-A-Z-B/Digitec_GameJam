using UnityEngine;

public class CaterpillarProjectile : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsPlayer;
    [SerializeField] private LayerMask _whatIsDestroy;

    private void OnTriggerEnter2D(Collider2D other) {
        if((_whatIsPlayer.value & (1 << other.gameObject.layer)) > 1f) {
            if(other.gameObject.TryGetComponent(out IDamageable health)) {
                health.ApplyDamage(1, transform);
            }
        }

        if((_whatIsDestroy.value & (1 << other.gameObject.layer)) > 1f) {
            if(other.gameObject.TryGetComponent(out IDamageable health)) {
                Destroy(gameObject);
            }
        }
    }
}
