using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] protected int _health;
    [SerializeField] protected int _attackPower;


    public void TakeDmg(int damage) {

        _health = _health - damage;
        if(_health <= 0) {
            Destroy(gameObject);
        }
    }
}
