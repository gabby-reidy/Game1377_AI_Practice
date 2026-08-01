using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    public InputSystem_Actions InputSystemActions;
    private InputSystem_Actions.PlayerActions playerActions;

    private Vector3 targetPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        InputSystemActions = new InputSystem_Actions();
        playerActions = InputSystemActions.Player;

        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        playerActions.Click.performed += ClickToMove;
        playerActions.Enable();
    }

    private void OnDisable()
    {
        playerActions.Click.performed -= ClickToMove;
        playerActions.Disable();
    }

    /// <summary>
    /// Uses raycast from mouse and updates nav agents destination accordingly
    /// </summary>
    /// <param name="context"></param>
    private void ClickToMove(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            targetPosition = hit.point;
            agent.SetDestination(targetPosition);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.ResetPlayerToStart();
        }

        if (collision.gameObject.CompareTag("Key"))
        {
            GameManager.Instance.HasKey = true;
            Debug.Log(GameManager.Instance.HasKey);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Door"))
        {
            if (GameManager.Instance.HasKey)
            {
                Destroy(collision.gameObject); // to test if our condition is working
            }
        }
    }
}
