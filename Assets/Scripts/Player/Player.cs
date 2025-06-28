using UnityEngine;

public class Player : MonoBehaviour
{
    private int _gold;
    [SerializeField] private int _startGold;
    [SerializeField] private int _goldPerIncomeTick;
    [SerializeField] private float _incomeTickTimeInSeconds;
    private float _passedTimeGoldIncome;

    public int Gold {
        get {
            return _gold;
        }
    }

    private void Awake() {
        _gold = _startGold;
        GameInstance.Instance.Player = this;

    }
    private void Update() {
        GoldIncome();

    }
    private void GoldIncome() {
        _passedTimeGoldIncome = _passedTimeGoldIncome + Time.deltaTime;
        if(_passedTimeGoldIncome <= _incomeTickTimeInSeconds) {
            return;
        }
        _passedTimeGoldIncome = -_incomeTickTimeInSeconds;
        _gold = _gold + _goldPerIncomeTick;
    }
    public void GetGold(int gold) {
        _gold = _gold + gold;
    }

}
