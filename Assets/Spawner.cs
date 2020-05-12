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
    public GameObject player;
    public GameObject missile;


    [Header("Tweaks")]
    public bool option1 = false;
    public float spawn_rate = 0.5f;
    public float missile_speed = 4.0f;

    [Header("Colors")]
    public Material[] materials;

    // Not shown
    private BoxCollider spawn_area;
    private float time = 0;

    void Start()
    {
        spawn_area = GetComponent<BoxCollider>();
    }

    void Update()
    {
        time += Time.deltaTime;

        if (time >= spawn_rate)
        {
            if (option1)
            {

                //Spawn
                Vector3 pos = RandomPointInBounds(spawn_area.bounds);

                GameObject ball = Instantiate<GameObject>(missile, pos, Quaternion.identity);
                Vector3 velocity = (player.transform.position - ball.transform.position).normalized;

                ball.GetComponent<Rigidbody>().velocity = velocity * missile_speed;
                ball.GetComponent<MeshRenderer>().material = materials[Random.Range(0, materials.Length)];

                Destroy(ball, 6.0f);
            }
            else
            {
                //Spawn
                GameObject ball = Instantiate<GameObject>(missile, transform.position, Quaternion.identity);

                Vector3 velocity = (player.transform.position - transform.position).normalized;

                ball.transform.position = RandomPointInBounds(spawn_area.bounds);
                ball.GetComponent<Rigidbody>().velocity = velocity * missile_speed;
                ball.GetComponent<MeshRenderer>().material = materials[Random.Range(0, materials.Length)];

                Destroy(ball, 6.0f);
            }

            time = 0;
        }
    }
    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

}
