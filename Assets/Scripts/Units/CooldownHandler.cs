using UnityEngine;
using UnityEngine.UI;

public class CooldownHandler : MonoBehaviour
{
    private Tower _tower;
    public Image cooldownFillImage;
    public Image cdfillimage;
    public GameObject hpBar;
    private float cooldownDuration;

    private float cooldownTimer;
    private bool isCooldownActive = false;

    void Start() {
        cooldownFillImage.gameObject.SetActive(false);
    }

    public void StartCooldown(float respawnTime,Tower tower) {
        _tower = tower;
        if(isCooldownActive) return;
        hpBar.gameObject.SetActive(false);
        isCooldownActive = true;
        cooldownDuration = respawnTime;
        cooldownTimer = cooldownDuration;
        cdfillimage.fillAmount = 1f;
        cooldownFillImage.gameObject.SetActive(true);
    }

    void Update() {
        if(!isCooldownActive) return;

        cooldownTimer -= Time.deltaTime;
        cdfillimage.fillAmount = cooldownTimer / cooldownDuration;

        if(cooldownTimer <= 0f) {
            isCooldownActive = false;
            _tower.enabled = true;
            cooldownFillImage.gameObject.SetActive(false);
            hpBar.gameObject.SetActive(true);
            _tower.Respawn();
        }
    }
}
