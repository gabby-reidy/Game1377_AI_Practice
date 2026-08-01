using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool hasKey;

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

    }
}
