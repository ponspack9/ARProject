using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public enum Colors
    {
        RED = 0,
        GREEN,
        BLUE,
        MAGENTA,
        CYAN,
        YELLOW,
        
        MAX
    }

    [Header("Prefabs")]
    public GameObject ball;
    public List<GameObject> balls;

    public int number_balls = 5;
    public float offset = 10;

    [Header("Tweaks")]
    public float angular_velocity = 0.5f;

    [Header("Colors")]
    public Material[] materials;

    private float time = 0;

    void Start()
    {
        SpawnBalls();
    }

    void Update()
    {
        //time += Time.deltaTime;

        //if (time >= angular_velocity)
        //{
        //    //Move balls
        //    for (int i = 0; i < number_balls; i++)
        //    {
        //        balls[i].transform.position.x = 
        //    }
        //    time = 0;
        //}
    }
    void SpawnBalls()
    {
        Vector3 position = transform.position;
        float dx = 2 * Mathf.PI / number_balls;
        float angle = 0;
        for (int i = 0; i < number_balls; i++, angle += dx)
        {
            position.x = offset * Mathf.Cos(angle);
            position.z = offset * Mathf.Sin(angle);

            balls.Add(Instantiate<GameObject>(ball, position, Quaternion.identity));
            balls[balls.Count-1].GetComponent<MeshRenderer>().material = materials[Random.Range(0, 6)];
            balls[balls.Count-1].GetComponent<Missile>().angle = angle;
        }
    }

}
