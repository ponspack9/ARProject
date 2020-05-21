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

    private int number_balls = 3;
    public int number_balls_on_lost = -1;
    private float amplitude = 1f;

    [Header("Tweaks")]
    private float angular_velocity = 0.5f;
    [Header("Colors")]
    public Material[] materials;

    public bool level_up = false;

    void Update()
    {
        transform.Rotate(transform.up,angular_velocity);

        transform.position = new Vector3(marker.transform.position.x, player.transform.position.y, marker.transform.position.z);

        if (level_up)
        {
            Invoke("LevelUp", 1.5f);
            level_up = false;
        }

        
    }
    public void LevelUp()
    {
        number_balls++;
        angular_velocity += 0.2f;
        if (number_balls % 3 == 0)
            amplitude += 0.1f;
        SpawnBalls();
        player.GetComponent<Player>().ResetPlayer();
    }
    public void StartGame()
    {
        number_balls = 3;
        amplitude = 1;
        angular_velocity = 0.5f;
        SpawnBalls();
    }
    public void SpawnBalls()
    {
        int n = (number_balls_on_lost > 0) ? number_balls_on_lost :number_balls;

        for (int i = 0; i < balls.Count; i++)
        {
            Destroy(balls[i]);
        }
        balls.Clear();

        Vector3 position = transform.position;
        float dx = 2 * Mathf.PI / number_balls;
        float angle = 0;
        for (int i = 0; i < n; i++, angle += dx)
        {
            position.x = transform.position.x + amplitude * Mathf.Cos(angle);
            position.z = transform.position.z + amplitude * Mathf.Sin(angle);

            balls.Add(Instantiate<GameObject>(ball, position, Quaternion.identity));

            int color = Random.Range(0, materials.Length);
            balls[balls.Count - 1].GetComponent<MeshRenderer>().material = materials[color];
            balls[balls.Count - 1].GetComponent<Missile>().color = color;
            balls[balls.Count - 1].transform.SetParent(this.transform);
        }
        number_balls_on_lost = -1;
        player.GetComponent<Player>().ResetPlayer();
    }

    public void SaveState()
    {
        number_balls_on_lost = balls.Count;
    }

}
