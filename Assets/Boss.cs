using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
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

    [Header("Colors")]
    public Material[] materials;

    public GameObject ball;
    public List<GameObject> balls;

    public int number_balls = 5;
    public float offset = 10;

    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        balls = new List<GameObject>();
        SpawnBalls();
    }

    void SpawnBalls()
    {
        Vector3 position = transform.position;
        float dx = 2 * Mathf.PI / number_balls;
        float angle = 0;
        for (int i =0; i< number_balls; i++, angle+=dx)
        {
            position.x = offset*Mathf.Cos(angle);
            position.z = offset*Mathf.Sin(angle);
            balls.Add(Instantiate<GameObject>(ball, position, Quaternion.identity));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
