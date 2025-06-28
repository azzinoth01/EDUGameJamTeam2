using UnityEngine;

public class InfernoTower : Tower
{
    public float damagePerSecond = 5f;
    public float damageIncreasePerSecond = 2f;
    public float cdDuration = 1f;
    public LineRenderer laserRenderer;
    public Transform baseTarget;
    private Enemy currentTarget;
    private float currentDamage;
    private float _targetCoolddownPassedTime = 0f;

    [SerializeField] private EnemyInRangeDetection _rangeDetection;

    private bool _canFindNextTarget = true;

    void Update() {
        if(currentTarget != null) {
            CheckCurrentTargetDeadOrOutOfRange();
        }
        UpdateTargetinCooldown();
        StopLaser();
        FindTarget();
        DamageTarget();
        Debug.Log(_canFindNextTarget);
    }

    private void UpdateTargetinCooldown() {
        _targetCoolddownPassedTime = _targetCoolddownPassedTime + Time.deltaTime;
        if(cdDuration > _targetCoolddownPassedTime) {
            return;
        }
        _canFindNextTarget = true;
        return;
    }
    private void StopLaser() {
        if(currentTarget == null) {
            StopDrawingLaser();
        }
    }
    private void CheckCurrentTargetDeadOrOutOfRange() {
        if(!_rangeDetection.IsInRange(currentTarget) || currentTarget.IsDead()) {
            currentTarget = null;
            currentDamage = damagePerSecond;
            _targetCoolddownPassedTime = 0;
            _canFindNextTarget = false;
        }
    }
    void DamageTarget() {
        if(currentTarget == null) {
            return;
        }
        DrawLaser();
        currentTarget.TakeDamage(currentDamage * Time.deltaTime);
        currentDamage += damageIncreasePerSecond * Time.deltaTime;
        CheckCurrentTargetDeadOrOutOfRange();
    }

    void FindTarget() {
        if(_canFindNextTarget == false || currentTarget != null) {
            return;
        }
        float closestToBase = Mathf.Infinity;
        Enemy mostAdvancedEnemy = null;

        foreach(var enemy in _rangeDetection.EnemiesInRange) {
            float distToBase = Vector2.Distance(enemy.transform.position,baseTarget.position);

            if(distToBase < closestToBase) {
                closestToBase = distToBase;
                mostAdvancedEnemy = enemy;
            }
        }

        currentTarget = mostAdvancedEnemy;
    }
    void DrawLaser() {
        if(laserRenderer != null && currentTarget != null) {
            laserRenderer.enabled = true;
            laserRenderer.SetPosition(0,transform.position);
            laserRenderer.SetPosition(1,currentTarget.transform.position);
        }
    }

    void StopDrawingLaser() {
        if(laserRenderer != null)
            laserRenderer.enabled = false;
    }
    protected override void Death() {
        base.Death();
        StopDrawingLaser();
    }
    public override void Respawn() {
        base.Respawn();
        ResetTowerState();
    }
    void ResetTowerState() {
        currentTarget = null;
        currentDamage = damagePerSecond;
        _targetCoolddownPassedTime = 0f;
        StopLaser();
    }
}
