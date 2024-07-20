using UnityEngine;

public interface IMovement {
    public void Initialize(Agent agent);
    public void StopImmediately();
    public void SetMovement(Vector3 movement);
}