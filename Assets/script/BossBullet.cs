using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private LayerMask destroyOnLayers;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>();   
    }
    void Start()
    {
        AudioManager.instance.PlaySFX(2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position+new Vector2(-bulletSpeed * transform.localScale.x * Time.fixedDeltaTime, 0));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealthController.instance.DealDamage();
            Destroy(gameObject);
            return;
        }

        if (((1 << collision.gameObject.layer) & destroyOnLayers) != 0)
            Destroy(gameObject);

    }
}
