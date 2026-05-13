using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    public AttackSystem system;
    public AttackSystem.WeaponState ownerState;
    public Animator ownerAnimator;
    public Collider myCollider;
    public Transform myTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (ownerState == null || system == null) return;

        // DEBUG İÇİN (İstersen silebilirsin)
        // Debug.Log($"<color=yellow>[HITBOX SENSÖRÜ] {gameObject.name} kılıcı şuna değdi: {other.name} (Etiketi: {other.tag})</color>");

        if (ownerState.isTimerActive && !ownerState.isHoldingAttack && !ownerState.hasHitTarget)
        {
            // HEM damageable HEM DE blocker ETİKETİNİ KABUL ET
            if (other.CompareTag("damageable") || other.CompareTag("blocker"))
            {
                // Kılıca veya gövdeye çarptıysa, asıl sahibini (PlayerController) bul
                IDamageable target = other.GetComponentInParent<IDamageable>();

                if (target != null)
                {
                    // other.gameObject yerine target'ın asıl objesini gönderiyoruz
                    system.ProcessHit(ownerState, ownerAnimator, myTransform, myCollider, ((MonoBehaviour)target).gameObject);

                    // ÖNEMLİ DÜZELTME: Bunu TRUE yapmalısın ki aynı saldırıda arka arkaya 50 kere hasar yazmasın!
                    ownerState.hasHitTarget = true;
                }
            }
        }
    }
}