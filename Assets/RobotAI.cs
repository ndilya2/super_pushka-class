using UnityEngine;
using UnityEngine.AI;

public class RobotAI : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent Agent;
    [SerializeField]
    private Vector3 Destination;
    [SerializeField]
    private LayerMask ObstacleLayer, EnemyLayer;
    [SerializeField]
    private float Distance;
    [SerializeField]
    private GameObject projectilePrefab;

    private void SetNewDestination()
    {
        Destination = new Vector3(Random.Range(-20, 20), Random.Range(-20, 20));
        Agent.SetDestination(Destination);
    }

    private void CheckForObstacleAndEnemys()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, Distance, ObstacleLayer);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Obstacle"))
            {
                Debug.LogWarning("Obstacle");
                SetNewDestination();
            }
        }

        colliders = Physics.OverlapSphere(transform.position, Distance, EnemyLayer);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Debug.Log("Enemy Spotted");
                LaunchProjectile(collider.gameObject); // Запускаем снаряд в цель
            }
        }

        if (colliders.Length == 0)
        {
            Debug.Log("Nothing");
        }
    }
    private void LaunchProjectile(GameObject target)
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().AddForce((target.transform.position - transform.position) * 10f, ForceMode.Impulse);
        Destroy(projectile, 2f); // Уничтожаем снаряд через 2 секунды
    }


    // Start is called before the first frame update
    void Start()
    {
        SetNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Agent.pathPending && Agent.remainingDistance < 0.5f)
        {
            SetNewDestination();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Distance);
    }
}
