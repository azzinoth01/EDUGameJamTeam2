using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 10f;
    public enum ArrowType { Normal, Slow, Poison, Freeze }
    public ArrowType arrowType = ArrowType.Normal;

    private Transform target;

    public void SetTarget(Transform t)
    {
        target = t;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        Enemy e = target.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(damage);

            switch (arrowType)
            {
                case ArrowType.Slow:
                    e.Slow(0.5f, 2f); // Hızı %50'ye düşür, 2 saniye boyunca
                    break;

                case ArrowType.Poison:
                    StartCoroutine(ApplyPoison(e, 2f, 5f)); // 2 saniye boyunca her saniye 5 hasar
                    break;

                case ArrowType.Freeze:
                    e.Freeze(0.5f); // 0.5 saniye dondur
                    break;
            }
        }

        Destroy(gameObject);
    }

    IEnumerator ApplyPoison(Enemy enemy, float dps, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            if (enemy != null && !enemy.IsDead())
            {
                enemy.TakeDamage(dps);
            }

            yield return new WaitForSeconds(0.5f);
            elapsed += 0.5f;
        }
    }
}
