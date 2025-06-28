using UnityEngine;

public class Base : ArrowTower
{
    [SerializeField] GameObject _winScreen;

    protected override void Death() {

        gameObject.SetActive(false);
        _winScreen.SetActive(true);
    }

}
