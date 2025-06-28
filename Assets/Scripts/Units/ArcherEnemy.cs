using UnityEngine;

public class ArcherEnemy : Enemy
{

    [SerializeField] private float _speedWhileAttacking;
    [SerializeField] private Arrow _arrow;

    [SerializeField] private float _fireRate;
    private float _fireTime;
    private float _passedTimeShooting;
    private Transform _attackTarget;

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



    private void Update() {
        ShootArrow();
        if(_attackTarget != null) {
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


    private void ShootArrow() {
        _passedTimeShooting = _passedTimeShooting + Time.deltaTime;
        if(_fireTime > _passedTimeShooting) {
            return;
        }
        _passedTimeShooting = _passedTimeShooting - _fireTime;

        SpawnArrow(_attackTarget);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(_arrow == null) {
            return;
        }
        _attackTarget = collision.gameObject.transform;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if(_arrow == null) {
            return;
        }
        if(_attackTarget == null) {
            _attackTarget = collision.gameObject.transform;
        }
        if(_attackTarget.TryGetComponent(out Tower tower)) {
            if(tower.IsAlive()) {
                return;
            }
            else {
                _attackTarget = collision.gameObject.transform;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        _attackTarget = null;
    }

    private void SpawnArrow(Transform target) {
        if(target == null) {
            return;
        }

        Arrow arrow = Instantiate(_arrow,transform.position,Quaternion.identity);
        arrow.SetTarget(target);
        arrow.Damage = _attackDamage;
    }
}