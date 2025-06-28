using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int _goldOnDeath;
    [SerializeField] private int _baseHealth;
    private int _health;
    [SerializeField] private float _respawnTime;

    private void Awake() {
        _health = _baseHealth;
    }

    private void OnDestroy() {
        GameInstance.Instance.Player.AddGold(_goldOnDeath);
    }

    public void TakeDmg(int damage) {
        _health = _health - damage;
        if(_health < 0) {
            Death();
        }
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
