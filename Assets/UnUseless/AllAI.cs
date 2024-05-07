using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllAI : MonoBehaviour
{
    public float speed; // скорость противника
    public float checkRadius; // радиус, в котором противник ищет препятствия
    public float viewDistance; // расстояние, на котором противник видит игрока
    public LayerMask obstacleLayer; // слой, на котором находятся препятствия
    public Transform player; // ссылка на игрока

    public bool playerInSight; // флаг, что игрок в зоне видимости
    public Vector3 destination; // текущая точка назначения
    public Vector3 direction; // направление движения

    public int Health;


    public virtual void Move()
    {

    }

    public virtual Vector3 GetRandomDestination()
    {
        Vector3 randomPoint = Random.insideUnitCircle * viewDistance;
        return transform.position + randomPoint;
    }

    public void OnDrawGizmosSelected()
    {
        // рисуем окружность, на которой противник ищет препятствия
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
        // рисуем луч, по которому противник видит игрока
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, player.position - transform.position);
        // рисуем радиус, в котором противник видит игрока
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, viewDistance);
    }

}
