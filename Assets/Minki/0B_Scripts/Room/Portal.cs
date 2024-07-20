using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsPlayer;

    private Room _owner;

    public void SetOwner(Room room) {
        _owner = room;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if((_whatIsPlayer.value & (1 << other.gameObject.layer)) > 0)
            _owner.GotoNextStage();
    }
}
