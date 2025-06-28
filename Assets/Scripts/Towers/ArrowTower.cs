using UnityEngine;

public class ArrowTower : MonoBehaviour
{
    public GameObject arrow;
    public Transform Tower;
    public float fireRate = 1f;
    public float range = 5f;
    public Transform baseTarget;

    private float fireCD = 0f;
    private Enemy target;
    private float deadWait = 0f;
    public float waitTimeAfterKill = 1f;

    void Update()
    {
        fireCD -= Time.deltaTime;

        if (target == null || target.IsDead() || !IsInRange(target))
        {
            if (deadWait > 0)
            {
                deadWait -= Time.deltaTime;
                return;
            }

            FindTarget();
        }

        if (target != null && fireCD <= 0f)
        {
            Shoot();
            fireCD = 1f / fireRate;

            if (target.IsDead())
                deadWait = waitTimeAfterKill;
        }
    }

    void Shoot()
    {
        if (arrow == null || target == null) return;

        GameObject spawnedArrow = Instantiate(arrow, Tower.position, Quaternion.identity);
        spawnedArrow.GetComponent<Arrow>().SetTarget(target.transform);
    }

    void FindTarget()
    {
        Enemy[] allEnemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        float nearestToBase = Mathf.Infinity;
        Enemy chosen = null;

        foreach (Enemy e in allEnemies)
        {
            float distToBase = Vector2.Distance(e.transform.position, baseTarget.position);
            float distToMe = Vector2.Distance(transform.position, e.transform.position);

            if (distToMe <= range && distToBase < nearestToBase)
            {
                nearestToBase = distToBase;
                chosen = e;
            }
        }

        target = chosen;
    }

    bool IsInRange(Enemy e)
    {
        return Vector2.Distance(transform.position, e.transform.position) <= range;
    }
}
