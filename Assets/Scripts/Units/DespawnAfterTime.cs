using UnityEngine;

public class DespawnAfterTime : MonoBehaviour
{
    [SerializeField] private float _despawnTime = 1f;
    private float _passedTime;

    void Update() {
        Despawn();
    }

    private void Despawn() {
        _passedTime = _passedTime + Time.deltaTime;
        if(_despawnTime > _passedTime) {
            return;
        }
        Destroy(gameObject);
    }
}
