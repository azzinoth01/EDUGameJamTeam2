using UnityEngine;

public class InfernoTower : Tower
{
    public float damagePerSecond = 5f;
    public float damageIncreasePerSecond = 2f;
    public float cdDuration = 1f;
    public Transform baseTarget;
    private Enemy currentTarget;
    private float currentDamage;
    private float _targetCoolddownPassedTime = 0f;

    [SerializeField] private Transform _shootingPoint;

    [SerializeField] private SpriteRenderer _laserSprite;

    [SerializeField] private EnemyInRangeDetection _rangeDetection;


    private bool _canFindNextTarget = true;

    protected override void Awake() {
        base.Awake();
        _laserSprite = Instantiate(_laserSprite);
        _laserSprite.gameObject.SetActive(false);
    }


    void Update() {
        if(currentTarget != null) {
            CheckCurrentTargetDeadOrOutOfRange();
        }
        UpdateTargetinCooldown();
        StopLaser();
        FindTarget();
        DamageTarget();

    }

    private void UpdateTargetinCooldown() {
        _targetCoolddownPassedTime = _targetCoolddownPassedTime + Time.deltaTime;
        if(cdDuration > _targetCoolddownPassedTime) {
            return;
        }
        _animationController.SetBool("IsOverHeating",false);
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
            _animationController.SetBool("IsOverHeating",true);
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

        if(currentTarget == null) {
            return;
        }
        _animationController.SetBool("IsAttacking",true);
        Vector2 dir = currentTarget.transform.position - _shootingPoint.position;
        float distance = dir.magnitude;

        Vector2 spritePosition = Vector2.Lerp(currentTarget.transform.position,_shootingPoint.position,0.5f);
        _laserSprite.transform.position = spritePosition;

        Quaternion rotation = Quaternion.LookRotation(Vector3.forward,dir.normalized);
        _laserSprite.transform.rotation = rotation;

        Vector3 scale = _laserSprite.transform.localScale;
        _laserSprite.transform.localScale = new Vector3(scale.x,distance,scale.z);
        _laserSprite.gameObject.SetActive(true);
    }

    void StopDrawingLaser() {
        _laserSprite.gameObject.SetActive(false);
        _animationController.SetBool("IsAttacking",false);

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
