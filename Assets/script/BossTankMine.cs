using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankMine : MonoBehaviour
{
    public GameObject explosion;
    // Start is called before the first frame update

    private static readonly List<BossTankMine> activeMines = new List<BossTankMine>();

    private void OnEnable() => activeMines.Add(this);
    private void OnDisable() => activeMines.Remove(this);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Explode();
        PlayerHealthController.instance.DealDamage();
    }

    public void Explode()
    {
        AudioManager.instance.PlaySFX(3);
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public static void ExplodeAll()
    {
        if (activeMines.Count == 0) return;

        
        var mines = activeMines.ToArray();
        foreach (BossTankMine m in mines)
        {
            if (m != null) m.Explode();
        }
    }
}
