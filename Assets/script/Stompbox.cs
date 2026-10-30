using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stompbox : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject collectible;
    [Range(0f, 100f)][SerializeField] private float chanceToDrop;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        GameObject enemyRoot = FindEnemyRoot(other);
        if (enemyRoot == null) return;

        enemyRoot.SetActive(false);
        
        PlayerController.instance.Bounce();
        Instantiate(deathEffect, other.transform.position, other.transform.rotation);
        AudioManager.instance.PlaySFX(3);

        if (Random.Range(0f, 100f) < chanceToDrop)
            Instantiate(collectible, other.transform.position, other.transform.rotation);
    }

    private static GameObject FindEnemyRoot(Collider2D col)
    {
        if (PlayerController.instance.transform.position.y < col.gameObject.transform.position.y) return null;

        if (col.attachedRigidbody != null) return col.attachedRigidbody.gameObject;

        var ground = col.GetComponentInParent<EnemyController>();
        if (ground != null) return ground.gameObject;

        var flyer = col.GetComponentInParent<FlyingEnemyController>();
        if (flyer != null) return flyer.gameObject;

        return null;
    }
}
