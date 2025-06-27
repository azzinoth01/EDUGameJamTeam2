using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    [SerializeField] private Enemy _enemyType;
    [SerializeField] private Transform _spawnPosition;

    public void SpawnEnemyAtPosition() {
        Instantiate(_enemyType,_spawnPosition.position,Quaternion.identity);
    }

}
