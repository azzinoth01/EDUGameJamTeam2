using UnityEngine;
using UnityEngine.UI;

public class HealthBarTower : MonoBehaviour
{
    [SerializeField] private Image _barImage;
    [SerializeField] private Tower _unit;
    [SerializeField] private Image _barBackground;

    // Update is called once per frame
    void Update() {
        IHealth healthValues = _unit;

        float procent = healthValues.Health / healthValues.MaxHealth;
        if(procent >= 1f) {
            _barImage.enabled = false;
            _barBackground.enabled = false;
        }
        else {
            _barImage.enabled = true;
            _barBackground.enabled = true;
        }
        _barImage.fillAmount = procent;
    }
}
