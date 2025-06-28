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

    [SerializeField] float _pushBackDistanceOnCollison;

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

        SplineUtility.GetNearestPoint(_movePath.Spline,transform.position,out float3 position,out float distanceOnSpline);

        _animate.StartOffset = distanceOnSpline;

        _animate.Play();

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject collisionGameObject = collision.collider.gameObject;
        if(collisionGameObject.TryGetComponent(out Base unit)) {
            unit.TakeDamage(_damageToThrone);
            float lenght = _animate.Container.Spline.GetLength();
            float lerp = _pushBackDistanceOnCollison / lenght;
            _animate.NormalizedTime = _animate.NormalizedTime - lerp;

        }
    }


}
