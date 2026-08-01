using UnityEngine;
using UnityEngine.AI;

public class EnemyPatroller : MonoBehaviour
{
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float viewAngle = 120f;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject[] waypoints;
    private int waypointIndex = 0;

    private void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    private void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && (!agent.hasPath || agent.velocity.sqrMagnitude < 0.01f))
        {
            SetNextWaypoint();
        }
    }
    /// <summary>
    /// goes through array of patrol points and moves agent accordingly
    /// </summary>
    private void SetNextWaypoint()
    {
        agent.SetDestination(waypoints[waypointIndex].transform.position);
        waypointIndex = (waypointIndex + 1) % waypoints.Length;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.yellow;
        float halfAngle = viewAngle / 2f;
        Vector3 leftDirection = Quaternion.Euler(0, -halfAngle, 0) * transform.forward * detectionRange;
        Vector3 rightDirection = Quaternion.Euler(0, halfAngle, 0) * transform.forward * detectionRange;

        Gizmos.DrawLine(transform.position, transform.position + leftDirection);
        Gizmos.DrawLine(transform.position, transform.position + rightDirection);
        Gizmos.DrawLine(transform.position + leftDirection, transform.position + rightDirection);

        DrawWaypointPath();
    }

    private void DrawWaypointPath()
    {
        if (waypoints.Length < 2)
        {
            return;
        }

        Gizmos.color = Color.blue;

        for (int i = 0; i < waypoints.Length; i++)
        {
            Vector3 currentWaypoint = waypoints[i].transform.position;
            int nextIndex = (i + 1) % waypoints.Length;
            Vector3 nextWaypoint = waypoints[nextIndex].transform.position;

            Gizmos.DrawLine(currentWaypoint, nextWaypoint);

            DrawArrowHead(currentWaypoint, nextWaypoint, 0.5f);
        }
    }

    private void DrawArrowHead(Vector3 from, Vector3 to, float arrowSize)
    {
        Vector3 direction = (to - from).normalized;
        Vector3 midpoint = from + (to - from) * 0.5f;

        Vector3 right = Vector3.Cross(direction, Vector3.up).normalized * arrowSize;
        Vector3 up = Vector3.Cross(right, direction).normalized * arrowSize;

        Vector3 arrowTip = midpoint + direction * arrowSize;

        Gizmos.DrawLine(midpoint + right, arrowTip);
        Gizmos.DrawLine(midpoint - right, arrowTip);
        Gizmos.DrawLine(midpoint + up, arrowTip);
        Gizmos.DrawLine(midpoint - up, arrowTip);
    }
}
