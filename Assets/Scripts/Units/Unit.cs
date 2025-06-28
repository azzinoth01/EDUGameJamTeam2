using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] protected float _health;
    [SerializeField] protected int _attackPower;


    public void TakeDmg(float damage) {

        _health = _health - damage;
        if(_health <= 0) {
            Destroy(gameObject);
        }
    }
}
