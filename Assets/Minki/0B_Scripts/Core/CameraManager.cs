using Cinemachine;

public class CameraManager : MonoSingleton<CameraManager>
{
    private CinemachineImpulseSource _source;

    private void Awake() {
        _source = GetComponent<CinemachineImpulseSource>();
    }

    public void Shake(float impulsePower, float time = 0.2f) {
        _source.GenerateImpulse(impulsePower);
        _source.m_ImpulseDefinition.m_ImpulseDuration = time;
    }
}
