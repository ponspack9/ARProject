﻿using System.Collections;
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

    private int number_balls = 3;
    private float amplitude = 1f;

    [Header("Tweaks")]
    private float angular_velocity = 0.5f;
    [Header("Colors")]
    public Material[] materials;

    private float time = 0;
    public bool level_up = false;


    void Update()
    {
        transform.Rotate(transform.up,angular_velocity);

        transform.position = new Vector3(marker.transform.position.x, player.transform.position.y, marker.transform.position.z);

        //time += Time.deltaTime;
        time += Time.deltaTime;
        GetComponent<MeshRenderer>().material.SetFloat("_OFFSET", time);
        if (time >= 10000)
        {
            time = 0;
        }
        if (level_up)
        {
            Invoke("LevelUp", 1.5f);
            level_up = false;
        }

        
    }
    public void LevelUp()
    {
        number_balls++;
        angular_velocity += 0.25f;
        SpawnBalls();
    }
    public void StartGame()
    {
        number_balls = 3;
        angular_velocity = 0.5f;
        SpawnBalls();
    }
    public void SpawnBalls()
    {

        for (int i = 0; i < balls.Count; i++)
        {
            Destroy(balls[i]);
        }
        balls.Clear();

        Vector3 position = transform.position;
        float dx = 2 * Mathf.PI / number_balls;
        float angle = 0;
        for (int i = 0; i < number_balls; i++, angle += dx)
        {
            position.x = transform.position.x + amplitude * Mathf.Cos(angle);
            position.z = transform.position.z + amplitude * Mathf.Sin(angle);

            balls.Add(Instantiate<GameObject>(ball, position, Quaternion.identity));

            int color = Random.Range(0, materials.Length);
            balls[balls.Count - 1].GetComponent<MeshRenderer>().material = materials[color];
            balls[balls.Count - 1].GetComponent<Missile>().color = color;
            balls[balls.Count - 1].transform.SetParent(this.transform);
        }
    }

}
