using UnityEngine;

public class RestRoom : Room
{
    [SerializeField] private IInteractable _interact;
    
    private void Start() {
        if(_interact != null)
            _interact.OnInteractEnd += Clear;
    }
}
