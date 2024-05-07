using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyBehaviour : AllAI
{
    public string[] Dialoges;
    public int counter;

    public GameObject PanelText;
    public TMP_Text DialogeText;
    public bool InPlayer;

    private void Start()
    {
        // выбираем случайную точку назначения
        destination = GetRandomDestination();
        // вычисляем направление движения
        direction = destination - transform.position;
        PanelText.SetActive(false);
        DialogeText.text = Dialoges[counter];
    }

    private void Update()
    {
        Move();
        StartDialog();
    }

    public override void Move()
    {
        // если достигли текущей точки назначения, выбираем новую
        if (Vector3.Distance(transform.position, destination) < checkRadius)
        {
            destination = GetRandomDestination();
            direction = destination - transform.position;
        }
        // проверяем, есть ли на пути препятствия
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, obstacleLayer))
        {
            // если есть, выбираем новое направление
            destination = GetRandomDestination();
            direction = destination - transform.position;
        }

        // движение противника
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InPlayer = true;
            speed = 0;
        }
    }

    public void NextText()
    {
        counter++;
        DialogeText.text = Dialoges[counter];
        Debug.Log("NEXT");
    }

    public void PrevText()
    {
        counter--;
        DialogeText.text = Dialoges[counter];
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InPlayer = false;
            PanelText.SetActive(false);
            speed = 0.1f;
        }
    }

    void StartDialog()
    {
        if (InPlayer && Input.GetKeyDown(KeyCode.E))
        {
            PanelText.SetActive(true);
            DialogeText.text = Dialoges[counter];
        }
    }
}
