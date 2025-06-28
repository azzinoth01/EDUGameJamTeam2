using System.Collections.Generic;
using UnityEngine;

public class RespawnHandler : MonoBehaviour
{
    private List<RespawnObject> _respawnObjects;


    private void Awake() {
        _respawnObjects = new List<RespawnObject>();
        GameInstance.Instance.RespawnHandler = this;
    }

    // Update is called once per frame
    void Update() {
        float deltaTime = Time.deltaTime;
        for(int i = _respawnObjects.Count - 1; i >= 0; i--) {
            if(_respawnObjects[i].UpdateResapawnTime(deltaTime)) {
                _respawnObjects.RemoveAt(i);
            }
        }
    }

    public void RespawnTower(Tower towers,float time) {
        RespawnObject respawnObject = new RespawnObject(towers,time);
        _respawnObjects.Add(respawnObject);
    }
}
