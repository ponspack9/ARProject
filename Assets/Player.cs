using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Spawner spawner;
    public Slider slider;
    public Text score;
    private Rigidbody rigid;

    private int points = 0;

    public int color = 0;
    private float time = 0;

    private Vector3 original_pos = Vector3.zero;
    public float change_rate = 4.0f;
    public bool game_over = false;

    // Start is called before the first frame update
    void Start()
    {
        score.text = "SCORE: 0";
        original_pos = transform.position;
        rigid = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((original_pos - transform.position).magnitude > 9)
        {
            Debug.Log("RESTART");
        }
        time += Time.deltaTime;
        if (time >= change_rate)
        {
            //ChangeColor();
            time = 0;
        }
    }

    public void Shoot()
    {
        original_pos = transform.position;
        rigid.AddForce(new Vector3(0, 0, -slider.value));

    }

    void ChangeColor()
    {
        color = Random.Range(0, (int)Spawner.Colors.MAX);
        GetComponent<MeshRenderer>().material = spawner.materials[color];
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (transform.tag == "Player" && collision.transform.tag == "Missile")
        {
            if (collision.gameObject.GetComponent<Missile>().color == this.color)
            {
                points += 100;
                score.text = "SCORE: " + points.ToString();
                Debug.Log("Collision: " + gameObject.name);
                Debug.Log("Collision2: " + collision.gameObject.name);
                
            }
            else
            {
                game_over = true;
            }
            Destroy(collision.gameObject);
        }
    }
    void OnCollisionExit(Collision collisionInfo)
    {
        print("Collision Out: " + gameObject.name);
    }
}
