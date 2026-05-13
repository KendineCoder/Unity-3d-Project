using UnityEngine;

public class SpellPrefab : MonoBehaviour
{
    [Header("Spell Settings")]
    public float damage = 25f;
    public float speed = 10f;
    public float lifetime = 5f; // Destroy after 5 seconds to prevent memory leaks

    void Start()
    {
        // Automatically destroy the spell after 'lifetime' seconds if it misses everything
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move the spell forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 1. Check if we hit an enemy (or player)
        if (other.CompareTag("damageable"))
        {
            // Look for the IDamageable interface we set up earlier
            IDamageable target = other.GetComponent<IDamageable>();

            // Sometimes the collider is on a child object, so fallback to GetComponentInParent
            if (target == null) target = other.GetComponentInParent<IDamageable>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }

            // Optional: Instantiate an explosion or hit particle effect here

            // Destroy the spell upon hitting the target
            Destroy(gameObject);
        }
        // 2. Check if we hit a wall/ground
        else if (!other.CompareTag("Player") && !other.CompareTag("Wand"))
        {
            // Destroy the spell if it hits the environment
            Destroy(gameObject);
        }
    }
}