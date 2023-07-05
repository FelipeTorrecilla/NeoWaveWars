using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBanner : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
               transform.position = hit.point;
            }
        }
    }
}
