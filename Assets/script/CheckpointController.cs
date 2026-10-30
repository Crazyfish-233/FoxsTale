using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public static CheckpointController instance;
    private Checkpoint[] checkpoints;
    public Vector3 spawnPoint;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        checkpoints = FindObjectsOfType<Checkpoint>();
        SetSpawnPoint(PlayerController.instance.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeactiviteCheckpoints()
    {
        for (int i = 0; i < checkpoints.Length; i++) {
            checkpoints[i].ResetCheckpoints();

        }
    }
    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }

}
