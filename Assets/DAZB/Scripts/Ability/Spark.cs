using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : MonoBehaviour
{
    [SerializeField] private float checkRadius = 5f;
    [SerializeField] private float followSpeed = 2f;
    [SerializeField] private LayerMask enemyLayer;
    private GameObject targetEnemy;
    private Collider2D[] result = new Collider2D[10];
    private Rigidbody2D rigid;

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        rigid.velocity = Vector2.zero;
        NearEnemyCheck();
        StartCoroutine(SparkRoutine());
    }

    private IEnumerator SparkRoutine()
    {
        rigid.AddForce(Random.insideUnitCircle * 5, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        Vector2 starPos = transform.position;
        Vector2 endPos = targetEnemy.transform.position;

        float currentTime = 0;
        float totalTime = 1 / followSpeed;
        while (true) {
            endPos = targetEnemy.transform.position;
            if (Vector2.Distance(transform.position, endPos) <= 0.1f) {
                Destroy(gameObject);
                yield break;
            }
            float t = EaseOutQuart(currentTime / totalTime);
            transform.position = Vector2.Lerp(starPos, endPos, t);
            yield return null;
            currentTime += Time.deltaTime;
        }
    }

    private void NearEnemyCheck() {
        int numEnemies = Physics2D.OverlapCircleNonAlloc(transform.position, checkRadius, result, enemyLayer);
        if (numEnemies > 0) {
            targetEnemy = result[0].gameObject;
        } else {
            targetEnemy = null;
        }
    }

    private float EaseOutQuart(float t)
    {
        return 1 - Mathf.Pow(1 - t, 4);
    }


    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
