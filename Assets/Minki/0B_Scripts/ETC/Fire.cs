using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsPlayer;

    private Animator _animator;
    private Collider2D _collider;

    private readonly int disappearHash = Animator.StringToHash("Disappear");

    private void Awake() {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if((_whatIsPlayer & (1 << other.gameObject.layer)) > 0) {
            if(other.TryGetComponent(out Head player)) {
                player.HealthCompo.IncreaseHealth(1);
                _collider.enabled = false;

                _animator.SetTrigger(disappearHash);
                Destroy(gameObject);
            }
        }
    }
}
