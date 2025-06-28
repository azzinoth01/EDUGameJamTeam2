using UnityEngine;

public class ArrowTower : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public float range = 5f;

    private float fireCooldown = 0f;
    private Enemy currentTarget;

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (currentTarget == null || !IsInRange(currentTarget))
        {
            FindTarget();
        }

        if (currentTarget != null && fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / fireRate;
        }
    }

    void Shoot()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
        arrow.GetComponent<Arrow>().SetTarget(currentTarget.transform);
    }

    bool IsInRange(Enemy enemy)
    {
        return Vector2.Distance(transform.position, enemy.transform.position) <= range;
    }

    void FindTarget()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        float closestDistance = Mathf.Infinity;
        Enemy closestEnemy = null;

        foreach (var enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < closestDistance && dist <= range)
            {
                closestDistance = dist;
                closestEnemy = enemy;
            }
        }

        currentTarget = closestEnemy;
    }
}
