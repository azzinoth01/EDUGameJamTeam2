using UnityEngine;

public abstract class Unit : MonoBehaviour, IHealth
{
    [SerializeField] protected float _health;
    protected float _currentHealth;
    [SerializeField] protected int _goldOnDeath;

    [SerializeField] DespawnAfterTime _deadVisual;

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
        if(_health <= 0) {
            return;
        }
        _currentHealth = _currentHealth - damage;
        if(_currentHealth <= 0) {
            Death();
        }
    }

    private void Death() {
        GameInstance.Instance.Player.AddGold(_goldOnDeath);
        if(_deadVisual != null) {
            Instantiate(_deadVisual,transform.position,Quaternion.identity);
        }
        Destroy(gameObject);
    }
    protected virtual void Awake() {
        _currentHealth = _health;
    }


    public bool IsDead() {
        return _health <= 0;
    }

}
