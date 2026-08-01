using UnityEngine;

public class EnemyStationary : MonoBehaviour
{
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float viewAngle = 120f;

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
    }
}