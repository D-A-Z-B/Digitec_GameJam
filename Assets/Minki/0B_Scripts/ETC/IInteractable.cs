using System;

public interface IInteractable
{
    public event Action OnInteractEnd;

    public void Interact();
    public void Enter();
    public void Exit();
}
