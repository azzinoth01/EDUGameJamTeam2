using UnityEngine;
using UnityEngine.UI;

public class HealthBarTower : MonoBehaviour
{
    [SerializeField] private Image _barImage;
    [SerializeField] private Tower _unit;

    // Update is called once per frame
    void Update() {
        IHealth healthValues = _unit;

        float procent = healthValues.Health / healthValues.MaxHealth;
        _barImage.fillAmount = procent;
    }
}
