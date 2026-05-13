using UnityEngine;

[CreateAssetMenu(fileName = "NewTwoHandedSword", menuName = "Combat/Attack Strategy/TwoHandedSword")]
public class TwoHandedSword : AttackSystem
{
    private readonly int timeHash = Animator.StringToHash("timer");
    private readonly int isAttackingHash = Animator.StringToHash("isAttacking");
    private readonly int attackTypeHash = Animator.StringToHash("attackType");
    public float twohDamage = 10.0f;

    public override void Initialize(WeaponState state, Animator anim) { }

    public override void OnAttackStart(WeaponState state, Animator anim, float mx, float my)
    {
        state.attackTimer = 0f;
        state.isTimerActive = true;
        state.hasHitTarget = false;

        // Direction Logic
        if (Mathf.Abs(mx) > Mathf.Abs(my))
            state.currentDirection = mx > 0 ? 2 : 1;
        else
            state.currentDirection = my > 0 ? 3 : 0;

        anim.SetBool(isAttackingHash, true);
        anim.SetInteger(attackTypeHash, state.currentDirection);
        anim.SetFloat(timeHash, 0f);

        // DEBUG: SALDIRI BAŞLADI
        Debug.Log($"<color=red>[SALDIRI] {anim.gameObject.name} saldırıyor! Yön: {state.currentDirection} (1:Sağ, 2:Sol, 3:Yukarı, 0:Aşağı)</color>");
    }

    public override void OnAttackUpdate(WeaponState state, Animator anim, Collider weaponCollider)
    {
        if (!state.isTimerActive) return;

        if (state.isHoldingAttack)
        {
            state.attackTimer = 0.0f;
        }
        else
        {
            state.attackTimer += Time.deltaTime * attackSpeed;
        }

        float normalizedTime = Mathf.Clamp01(state.attackTimer / attackDuration);
        anim.SetFloat("timer", normalizedTime);

        if (weaponCollider != null)
            weaponCollider.enabled = normalizedTime > 0.01f && normalizedTime < 0.9f && !state.isHoldingAttack;

        if (normalizedTime >= 1.0f)
        {
            state.isTimerActive = false;
            if (weaponCollider != null) weaponCollider.enabled = false;
            anim.SetBool(isAttackingHash, false);
        }
    }

    public override void OnAttackEnd(WeaponState state, Animator anim)
    {
        state.isTimerActive = true;
    }

    public override void OnBlockStart(WeaponState state, Animator anim, float mx, float my)
    {
        state.isBlocking = true;
        anim.SetBool("isBlocking", true);

        // Reuse direction logic for block pose
        state.currentDirection = (Mathf.Abs(mx) > Mathf.Abs(my)) ? (mx > 0 ? 1 : 2) : (my > 0 ? 3 : 0);
        anim.SetFloat("blockType", state.currentDirection);

        // DEBUG: BLOK BAŞLADI
        Debug.Log($"<color=blue>[BLOK] {anim.gameObject.name} blok açtı! Yön: {state.currentDirection} (1:Sağ, 2:Sol, 3:Yukarı, 0:Aşağı)</color>");
    }

    public override void OnBlockEnd(WeaponState state, Animator anim)
    {
        state.isBlocking = false;
        anim.SetBool("isBlocking", false);

        // DEBUG: BLOK BİTTİ
        Debug.Log($"<color=grey>[BLOK BİTTİ] {anim.gameObject.name} bloğu indirdi.</color>");
    }

    public override void UpdateSpineRotation(WeaponState state, Animator anim, Transform spine, float angle, float mousePitch, float mouseYaw)
    {
        if (spine == null) return;

        state.currentSpineX = Mathf.Lerp(state.currentSpineX, mousePitch, Time.deltaTime * 10f);

        Quaternion naturalRotation = spine.localRotation;
        Quaternion targetRotation = naturalRotation * Quaternion.Euler(state.currentSpineX, 0, 0);

        bool isBusy = anim.GetCurrentAnimatorStateInfo(1).IsTag("block") || anim.GetCurrentAnimatorStateInfo(1).IsTag("grip") || state.currentDirection == 3;

        spine.localRotation = Quaternion.Slerp(state.lastSpineRotation, isBusy ? naturalRotation : targetRotation, Time.deltaTime * 10f);

        state.lastSpineRotation = spine.localRotation;
    }

    public void onBlocked(WeaponState state, Animator anim, Collider weaponCollider)
    {
        if (state == null) return;
        state.isTimerActive = false;

        if (anim != null) anim.SetBool("isAttacking", false);

        if (state.attackTimer > 0.1)
        {
            state.attackTimer -= Time.deltaTime * attackSpeed;
        }
    }

    public override void ProcessHit(WeaponState state, Animator attackerAnim, Transform attackerPos, Collider attackerCollider, GameObject victim)
    {
        IDamageable damageable = victim.GetComponent<IDamageable>();
        if (damageable == null) return;

        Transform victimTransform = damageable.GetTransform();
        int victimBlockDirection = damageable.IsBlocking();

        // 1. ÖNDEN Mİ SALDIRIYOR KONTROLÜ (Dot Product)
        Vector3 dirToAttacker = (attackerPos.position - victimTransform.position).normalized;
        dirToAttacker.y = 0;
        Vector3 victimForward = victimTransform.forward;
        victimForward.y = 0;

        float dotProduct = Vector3.Dot(victimForward.normalized, dirToAttacker.normalized);
        bool isAttackingFromFront = dotProduct > 0.3f;

        // =========================================================
        // DEBUG: ÇARPIŞMA (HIT) ANALİZİ
        Debug.Log($"<b>--- ÇARPIŞMA RAPORU ---</b>\n" +
                  $"<b>Saldıran:</b> {attackerAnim.gameObject.name} (Vurduğu Yön: {state.currentDirection})\n" +
                  $"<b>Kurban:</b> {victim.name} (Savunduğu Yön: {victimBlockDirection})\n" +
                  $"<b>Açı (DotProduct):</b> {dotProduct:F2} (0.3'ten büyükse önden sayılır: {isAttackingFromFront})");
        // =========================================================

        // 2. BLOK VE HASAR MANTIĞI
        if (victimBlockDirection != -1 && state.currentDirection == victimBlockDirection && isAttackingFromFront)
        {
            onBlocked(state, attackerAnim, attackerCollider);
            Debug.Log($"<color=cyan>SONUÇ: BAŞARILI BLOK! ({victim.name})</color>");

            PlayerController player = victim.GetComponent<PlayerController>();
            if (player != null) player.OnSuccessfulBlock();
        }
        else
        {
            damageable.TakeDamage(twohDamage);

            // Neden bloklanmadığını konsola yaz
            if (victimBlockDirection == -1)
                Debug.Log($"<color=orange>SONUÇ: HASAR ({victim.name} blok yapmıyordu!)</color>");
            else if (state.currentDirection != victimBlockDirection)
                Debug.Log($"<color=orange>SONUÇ: HASAR (Yanlış yön! Saldırı:{state.currentDirection}, Savunma:{victimBlockDirection})</color>");
            else if (!isAttackingFromFront)
                Debug.Log($"<color=orange>SONUÇ: HASAR (Arkadan vuruldu! Açı yetersiz.)</color>");
        }
    }
}