using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
//[RequireComponent(typeof(Rigidbody))]
public class MeleeHitbox : MonoBehaviour
{
    [SerializeField, Tooltip("The transform that the hitbox should rotate around (should be a direct parent and have initial rotation at (0, 0, 0))")]
    Transform m_HitboxPivot;

    float _swingTimer = 0; // used to manage the duration of melee swings
    float _currentSwingSpeed;
    float _currentSwingAngle;

    public void Update()
    {
        if (_swingTimer > 0) ContinueSwing();
        else gameObject.SetActive(false); // prevent the hitbox from colliding while not swinging
    }

    /// <summary>
    /// Sets the hitbox range and angle before starting a melee swing of the provided duration
    /// </summary>
    /// <param name="range"></param>
    /// <param name="angle"></param>
    /// /// <param name="duration"></param>
    /// <returns>
    /// True if the attack starts successfully
    /// False if the attack fails (already in the middle of another attack)
    /// </returns>
    public bool StartSwing(float range, float angle, float duration)
    {
        // Check if already attacking
        if (_swingTimer > 0) return false;

        // Activate game object
        gameObject.SetActive(true);

        // Update scale and position to adjust to range
        Vector3 newScale = transform.localScale;
        newScale.x = range;
        transform.localScale = newScale;
        Vector3 newPos = transform.localPosition;
        newPos.x = range / 2;
        transform.localPosition = newPos;

        // Set initial swing angle
        _currentSwingAngle = angle / 2;
        m_HitboxPivot.localRotation = Quaternion.Euler(0, _currentSwingAngle, 0);

        // Manage swing timings
        _swingTimer = duration;
        _currentSwingSpeed = angle / duration;
        return true;
    }

    /// <summary>
    /// Continue rotating the melee swing
    /// </summary>
    private void ContinueSwing()
    {
        // Advance swing rotation
        _currentSwingAngle -= _currentSwingSpeed * Time.deltaTime;
        m_HitboxPivot.localRotation = Quaternion.Euler(0, _currentSwingAngle, 0);

        // Update timer
        _swingTimer -= Time.deltaTime;
    }

    /// <summary>
    /// When the hitbox collides it will send damage once and then log the object to prevent repeat damage in the same swing
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hitbox collided with " + other.name);
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Debug.Log("Destroyed object with Enemy tag");
        }
    }
}