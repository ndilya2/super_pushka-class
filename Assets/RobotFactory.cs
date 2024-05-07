using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotFactory : MonoBehaviour
{
    [SerializeField]
    private Transform Position;

    [SerializeField]
    private GameObject PrefabRobot;
    
    [SerializeField]
    private float TimerDuration; // Продолжительность таймера
    private float Timer;
    // Start is called before the first frame update
    void Start()
    {
        Timer = TimerDuration;
    }

    // Update is called once per frame
    void Update()
    {
        PrefabInstantiate();
    }

    private void PrefabInstantiate()
    {
        Debug.Log(Timer);
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        else
        {
            Instantiate(PrefabRobot, Position);
            Timer = TimerDuration;
        }
    }
}
