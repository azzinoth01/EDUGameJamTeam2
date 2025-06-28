using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(SplineAnimate))]
public class Enemy : Unit
{
    private SplineContainer _movePath;
    protected SplineAnimate _animate;

    [SerializeField] protected float _moveSpeed = 3f;


    [SerializeField] private int _spawnCost;

    [SerializeField] private int _damageToThrone;
    [SerializeField] protected int _attackDamage;


    private float originalSpeed;

    private Coroutine slowRoutine;
    private Coroutine freezeRoutine;
    private Coroutine poisonRoutine;

    public int SpawnCost {
        get {
            return _spawnCost;
        }
    }



    protected virtual void Start() {
        _movePath = GameInstance.Instance.MovePaths.GetRandomMovePath();
        _animate = GetComponent<SplineAnimate>();
        _animate.Container = _movePath;

        _animate.AnimationMethod = SplineAnimate.Method.Speed;
        _animate.MaxSpeed = _moveSpeed;
        originalSpeed = _moveSpeed;

        SplineUtility.GetNearestPoint(_movePath.Spline,transform.position,out float3 position,out float distanceOnSpline);

        _animate.StartOffset = distanceOnSpline;

        _animate.Play();

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
        if(collisionGameObject.TryGetComponent(out Tower unit)) {
            unit.TakeDamage(_damageToThrone);
            Destroy(gameObject);
        }
    }


}
