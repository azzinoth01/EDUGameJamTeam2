using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected float _health;
    [SerializeField] protected int _goldOnDeath;


    public void TakeDamage(float damage) {
        _health = _health - damage;
        if(_health <= 0) {
            Death();
        }
    }

    private void Death() {
        GameInstance.Instance.Player.AddGold(_goldOnDeath);
        Destroy(gameObject);
    }
}
