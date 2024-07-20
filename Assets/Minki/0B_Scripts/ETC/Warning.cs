using UnityEngine;

public class Warning : MonoBehaviour
{
    [SerializeField] private GameObject _redBoxPrefab;

    public void ShowRange(Vector2 position, Vector2 range, float time) {
        GameObject obj = Instantiate(_redBoxPrefab, transform);
        obj.transform.localPosition = position;
        obj.transform.localScale = range;

        Destroy(obj, time);
    }
}
