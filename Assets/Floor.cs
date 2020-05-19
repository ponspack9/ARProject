using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private MeshRenderer mesh_renderer;
    private float time = 10;
    public float max = 50;

    public Spawner spawner;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        mesh_renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= max)
            time = 0;

        mesh_renderer.material.SetFloat("_OFFSET", time);

        Color c = spawner.materials[player.color].color;
        Debug.Log(c.ToString());
        mesh_renderer.material.SetColor("_Color", c);
        //mesh_renderer.material.SetFloat("_Red", (float)c.r);
        //mesh_renderer.material.SetFloat("_Green",(float) c.g);
        //mesh_renderer.material.SetFloat("_Blue", (float)c.b);

        //mesh_renderer.material.SetFloat("_Red", 1);
        //mesh_renderer.material.SetFloat("_Green", 0);
        //mesh_renderer.material.SetFloat("_Blue", 0);

        //mesh_renderer.material.SetFloat("_COLOR", spawner.materials[player.color].color);
        // mesh_renderer.material.SetColor("_Color", spawner.materials[player.color].color);



    }
}
