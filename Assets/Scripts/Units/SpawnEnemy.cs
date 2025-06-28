using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    [SerializeField] private Enemy _enemyType;


    public void SpawnEnemyAtPosition() {

        Transform spawnPosition = GameInstance.Instance.CurrentCheckPointPosition;
        Instantiate(_enemyType,spawnPosition.position,Quaternion.identity);
    }

}
