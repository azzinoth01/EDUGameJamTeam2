
public class RespawnObject
{
    private Tower _towers;
    private float _respawnTime;
    private float _passedTime;

    public RespawnObject(Tower towers,float respawnTime) {
        _towers = towers;
        _respawnTime = respawnTime;
        _passedTime = 0f;
    }

    public bool UpdateResapawnTime(float deltaTime) {
        _passedTime = _passedTime + deltaTime;
        if(_respawnTime > _passedTime) {
            return false;
        }
        _towers.Respawn();
        return true;

    }

}
