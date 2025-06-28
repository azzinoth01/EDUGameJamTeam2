using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] protected float _health;
    [SerializeField] protected int _attackPower;
    [SerializeField] protected int _goldOnDeath;


    public void TakeDmg(float damage) {

        _health = _health - damage;
        if(_health <= 0) {
            Death();
        }
    }

    private void Death() {
        GameInstance.Instance.Player.GetGold(_goldOnDeath);
        Destroy(gameObject);
    }
}
