using UnityEngine;

public class EnemyBestBeh : MonoBehaviour
{
    public AnimationCurve speedCurve;
    public float viewDistance;
    public LayerMask obstacleLayer;
    public Transform player;

    private bool playerInSight;
    private Vector3 destination;
    private Vector3 direction;

    private void Start()
    {
        destination = GetRandomDestination();
        direction = destination - transform.position;
    }

    private void Update()
    {
        EnemyMove();
    }

    private void EnemyMove()
    {
        // Check if the player is within the view distance
        if (Vector3.Distance(player.position, transform.position) < viewDistance)
        {
            // Check for obstacles between enemy and player
            RaycastHit hit;
            if (!Physics.Raycast(transform.position, player.position - transform.position, out hit, viewDistance, obstacleLayer) || hit.transform == player)
            {
                playerInSight = true;
            }
            else
            {
                playerInSight = false;
            }
        }
        else
        {
            playerInSight = false;
        }

        // Update speed based on the Animation Curve
        float currentTime = Time.time;
        float speed = speedCurve.Evaluate(currentTime);

        if (playerInSight)
        {
            destination = player.position;
            direction = destination - transform.position;
        }
        else
        {
            if (Vector3.Distance(transform.position, destination) < 1.0f)
            {
                destination = GetRandomDestination();
                direction = destination - transform.position;
            }
        }

        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    private Vector3 GetRandomDestination()
    {
        return new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
    }
}
