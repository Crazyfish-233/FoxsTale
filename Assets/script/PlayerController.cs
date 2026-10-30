using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float groundCheckRadius = 0.2f;
    public static PlayerController instance;
    public float moveSpeed;
    public float jumpPower;
    public float bounceForce;
    public float knockBackCount, knockBackLength, knockBackForce;
    public Rigidbody2D theRB;
    public bool isGround;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    public bool canDoubleJump;
    private Animator anim;
    private SpriteRenderer theSR;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
       
        isGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);
        if (isGround) canDoubleJump = true;

       
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGround", isGround);


        if (!GameState.CanControlPlayer) return;


        if (knockBackCount > 0)
        {
            knockBackCount -= Time.deltaTime;
            float dir = theSR.flipX ? 1f : -1f;
            theRB.velocity = new Vector2(dir * knockBackForce, theRB.velocity.y);
            return;
        }

        
        theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpPower);
                AudioManager.instance.PlaySFX(10);
            }
            else if (canDoubleJump)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpPower);
                canDoubleJump = false;
                AudioManager.instance.PlaySFX(10);
            }
        }

        if (theRB.velocity.x < 0) theSR.flipX = true;
        else if (theRB.velocity.x > 0) theSR.flipX = false;  

    }
    public void KnockBack()
    {
        knockBackCount = knockBackLength;
        theRB.velocity = new Vector2(0f, knockBackForce);

        anim.SetTrigger("hurt");
    }
    public void Bounce()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, bounceForce);
    }
}
