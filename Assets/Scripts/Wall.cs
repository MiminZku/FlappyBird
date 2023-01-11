using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float wallSpeed = 5;
    public ObstacleGenerator og;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-(wallSpeed + og.difficulty) * Time.deltaTime, 0, 0), Space.World);
    }
}
