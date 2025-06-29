using UnityEngine;

public class DespawnAfterTime : MonoBehaviour
{
    [SerializeField] private float _despawnTime = 1f;
    private float _passedTime;


    // Update is called once per frame
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
