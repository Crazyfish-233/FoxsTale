using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    public float BounceForce = 20f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("Bounce");
            PlayerController.instance.theRB.velocity = new Vector2(PlayerController.instance.theRB.velocity.x, BounceForce);
            AudioManager.instance.PlaySFX(10);
            PlayerController.instance.canDoubleJump = true;

        }
    }
}
