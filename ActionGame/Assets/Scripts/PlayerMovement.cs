using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    //check keyboard iinput multipy delta time with speed with keyboard character
    //fields
    private Rigidbody rb;
    public float speed = 2f;
    public float jumpForce = 2f;
    [SerializeField] private float distToGround = 0.5f;

    [SerializeField] private float gravityScale = 1.0f;

    public bool isPaused = false;

    public Animator anim;
    public bool animatorIsGrounded = true;

    // time to wait before allowing the player to jump again
    [SerializeField] private float jumpCooldown = 0.5f;
    private bool _jumpingIsEnabled = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // don't allow player to do anything if they're paused
        if (isPaused) return;

        // if the animation state says player is not grounded but they actually are
        if (!animatorIsGrounded && _jumpingIsEnabled && IsGrounded())
        {
            animatorIsGrounded = true;
            anim.SetBool("IsGrounded", animatorIsGrounded);
        }

        //Left and Right movement
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb.MovePosition(transform.position + Vector3.left * speed * Time.deltaTime);
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);

            anim.SetBool("IsWalking", true);
        } else if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb.MovePosition(transform.position + Vector3.right * speed * Time.deltaTime);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

            anim.SetBool("IsWalking", true);
        } else
        {
            anim.SetBool("IsWalking", false);
        }

        //Jumping
        if ((Input.GetKeyDown("up") || Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.Space)) && _jumpingIsEnabled && IsGrounded())
        {
            //found on Unity's Documentation v.2022.3
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // activate the cooldown
            _jumpingIsEnabled = false;
            Invoke(nameof(ReenableJumping), jumpCooldown);

            // tell the animator that the player is not grounded
            animatorIsGrounded = false;
            anim.SetBool("IsGrounded", animatorIsGrounded);
        } 
    }

    private void FixedUpdate()
    {
        // apply a continuous downward force to make the jumps feel more realistic
        rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    private void ReenableJumping()
    {
        _jumpingIsEnabled = true;
    }
}
