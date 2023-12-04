using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reads Input and Fires Projectiles
/// </summary>
public class MeleeAttackManager : MonoBehaviour
{
    [SerializeField, Tooltip("The hitbox object for the melee attack")]
    MeleeHitbox m_MeleeHitbox;

    [Header("Attack Stats")]
    [SerializeField, Tooltip("The maximum distance from the center of the attack that the player can damage")]
    float m_AttackRange = 1;
    [SerializeField, Tooltip("The angle in degrees that the attack swings from start to finish")]
    float m_SwingAngle = 100;
    [SerializeField, Tooltip("How long it takes for a single attack to complete in seconds (does not include initial delay)")]
    float m_AttackDuration = 0.2f;
    [SerializeField, Tooltip("How long it takes for the attack to start after key input")]
    float m_InitialDelay = 0.05f;

    [SerializeField, Tooltip("How long the player must wait between attack inputs")]
    float m_AttackCooldown = 1;
    bool _cooldownActive = false;

    /// <summary>
    /// TEMPORARY INPUT LISTENERS WILL CHANGE TO NEW INPUT SYSTEM LATER
    /// </summary>
    private void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            Melee(true);
        }
    }

    /// <summary>
    /// Wait the delay time before starting the attack
    /// </summary>
    /// <returns></returns>
    IEnumerator NewAttack()
    {
        yield return new WaitForSeconds(m_InitialDelay);

        m_MeleeHitbox.StartSwing(m_AttackRange, m_SwingAngle, m_AttackDuration);

    }

    /// <summary>
    /// Wait for the attack to stop before allowing more attacks
    /// </summary>
    /// <returns></returns>
    IEnumerator CooldownTimer()
    {
        yield return new WaitForSeconds(m_AttackCooldown);

        _cooldownActive = false;
    }

    /// <summary>
    /// Pass the desired properties for the hitbox and start a melee attack
    /// </summary>
    private void Melee(bool doAttack)
    {
        if (!doAttack) { return; }
        if (_cooldownActive) return; // don't attack if the cooldown is currently active

        _cooldownActive = true;
        StartCoroutine(NewAttack());
        StartCoroutine(CooldownTimer());
    }
}