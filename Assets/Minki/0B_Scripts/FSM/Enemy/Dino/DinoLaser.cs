using UnityEngine;

public class DinoLaser : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsPlayer;

    private void OnTriggerEnter2D(Collider2D other) {
        if((_whatIsPlayer.value & (1 << other.gameObject.layer)) > 0) {
            if(other.TryGetComponent(out IDamageable health)) {
                health.ApplyDamage(1, transform);
            }
        }
    }
}
