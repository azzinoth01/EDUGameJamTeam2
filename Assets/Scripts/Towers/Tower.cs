using UnityEngine;

public class Tower : MonoBehaviour, IHealth
{
    [SerializeField] private int _goldOnDeath;
    [SerializeField] private int _baseHealth;
    private float _health;
    [SerializeField] private float _respawnTime;
    [SerializeField] protected float _attackPower;
    [SerializeField] private CooldownHandler _cooldownHandler;
    [SerializeField] protected Animator _animationController;

    public float Health {
        get {
            return _health;
        }
    }

    public float MaxHealth {
        get {
            return _baseHealth;
        }
    }

    protected virtual void Awake() {
        _health = _baseHealth;
    }

    private void OnDestroy() {
        GameInstance.Instance.Player.AddGold(_goldOnDeath);
    }

    public void TakeDamage(float damage) {

        if(_health <= 0) {
            return;
        }
        _health = _health - damage;

        if(_health <= 0) {
            Death();
        }
    }
    public void HealDamage(float heal) {
        _health = _health + heal;
        if(_health >= _baseHealth) {
            _health = _baseHealth;
        }
    }
    public bool IsAlive() {
        return _health > 0;
    }
    protected virtual void Death() {
        GameInstance.Instance.Player.AddGold(_goldOnDeath);
        if(_animationController != null) {
            _animationController.SetBool("IsDead",true);
        }

        gameObject.SetActive(false);
        if(_cooldownHandler != null) {
            _cooldownHandler.StartCooldown(_respawnTime,this);
        }

    }
    public virtual void Respawn() {
        _health = _baseHealth;
        if(_animationController != null) {
            _animationController.SetBool("IsDead",false);
        }
        gameObject.SetActive(true);
    }
}
