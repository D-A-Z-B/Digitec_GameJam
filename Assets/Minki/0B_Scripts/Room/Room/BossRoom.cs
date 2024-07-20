using UnityEngine;

public class BossRoom : Room
{
    [SerializeField] private DinoTimeline _timeline;

    private void Start() {
        OnActive += HandleBoss;
    }

    private void HandleBoss() {
        _timeline.StartTimeline();
    }
}
