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
    [SerializeField] float _pushBackDuration;
    private float _passedTimePushBack;
    private float _moveBackPerSecond;
    protected bool _pushBackOnSpline;

    public int SpawnCost {
        get {
            return _spawnCost;
        }
    }
    protected virtual void Update() {
        if(_pushBackOnSpline) {
            _animate.Pause();
            PushBack();
        }

    }
    private void PushBack() {

        _passedTimePushBack = _passedTimePushBack + Time.deltaTime;
        _animate.NormalizedTime = _animate.NormalizedTime - _moveBackPerSecond * Time.deltaTime;

        if(_passedTimePushBack >= _pushBackDuration) {
            _pushBackOnSpline = false;
            _animate.Play();
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
        CalculatePushBack();
    }

    private void CalculatePushBack() {
        float lenght = _animate.Container.Spline.GetLength();
        float lerp = _pushBackDistanceOnCollison / lenght;
        _moveBackPerSecond = lerp / _pushBackDuration;
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject collisionGameObject = collision.collider.gameObject;
        if(collisionGameObject.TryGetComponent(out Base unit)) {
            unit.TakeDamage(_damageToThrone);
            _passedTimePushBack = 0;
            _pushBackOnSpline = true;
            _animate.Pause();
        }
    }


}
