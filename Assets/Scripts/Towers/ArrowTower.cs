using UnityEngine;

public class ArrowTower : Tower
{
    public Arrow _arrowPrefab;
    public Transform firePoint;
    public float _fireRate = 1f;
    public float range = 5f;

    private float _fireCooldown = 0f;
    private Enemy currentTarget;

    void Update() {
        _fireCooldown -= Time.deltaTime;

        if(currentTarget == null || !IsInRange(currentTarget)) {
            FindTarget();
        }

        if(currentTarget != null && _fireCooldown <= 0f) {
            Shoot();
            _fireCooldown = 1f / _fireRate;
        }
    }

    void Shoot() {
        Arrow arrow = Instantiate(_arrowPrefab,firePoint.position,Quaternion.identity);
        arrow.SetTarget(currentTarget.transform);
        arrow.Damage = _attackPower;
    }

    bool IsInRange(Enemy enemy) {
        return Vector2.Distance(transform.position,enemy.transform.position) <= range;
    }

    void FindTarget() {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        float closestDistance = Mathf.Infinity;
        Enemy closestEnemy = null;

        foreach(var enemy in enemies) {
            float dist = Vector2.Distance(transform.position,enemy.transform.position);
            if(dist < closestDistance && dist <= range) {
                closestDistance = dist;
                closestEnemy = enemy;
            }
        }

        currentTarget = closestEnemy;
    }
}
