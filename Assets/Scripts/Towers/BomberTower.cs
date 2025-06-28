using UnityEngine;

public class BomberTower : MonoBehaviour
{
    public GameObject bombPrefab;
    public Transform firePoint;
    public float range = 5f;
    public float fireRate = 1f;

    private float fireCooldown = 0f;
    private Enemy currentTarget;

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (currentTarget == null || !IsInRange(currentTarget))
            FindTarget();

        if (currentTarget != null && fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / fireRate;
        }
    }

    void Shoot()
    {
        GameObject bomb = Instantiate(bombPrefab, firePoint.position, Quaternion.identity);
        bomb.GetComponent<Bomb>().SetTarget(currentTarget.transform);
    }

    bool IsInRange(Enemy enemy)
    {
        return Vector2.Distance(transform.position, enemy.transform.position) <= range;
    }

    void FindTarget()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        float closestDist = Mathf.Infinity;
        Enemy closestEnemy = null;

        foreach (Enemy enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < closestDist && dist <= range)
            {
                closestDist = dist;
                closestEnemy = enemy;
            }
        }

        currentTarget = closestEnemy;
    }
}
