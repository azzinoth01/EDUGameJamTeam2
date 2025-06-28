using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int _goldOnDeath;
    [SerializeField] private int _baseHealth;
    [SerializeField] private float _health;
    [SerializeField] private float _respawnTime;
    [SerializeField] protected float _attackPower;

    private void Awake() {
        _health = _baseHealth;
    }

    private void OnDestroy() {
        GameInstance.Instance.Player.AddGold(_goldOnDeath);
    }

    public void TakeDamage(float damage) {
        _health = _health - damage;
        if(_health < 0) {
            Death();
        }
    }
    public bool IsAlive() {
        return _health > 0;
    }
    private void Death() {
        GameInstance.Instance.Player.AddGold(_goldOnDeath);
        gameObject.SetActive(false);
        GameInstance.Instance.RespawnHandler.RespawnTower(this,_respawnTime);
    }

    public void Respawn() {
        _health = _baseHealth;
        gameObject.SetActive(true);
    }


}
