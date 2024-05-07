using System.Collections.Generic;
using UnityEngine;

public class SelectAndMoveManager : MonoBehaviour
{
    public List<GameObject> selectedUnits = new List<GameObject>();
    public Vector3 targetPosition;
    public bool isMoving = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 clickPosition = hit.point;
                SetTargetPosition(clickPosition);
            }
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            SelectUnitsUnderMouse();
        }

        if (isMoving)
        {
            MoveUnits();
        }
    }

    void SelectUnitsUnderMouse()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            GameObject selectedUnit = hit.collider.gameObject;

            if (selectedUnit.gameObject.tag == "Unit")
            {
                if (!selectedUnits.Contains(selectedUnit))
                {
                    selectedUnits.Add(selectedUnit);
                }
            }
        }
    }

    void MoveUnits()
    {
        foreach (GameObject unit in selectedUnits)
        {
            if (Vector3.Distance(unit.transform.position, targetPosition) > 0.1f)
            {
                unit.transform.position = Vector3.MoveTowards(unit.transform.position,
                targetPosition, Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        foreach (GameObject unit in selectedUnits)
        {
            if (other.gameObject.tag == "Unit")
            {
                unit.transform.position = Vector3.MoveTowards(unit.transform.position, targetPosition, Time.deltaTime);
            }
        }
    }

    void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
        isMoving = true;
    }
}
