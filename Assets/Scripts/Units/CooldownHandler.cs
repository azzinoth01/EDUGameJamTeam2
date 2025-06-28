using UnityEngine;
using UnityEngine.UI;

public class CooldownHandler : MonoBehaviour
{
    public MonoBehaviour towerScript; 
    public Image cooldownFillImage;
    public Image cdfillimage;
    public GameObject hpBar;
    public float cooldownDuration = 5f;

    private float cooldownTimer;
    private bool isCooldownActive = false;

    void Start()
    {
        cooldownFillImage.gameObject.SetActive(false);
    }

    public void StartCooldown()
    {
        if (isCooldownActive) return;
        hpBar.gameObject.SetActive(false);
        isCooldownActive = true;
        cooldownTimer = cooldownDuration;
        if (towerScript != null)
            towerScript.enabled = false;

        cdfillimage.fillAmount = 1f;
        cooldownFillImage.gameObject.SetActive(true);
    }

    void Update()
    {
        if (!isCooldownActive) return;

        cooldownTimer -= Time.deltaTime;
        cdfillimage.fillAmount = cooldownTimer / cooldownDuration;

        if (cooldownTimer <= 0f)
        {
            isCooldownActive = false;
            towerScript.enabled = true;
            cooldownFillImage.gameObject.SetActive(false);
            hpBar.gameObject.SetActive(true);
            Tower tower = GetComponent<Tower>();
            if (tower != null)
                tower.Respawn();
        }
    }
}
