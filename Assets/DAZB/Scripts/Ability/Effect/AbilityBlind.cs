using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(menuName = "SO/Ability/Blind")]
public class AbilityBlind : AbilityEffectSO
{
    public int IncreaseAttackDamage;
    
    public override void ApplyEffect()
    {
        if (!AlreadyApplied) {
            AlreadyApplied = true;
            PlayerManager.Instance.Player.attackDamage += IncreaseAttackDamage;
            PlayerManager.Instance.Head.StartCoroutine(BlindRoutine());
        }
    }

    private IEnumerator BlindRoutine() {
        Light2D light = PlayerManager.Instance.Player.transform.Find("Light 2D").GetComponent<Light2D>();
        light.gameObject.SetActive(true);
        light.GetComponentInChildren<SpriteRenderer>(false).transform.SetParent(PlayerManager.Instance.Head.transform);
        float currentTime = 0;
        float totalTime = 1f;
        float percent = 0;
        while (true) {
            if (percent >= 1) {
                light.pointLightOuterRadius = 3f;
                break;
            }
            light.pointLightOuterRadius = Mathf.Lerp(100, 4, currentTime / totalTime);
            currentTime += Time.deltaTime;
            yield return null;
        }
    }
}