using System.Collections.Generic;
using UnityEngine;

public class TowerInRangeDetection : MonoBehaviour
{

    private List<Tower> _towerInRange;

    public Tower GetFirstTower() {
        if(_towerInRange == null || _towerInRange.Count == 0) {
            return null;
        }
        return _towerInRange[0];
    }

    private void Awake() {
        _towerInRange = new List<Tower>();
    }




    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.TryGetComponent(out Tower tower)) {
            _towerInRange.Add(tower);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.TryGetComponent(out Tower tower)) {
            _towerInRange.Remove(tower);
        }
    }

}
