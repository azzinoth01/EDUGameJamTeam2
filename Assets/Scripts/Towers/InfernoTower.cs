using UnityEngine;

public class InfernoTower : MonoBehaviour
{
    public float range = 5f;
    public float damagePerSecond = 5f;
    public float damageIncreasePerSecond = 2f;
    public LineRenderer laserRenderer;

    private Enemy currentTarget;
    private float currentDamage;

    void Update() {
        if(currentTarget == null || !IsInRange(currentTarget)) {
            FindTarget();
            currentDamage = damagePerSecond;
        }

        if(currentTarget != null) {
            DamageTarget();
            DrawLaser();
        }
        else {
            StopLaser();
        }
    }

    void DamageTarget() {
        if(currentTarget != null) {
            currentTarget.TakeDmg(currentDamage * Time.deltaTime);
            currentDamage += damageIncreasePerSecond * Time.deltaTime;
        }
    }

    void FindTarget() {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        float closestDistance = Mathf.Infinity;
        Enemy closestEnemy = null;

        foreach(var enemy in enemies) {
            float dist = Vector2.Distance(transform.position,enemy.transform.position);
            if(dist < closestDistance && dist <= range) {
                closestDistance = dist;
                closestEnemy = enemy;
            }
        }

        currentTarget = closestEnemy;
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
        if(laserRenderer != null) {
            laserRenderer.enabled = false;
        }
    }
}
