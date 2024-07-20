using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class DinoTimeline : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GameObject _boss;

    private PlayableDirector _timeline;

    private void Awake() {
        _timeline = GetComponent<PlayableDirector>();
    }

    public void StartTimeline() {
        StartCoroutine(TimelineCoroutine());
        _timeline.Play();
    }

    private IEnumerator TimelineCoroutine() {
        _inputReader.PlayerInputDisable();

        yield return new WaitForSeconds(2f);

        //Warning

        yield return new WaitForSeconds(1f);

        CameraManager.Instance.Shake(5f, 3f);

        yield return new WaitForSeconds(3f);

        _inputReader.PlayerInputEnable();

        _boss.SetActive(true);
        gameObject.SetActive(false);
    }
}
