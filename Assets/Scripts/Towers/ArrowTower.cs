using UnityEngine;

public class ArrowTower : Tower
{
    public Arrow _arrowPrefab;
    public Transform firePoint;
    public float _fireRate = 1f;
    public float range = 5f;
    public Transform baseTarget;

    private float fireCD = 0f;
    private Enemy target;
    private float deadWait = 0f;
    public float waitTimeAfterKill = 1f;

    protected virtual void Update() {
        fireCD -= Time.deltaTime;

        if(target == null || target.IsDead() || !IsInRange(target)) {
            if(deadWait > 0) {
                deadWait -= Time.deltaTime;
                return;
            }

            FindTarget();
        }

        if(target != null && fireCD <= 0f) {
            Shoot();
            fireCD = 1f / _fireRate;

            if(target.IsDead())
                deadWait = waitTimeAfterKill;
        }
    }

    void Shoot() {
        if(_arrowPrefab == null || target == null) {
            return;
        }
        Arrow arrow = Instantiate(_arrowPrefab,firePoint.position,Quaternion.identity);
        arrow.SetTarget(target.transform);
        arrow.Damage = _attackPower;
    }

    bool IsInRange(Enemy enemy) {
        return Vector2.Distance(transform.position,enemy.transform.position) <= range;
    }

    void FindTarget() {
        Enemy[] allEnemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        float nearestToBase = Mathf.Infinity;
        Enemy chosen = null;

        foreach(Enemy e in allEnemies) {
            float distToBase = Vector2.Distance(e.transform.position,baseTarget.position);
            float distToMe = Vector2.Distance(transform.position,e.transform.position);

            if(distToMe <= range && distToBase < nearestToBase) {
                nearestToBase = distToBase;
                chosen = e;
            }
        }

        target = chosen;
    }
}
