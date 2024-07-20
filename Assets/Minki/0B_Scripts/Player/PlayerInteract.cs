using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float _checkRadius = 2.5f;
    [SerializeField] private LayerMask _whatIsInteractable;

    private Collider2D[] _colliders = new Collider2D[1];

    private IInteractable _interact;

    private void Update() {
        int count = Physics2D.OverlapCircleNonAlloc(transform.position, _checkRadius, _colliders, _whatIsInteractable);

        if(count > 0) {
            if(_colliders[0].TryGetComponent(out IInteractable interact)) {
                if(_interact == null) {
                    _interact = interact;

                    _interact.Enter();
                }
            }
        }
        else {
            _interact?.Exit();
            _interact = null;
        }
    }

    #if UNITY_EDITOR

    private void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _checkRadius);
    }

    #endif
}
