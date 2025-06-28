using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    private float _damage = 10f;
    //public enum ArrowType
    //{
    //    Normal, Slow, Poison, Freeze
    //}
    //public ArrowType arrowType = ArrowType.Normal;

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
        float moveDistance = speed * Time.deltaTime;

        if(dir.magnitude <= moveDistance) {
            HitTarget();
            return;
        }
        transform.position = transform.position + (Vector3) dir.normalized * moveDistance;
    }

    void HitTarget() {

        if(_target.TryGetComponent(out Enemy enemy)) {
            enemy.TakeDamage(_damage);
        }
        else if(_target.TryGetComponent(out Unit unit)) {
            unit.TakeDamage(_damage);
        }


        //switch(arrowType) {
        //    case ArrowType.Slow:
        //        enemy.Slow(0.5f,2f); // Hızı %50'ye düşür, 2 saniye boyunca
        //        break;

        //    case ArrowType.Poison:
        //        StartCoroutine(ApplyPoison(enemy,2f,5f)); // 2 saniye boyunca her saniye 5 hasar
        //        break;

        //    case ArrowType.Freeze:
        //        enemy.Freeze(0.5f); // 0.5 saniye dondur
        //        break;
        //}


        Destroy(gameObject);
    }
}
