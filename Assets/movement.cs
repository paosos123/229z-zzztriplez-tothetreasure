using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class movement : MonoBehaviour
{
    private float horizontal;
    [SerializeField]private float speed = 8f;
    [SerializeField]private float jumpingPower = 16f;
    private bool isFacingRight = true;
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public int scoreAmount;
    [SerializeField]TextMeshProUGUI scoreText;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject backGround;
    [SerializeField] private GameObject lose;
    [SerializeField] private GameObject win;
    private bool canFilp ;
    void Start()
    {
        scoreAmount = 0;
        canFilp = true;
    }
    void Update()
    {
        scoreText.text = "Score : " + scoreAmount;
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (canFilp)
        {
            Flip();
        }
       
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Trap"))
        {
            Debug.Log("Collided with enemy");
            canFilp = false;
            backGround.SetActive(true);
            lose.SetActive(true);
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY ;
        }
        else if (collision.gameObject.CompareTag("score"))
        {
            scoreAmount += 1;
        }
        else if (collision.gameObject.CompareTag("end"))
        {
            canFilp = false;
            backGround.SetActive(true);
            win.SetActive(true);
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY ;
        }
    }
   
    
}

