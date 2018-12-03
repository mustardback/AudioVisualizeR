using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquarePlacement : MonoBehaviour
{
    public GameObject circleCenter;
    public int radius = 5;

    // Use this for initialization
    void Start()
    {
        Vector3 center = transform.position;
        float ang = 0;
        for (int i = 0; i < 16; i++)
        {
            GameObject sphere = GameObject.Find("parametric-cube " + i);
            Vector3 newPos = CalculatePlacement(center, radius, ang);
            ang += 22.5f;
            sphere.transform.position = newPos;
        }
    }

    Vector3 CalculatePlacement(Vector3 center, float radius, float ang)
    {
        Vector3 pos;
        pos.x = center.x;
        pos.y = center.y + +radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }
}