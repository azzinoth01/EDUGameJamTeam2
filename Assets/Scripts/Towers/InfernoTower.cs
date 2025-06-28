using UnityEngine;

public class InfernoTower : Tower
{
    public float range = 5f;
    public float damagePerSecond = 5f;
    public float damageIncreasePerSecond = 2f;
    public float cdDuration = 1f;
    public LineRenderer laserRenderer;
    public Transform baseTarget;
    private Enemy currentTarget;
    private float currentDamage;
    private float targetcd = 0f;

    void Update() {
        if(currentTarget == null || !IsInRange(currentTarget) || currentTarget.IsDead()) {
            StopLaser();
            currentTarget = null;

            if(targetcd > 0) {
                targetcd -= Time.deltaTime;
                return;
            }

            FindTarget();
            currentDamage = damagePerSecond;
        }

        if(currentTarget != null) {
            DamageTarget();
            DrawLaser();

            if(currentTarget.IsDead()) {
                currentTarget = null;
                targetcd = cdDuration;
            }
        }
    }

    void DamageTarget() {
        currentTarget.TakeDamage(currentDamage * Time.deltaTime);
        currentDamage += damageIncreasePerSecond * Time.deltaTime;
    }

    void FindTarget() {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        float closestToBase = Mathf.Infinity;
        Enemy mostAdvancedEnemy = null;

        foreach(var enemy in enemies) {
            float distToBase = Vector2.Distance(enemy.transform.position,baseTarget.position);
            float distToTower = Vector2.Distance(transform.position,enemy.transform.position);

            if(distToBase < closestToBase && distToTower <= range) {
                closestToBase = distToBase;
                mostAdvancedEnemy = enemy;
            }
        }

        currentTarget = mostAdvancedEnemy;
    }

    bool IsInRange(Enemy enemy) {
        return Vector2.Distance(transform.position,enemy.transform.position) <= range;
    }

    void DrawLaser() {
        if(laserRenderer != null && currentTarget != null) {
            laserRenderer.enabled = true;
            laserRenderer.SetPosition(0,transform.position);
            laserRenderer.SetPosition(1,currentTarget.transform.position);
        }
    }

    void StopLaser() {
        if(laserRenderer != null)
            laserRenderer.enabled = false;
    }
}
