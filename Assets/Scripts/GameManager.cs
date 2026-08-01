using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool hasKey;

    [SerializeField] private Vector3 respawnPoint;
    [SerializeField] private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPlayerToStart()
    {
        NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
        agent.Warp(respawnPoint);
    }
}
