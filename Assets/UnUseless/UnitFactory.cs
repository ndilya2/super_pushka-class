using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFactory : MonoBehaviour
{
 public GameObject unitPrefab;
    public AnimationCurve spawnSpeedCurve;
    public Transform spawnPoint;
    public float spawnInterval = 1.0f;

    private float timer = 0.0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnUnit();
            timer = 0.0f;
        }
    }

    void SpawnUnit()
    {
        Vector3 spawnPosition = spawnPoint.position;
        Instantiate(unitPrefab, spawnPosition, Quaternion.identity);

        // Изменяем интервал между созданием юнитов в соответствии с Animation Curve
        float curveValue = spawnSpeedCurve.Evaluate(timer / spawnInterval);
        spawnInterval = Mathf.Lerp(0.1f, curveValue, curveValue); // Можно настроить диапазон скоростей
        
        Debug.Log("Spawned a unit at " + spawnPosition + "! Next spawn in " + spawnInterval + " seconds.");
    }
}
