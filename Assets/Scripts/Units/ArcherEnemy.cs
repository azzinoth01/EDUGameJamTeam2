using UnityEngine;

public class ArcherEnemy : Enemy
{

    [SerializeField] private float _speedWhileAttacking;
    [SerializeField] private Arrow _arrow;

    [SerializeField] private float _fireRate;
    private float _fireTime;
    private float _passedTimeShooting;

    [SerializeField] private TowerInRangeDetection _towerInRangeDetection;
    [SerializeField] private Animator _animationController;

    private const string _shootAnimation = "Shoot";

    public float FireRate {
        get {
            return _fireRate;
        }

        set {
            _fireRate = value;
            UpdateFireTime();
        }
    }
    private void UpdateFireTime() {
        _fireTime = 1f / _fireRate;
    }
    protected override void Start() {
        base.Start();
        UpdateFireTime();
    }



    protected override void Update() {
        base.Update();
        if(_pushBackOnSpline) {
            return;
        }



        Transform attackTarget = _towerInRangeDetection.GetFirstTower()?.transform;
        ShootArrow(attackTarget);

        if(attackTarget != null) {
            if(_speedWhileAttacking == 0f) {
                _animate.Pause();
            }
            float normalizedTime = _animate.NormalizedTime;
            _animate.MaxSpeed = _speedWhileAttacking;
            _animate.NormalizedTime = normalizedTime;
        }
        else {

            float normalizedTime = _animate.NormalizedTime;
            _animate.MaxSpeed = _moveSpeed;
            _animate.NormalizedTime = normalizedTime;
            if(_animate.IsPlaying == false) {
                _animate.Play();
            }
        }
    }


    private void ShootArrow(Transform target) {
        _passedTimeShooting = _passedTimeShooting + Time.deltaTime;
        if(_fireTime > _passedTimeShooting) {
            return;
        }
        _passedTimeShooting = _passedTimeShooting - _fireTime;

        SpawnArrow(target);
    }

    private void SpawnArrow(Transform target) {
        if(target == null || _arrow == null) {
            return;
        }
        _animationController.SetTrigger(_shootAnimation);
        Arrow arrow = Instantiate(_arrow,transform.position,Quaternion.identity);
        arrow.SetTarget(target);
        arrow.Damage = _attackDamage;
    }
}