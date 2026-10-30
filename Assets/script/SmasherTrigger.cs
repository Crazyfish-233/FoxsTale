using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmasherTrigger : MonoBehaviour
{
    public Transform smasher;
    private Vector3 returnPosition;
    public float waitAttack, attackSpeed;
    private bool Attack;
    private float attackCount;
    // Start is called before the first frame update
    void Start()
    {
        returnPosition = smasher.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackCount > 0)
        {
            attackCount -= Time.deltaTime;
            smasher.position = Vector3.MoveTowards(smasher.position, returnPosition, attackSpeed * Time.deltaTime);
        }
        else
        {
            if (Attack)
            {
                smasher.position = Vector3.MoveTowards(smasher.position, transform.position, attackSpeed*Time.deltaTime);
                if (Vector3.Distance(smasher.position, transform.position) < .1f)
                {
                    attackCount = waitAttack;
                    Attack = false;
                }
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Attack = true;
        }
    }
}
