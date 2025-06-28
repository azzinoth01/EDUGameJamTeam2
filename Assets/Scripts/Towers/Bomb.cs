using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float speed = 5f;
    public float explosionRadius = 2f;
    public float damage = 30;
    public LayerMask enemyLayer;
    public GameObject explosionEffect;

    private Vector2 targetPosition;

    public void SetTarget(Transform t) {
        if(t != null)
            targetPosition = t.position; 
        else
            Destroy(gameObject); 
    }

    void Update() {
        Vector2 dir = targetPosition - (Vector2) transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame) {
            Explode();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame,Space.World);
    }

    void Explode() {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position,explosionRadius,enemyLayer);
        HashSet<Enemy> damagedEnemies = new HashSet<Enemy>();

        foreach(var hit in hits) {
            Enemy e = hit.GetComponentInParent<Enemy>();
            if(e != null && !damagedEnemies.Contains(e)) {
                damagedEnemies.Add(e);
                Debug.Log("🎯 Enemy bulundu: " + e.name);
                e.TakeDamage(damage);
            }
        }

        if(explosionEffect != null)
            Instantiate(explosionEffect,transform.position,Quaternion.identity);

        Destroy(gameObject);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,explosionRadius);
    }
}
