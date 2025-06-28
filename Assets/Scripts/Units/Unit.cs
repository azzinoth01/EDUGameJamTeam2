using UnityEngine;

public abstract class Unit : MonoBehaviour, IHealth
{
    [SerializeField] protected float _health;
    protected float _currentHealth;
    [SerializeField] protected int _goldOnDeath;

    public float Health {
        get {
            return _currentHealth;
        }
    }

    public float MaxHealth {
        get {
            return _health;
        }
    }

    public void TakeDamage(float damage) {
        _currentHealth = _currentHealth - damage;
        if(_currentHealth <= 0) {
            Death();
        }
    }

    private void Death() {
        GameInstance.Instance.Player.AddGold(_goldOnDeath);
        Destroy(gameObject);
    }
    protected virtual void Awake() {
        _currentHealth = _health;
    }


    public bool IsDead() {
        return _health <= 0;
    }

}
