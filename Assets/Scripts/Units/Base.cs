using UnityEngine;

public class Base : ArrowTower
{
    [SerializeField] GameObject _winScreen;
    [SerializeField] float _regenHealth;
    [SerializeField] float _regenTime;
    float _passedTime;

    protected override void Death() {


        gameObject.SetActive(false);
        _winScreen.SetActive(true);
    }
    protected override void Update() {
        base.Update();
        RegenHealth();
    }
    private void RegenHealth() {
        _passedTime = _passedTime + Time.deltaTime;
        if(_passedTime >= _regenTime) {
            HealDamage(_regenHealth);
        }
    }
}
