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
    public GameObject player;
    public GameObject marker;

    public int number_balls = 5;
    public float amplitude = 4;

    [Header("Tweaks")]
    private float angular_velocity = 0.5f;
    private float angle = 0;
    [Header("Colors")]
    public Material[] materials;

    private float time = 0;

    void Start()
    {
        //SpawnBalls();
    }

    void Update()
    {
        //transform.rotation = Quaternion.LookRotation(Vector3.up, Vector3.forward);
        //transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
        //m_Camera.transform.rotation * Vector3.up);
        //transform.position = marker.transform.position;

        transform.Rotate(transform.up,angular_velocity);
        transform.position = new Vector3(marker.transform.position.x, player.transform.position.y, marker.transform.position.z);
        //time += Time.deltaTime;
        time += Time.deltaTime;
        GetComponent<MeshRenderer>().material.SetFloat("_OFFSET", time);
        if (time >= 10000)
        {
            //Move balls
            //for (int i = 0; i < number_balls; i++)
            //{
            //    balls[i].transform.position.x =
            //}
            time = 0;
        }
    }
    
    public void SpawnBalls()
    {

        for (int i = 0; i < balls.Count; i++)
        {
            Destroy(balls[i]);
        }

        Vector3 position = transform.position;
        float dx = 2 * Mathf.PI / number_balls;
        float angle = 0;
        for (int i = 0; i < number_balls; i++, angle += dx)
        {
            position.x = transform.position.x + amplitude * Mathf.Cos(angle);
            position.z = transform.position.z + amplitude * Mathf.Sin(angle);

            balls.Add(Instantiate<GameObject>(ball, position, Quaternion.identity));
            balls[balls.Count-1].GetComponent<MeshRenderer>().material = materials[Random.Range(0, 6)];
            balls[balls.Count-1].GetComponent<Missile>().angle = angle;
            balls[balls.Count - 1].GetComponent<Missile>().offset = transform.position;
            balls[balls.Count - 1].GetComponent<Missile>().spawner = this;
            balls[balls.Count - 1].transform.SetParent(this.transform);
        }
    }

}
