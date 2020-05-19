using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public int color = 0;
    public float angle = 0;
    private static float angular_velocity = 1;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        angle += angular_velocity * Time.deltaTime;
        transform.position = new Vector3( 10 * Mathf.Cos(angle),transform.position.y,10 * Mathf.Sin(angle) );
    }
}
