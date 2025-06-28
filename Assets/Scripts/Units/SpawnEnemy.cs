using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SpawnEnemy : MonoBehaviour
{

    [SerializeField] private Enemy _enemyType;
    private Button _button;
    [SerializeField] private float _spawnCooldownInSeconds;
    private float _passedTime;

    private void Awake() {
        _button = GetComponent<Button>();
        _passedTime = _spawnCooldownInSeconds;
    }

    public void SpawnEnemyAtPosition() {
        if(CanSpawn() == false) {
            return;
        }
        GameInstance.Instance.Player.AddGold(-_enemyType.SpawnCost);
        Transform spawnPosition = GameInstance.Instance.CurrentCheckPointPosition;
        Instantiate(_enemyType,spawnPosition.position,Quaternion.identity);

        _passedTime = 0;
    }


    private bool CanSpawn() {
        int gold = GameInstance.Instance.Player.Gold;

        if(gold >= _enemyType.SpawnCost) {
            return true;
        }
        return false;
    }

    private void Update() {
        bool noCooldonw = UpdateSpawnCoolddown();
        bool allowSpawn = noCooldonw && CanSpawn();

        _button.interactable = allowSpawn;
    }

    private bool UpdateSpawnCoolddown() {
        _passedTime = _passedTime + Time.deltaTime;
        if(_passedTime >= _spawnCooldownInSeconds) {
            return true;
        }
        return false;
    }

}
