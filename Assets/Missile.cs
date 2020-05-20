using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public int color = 0;
    public float angle = 0;
    private static float angular_velocity = 1;

    public Spawner spawner;
    public Vector3 offset = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        ////Vector3 pos = spawner.transform.position;
        //angle += angular_velocity * Time.deltaTime;
        //transform.localPosition = new Vector3( 
        //    /*offset.x + */spawner.amplitude * Mathf.Cos(angle), 
        //    /*offset.y + */transform.position.y,
        //    /*offset.z + */spawner.amplitude * Mathf.Sin(angle) );
    }
}
