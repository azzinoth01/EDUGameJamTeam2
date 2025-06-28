using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(SplineAnimate))]
public class Enemy : Unit
{
    private SplineContainer _movePath;
    private SplineAnimate _animate;

    [SerializeField] private float _moveSpeed = 3f;

    [SerializeField] private int _spawnCost;

    [SerializeField] private int _damageToThrone;
    [SerializeField] private int _attackDamage;
    [SerializeField] private Arrow _arrow;

    [SerializeField] private float _fireRate;
    private float _fireTime;
    private float _passedTimeShooting;
    private Transform _attackTarget;

    private float originalSpeed;

    private Coroutine slowRoutine;
    private Coroutine freezeRoutine;
    private Coroutine poisonRoutine;

    public int SpawnCost {
        get {
            return _spawnCost;
        }
    }

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

    private void Start() {
        _movePath = GameInstance.Instance.MovePaths.GetRandomMovePath();
        _animate = GetComponent<SplineAnimate>();
        _animate.Container = _movePath;

        _animate.AnimationMethod = SplineAnimate.Method.Speed;
        _animate.MaxSpeed = _moveSpeed;
        originalSpeed = _moveSpeed;

        SplineUtility.GetNearestPoint(_movePath.Spline,transform.position,out float3 position,out float distanceOnSpline);

        _animate.StartOffset = distanceOnSpline;

        _animate.Play();
        UpdateFireTime();
    }

    private void Update() {
        ShootArrow();
    }

    private void ShootArrow() {
        _passedTimeShooting = _passedTimeShooting + Time.deltaTime;
        if(_fireTime > _passedTimeShooting) {
            return;
        }
        _passedTimeShooting = _passedTimeShooting - _fireTime;

        SpawnArrow(_attackTarget);
    }


    public bool IsDead() {
        return _health <= 0;
    }

    // Freeze (dondurma)
    public void Freeze(float duration) {
        if(freezeRoutine != null)
            StopCoroutine(freezeRoutine);

        freezeRoutine = StartCoroutine(FreezeRoutine(duration));
    }

    IEnumerator FreezeRoutine(float duration) {
        float prevSpeed = _animate.MaxSpeed;

        _animate.MaxSpeed = 0.01f; // pratikte 0 sayılır
        yield return new WaitForSeconds(duration);

        _animate.MaxSpeed = prevSpeed;
        freezeRoutine = null;
    }

    // Slow (yavaşlatma)
    public void Slow(float multiplier,float duration) {
        if(slowRoutine != null)
            StopCoroutine(slowRoutine);

        slowRoutine = StartCoroutine(SlowRoutine(multiplier,duration));
    }

    IEnumerator SlowRoutine(float multiplier,float duration) {
        _animate.MaxSpeed = originalSpeed * multiplier;
        yield return new WaitForSeconds(duration);
        _animate.MaxSpeed = originalSpeed;
        slowRoutine = null;
    }

    public void ApplyPoison(int dps,float duration) {
        if(poisonRoutine != null)
            StopCoroutine(poisonRoutine);

        poisonRoutine = StartCoroutine(PoisonDamageRoutine(dps,duration));
    }

    IEnumerator PoisonDamageRoutine(float dps,float duration) {
        float elapsed = 0f;
        while(elapsed < duration) {
            if(!IsDead())
                TakeDamage(dps);

            yield return new WaitForSeconds(1f);
            elapsed += 1f;
        }

        poisonRoutine = null;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject collisionGameObject = collision.collider.gameObject;
        if(collisionGameObject.TryGetComponent(out Unit unit)) {
            unit.TakeDamage(_damageToThrone);
            Destroy(gameObject);
        }
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
