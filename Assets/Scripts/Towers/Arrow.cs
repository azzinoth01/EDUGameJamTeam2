using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    private float _damage = 10f;
    private Transform _target;

    public float Damage {
        get {
            return _damage;
        }

        set {
            _damage = value;
        }
    }

    public void SetTarget(Transform target) {
        _target = target;
    }

    void Update() {
        if(_target == null) {
            Destroy(gameObject);
            return;
        }

        Vector2 dir = _target.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(Vector3.forward,dir);

        transform.rotation = rotation;

        float moveDistance = speed * Time.deltaTime;

        if(dir.magnitude <= moveDistance) {
            HitTarget();
            return;
        }
        transform.position = transform.position + (Vector3) dir.normalized * moveDistance;
    }

    void HitTarget() {

        if(_target.TryGetComponent(out Unit enemy)) {
            enemy.TakeDamage(_damage);
        }
        else if(_target.TryGetComponent(out Tower unit)) {
            unit.TakeDamage(_damage);
        }
        Destroy(gameObject);
    }
}
