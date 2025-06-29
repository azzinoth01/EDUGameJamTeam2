using UnityEngine;

public class ArrowTower : Tower
{
    public Arrow _arrowPrefab;
    public Transform firePoint;
    public float _fireRate = 1f;
    public Transform baseTarget;
    private float fireCD = 0f;
    private Enemy target;
    private float deadWait = 0f;
    public float waitTimeAfterKill = 1f;

    [SerializeField] private EnemyInRangeDetection _rangeDetection;
    [SerializeField] private Animator _animationController;


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
        _animationController.SetTrigger("Shoot");
        Arrow arrow = Instantiate(_arrowPrefab,firePoint.position,Quaternion.identity);
        arrow.SetTarget(target.transform);
        arrow.Damage = _attackPower;
    }


    void FindTarget() {
        float nearestToBase = Mathf.Infinity;
        Enemy chosen = null;

        foreach(Enemy e in _rangeDetection.EnemiesInRange) {
            float distToBase = Vector2.Distance(e.transform.position,baseTarget.position);


            if(distToBase < nearestToBase) {
                nearestToBase = distToBase;
                chosen = e;
            }
        }

        target = chosen;
    }
}
