using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private Vector3 _rotation;

    void Start()
    {
        _rotation = new Vector3(0, 0, 90f);
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

            if (hit && hit.collider.tag == "Pipe")
            {
                if (!hit.collider.GetComponent<Pipe>().HaveWater)
                    hit.collider.transform.Rotate(_rotation);
            }
        }
    }
}
