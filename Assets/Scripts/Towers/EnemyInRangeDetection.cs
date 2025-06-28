using System.Collections.Generic;
using UnityEngine;

public class EnemyInRangeDetection : MonoBehaviour
{

    private List<Enemy> _enemiesInRange;

    public List<Enemy> EnemiesInRange {
        get {
            return _enemiesInRange;
        }
    }

    public bool IsInRange(Enemy enemy) {
        return _enemiesInRange.Contains(enemy);
    }

    private void Awake() {
        _enemiesInRange = new List<Enemy>();
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.TryGetComponent(out Enemy enemy)) {
            _enemiesInRange.Add(enemy);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.TryGetComponent(out Enemy enemy)) {
            _enemiesInRange.Remove(enemy);
        }
    }

}
