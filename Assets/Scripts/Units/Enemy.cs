using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(SplineAnimate))]
public class Enemy : Unit
{
    private SplineContainer _movePath;
    [SerializeField] private float _moveSpeed;
    private SplineAnimate _animate;


    private void Start() {
        _movePath = GameInstance.Instance.MovePaths.GetRandomMovePath();
        _animate = gameObject.GetComponent<SplineAnimate>();
        _animate.Container = _movePath;

        _animate.AnimationMethod = SplineAnimate.Method.Speed;
        _animate.MaxSpeed = _moveSpeed;
        _animate.Play();
    }

    private void Update() {

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject collisionGameObject = collision.collider.gameObject;
        if(collisionGameObject.TryGetComponent(out Unit unit)) {
            unit.TakeDmg(_attackPower);
            Destroy(gameObject);
        }
    }
}
