using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Spawner spawner;
    public Text score;
    private int points = 0;

    public int color = 0;
    private float time = 0;

    public float change_rate = 4.0f;
    public bool game_over = false;

    // Start is called before the first frame update
    void Start()
    {
        score.text = "SCORE: 0";
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= change_rate)
        {
            ChangeColor();
            time = 0;
        }
    }

    void ChangeColor()
    {
        color = Random.Range(0, (int)Spawner.Colors.MAX);
        GetComponent<MeshRenderer>().material = spawner.materials[color];
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Missile")
        {
            if (collision.gameObject.GetComponent<Missile>().color == this.color)
            {
                points += collision.gameObject.GetComponent<Missile>().points;
                score.text = "SCORE: " + points.ToString();
            }
            else
            {
                game_over = true;
            }
        }
    }
}
