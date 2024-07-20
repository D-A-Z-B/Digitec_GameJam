using System;

public interface IInteractable
{
    public event Action OnInteractEnd;

    public void Interact();
}
