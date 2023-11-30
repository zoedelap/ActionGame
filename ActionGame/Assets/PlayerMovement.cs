using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    //check keyboard iinput multipy delta time with speed with keyboard character
    //fields
    public Rigidbody rb;
    public float speed = 2f;
    public float jumpForce = 2f;
    public float distanceToCheck = 0.2f;
    public float distToGround = 0.5f;

    public Animator anim;
    public bool animatorIsGrounded = true;
    

    //player.transform = deltatime * speed
    //get position, add speed * delta time

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // set the speed parameter to the rgidbody's speed
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        // if the animation state says player is not grounded but they actually are
        if (!animatorIsGrounded && IsGrounded())
        {
            animatorIsGrounded = true;
            anim.SetBool("IsGrounded", animatorIsGrounded);
        }

        //Left and Right movement
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb.MovePosition(transform.position + Vector3.left * speed * Time.deltaTime);
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        } else if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb.MovePosition(transform.position + Vector3.right * speed * Time.deltaTime);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        //Jumping
        if ((Input.GetKey("up") || Input.GetKey("w") || Input.GetKey(KeyCode.Space)) && IsGrounded())
        {
            //found on Unity's Documentation v.2022.3
            rb.AddForce(transform.up * jumpForce);
            // tell the animator that the player is not grounded
            animatorIsGrounded = false;
            anim.SetBool("IsGrounded", animatorIsGrounded);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    
}
