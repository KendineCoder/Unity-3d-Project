using UnityEngine;

public interface IDamageable
{

    void TakeDamage(float amount);
    int IsBlocking();
    Transform GetTransform();
}