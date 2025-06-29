using UnityEngine;

public class ArrowTower : Tower
{
    public Arrow _arrowPrefab;
    public Transform firePoint;
    public float _fireRate = 1f;

    private float fireCD = 0f;
    private Enemy target;
    private float deadWait = 0f;
    public float waitTimeAfterKill = 1f;

    [SerializeField] private EnemyInRangeDetection _rangeDetection;


    protected virtual void Update() {
        fireCD -= Time.deltaTime;

        if(target == null || target.IsDead() || !_rangeDetection.IsInRange(target)) {
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
        if(_animationController != null) {
            _animationController.SetTrigger("Shoot");
        }
        Arrow arrow = Instantiate(_arrowPrefab,firePoint.position,Quaternion.identity);
        arrow.SetTarget(target.transform);
        arrow.Damage = _attackPower;
    }


    void FindTarget() {
        float nearestToBase = Mathf.Infinity;
        Enemy chosen = null;

        foreach(Enemy enemy in _rangeDetection.EnemiesInRange) {
            float distToBase = enemy.GetDistanceToBase();

            if(distToBase < nearestToBase) {
                nearestToBase = distToBase;
                chosen = enemy;
            }
        }

        target = chosen;
    }
}
