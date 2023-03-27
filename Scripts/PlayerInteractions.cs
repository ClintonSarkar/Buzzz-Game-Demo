using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteractions : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpForce = 16f;
    private bool isFacingRight = true;
    public int health = 3;
    private bool isHit = false;
 

    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask changeLayer;
    public LayerMask enemyLayer;
    public Animator anim;
    public GameObject youWinText,youDiedText,restartButton,quitButton;
    public WinSound winSound;

    Vector3 lastVelocity;

    private AudioSource audioSource;

    void Start()
    {
        youWinText.SetActive(false);
        youDiedText.SetActive(false);
        restartButton.SetActive(false);
        quitButton.SetActive(false);

        audioSource= GetComponent<AudioSource>(); 
    }
    
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && (IsGrounded() || IsActive()))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if(IsActive())
        {
            winSound.audioSource.Play();
            StartCoroutine(WinDelay());
        }

      
        lastVelocity = rb.velocity;

        Flip();
        Animation();
    }          

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    
    private bool IsActive()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, changeLayer);
    }  
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

        if (collision.gameObject.tag.Equals("Enemy"))
        {
            rb.velocity = direction * Mathf.Min(speed*2,15f);

            health -= 1;
            Debug.Log(health);

            isHit = true;

            if (health == 0)
            {
                audioSource.Play();
                StartCoroutine(DeathDelay());
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            isHit= false;
        }
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

    private void Animation()
    {
        anim.SetFloat("horizontal", Mathf.Abs(horizontal));
        anim.SetBool("touchGround", IsGrounded() || IsActive());
        anim.SetBool("hit", isHit);
    }

    IEnumerator DeathDelay()
    {
        youDiedText.SetActive(true);
        restartButton.SetActive(true);
        quitButton.SetActive(true);
        yield return new WaitForSeconds(.6f);
        gameObject.SetActive(false);
    }
       IEnumerator WinDelay()
    {
        youWinText.SetActive(true);
        restartButton.SetActive(true);
        quitButton.SetActive(true);
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }

}


