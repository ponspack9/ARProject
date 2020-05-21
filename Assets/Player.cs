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

    public int points = 0;

    public int color = 0;
    private float time = 0;
    public float original_distance = 1000;
    public float distance = 0;
    private bool shot = false;

    private Vector3 original_pos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
        rigid = GetComponent<Rigidbody>();
        
    }
    public void StartGame()
    {
        score.text = "SCORE: 0";
        ChangeColor();
        ResetPlayer();
        original_pos = transform.position;
        original_distance = (spawner.transform.position - transform.position).magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        distance = (original_pos - transform.position).magnitude;
        if (distance > original_distance+10)
        {

            ResetPlayer();
        }

    }

    public void Shoot()
    {
        original_pos = transform.position;
        
        //rigid.AddForce(new Vector3(0, 0, -slider.value));
        rigid.AddForce(transform.forward.normalized * slider.value);
        shot = true;

    }

    public void ChangeColor()
    {
        color = (color +1)%(int)Spawner.Colors.MAX;
        GetComponent<MeshRenderer>().material = spawner.materials[color];
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (GameController.gameOver) return;

        if (collision.transform.tag == "Missile")
        {
            if (shot && collision.gameObject.GetComponent<Missile>().color == this.color)
            {
                points += 100;
                score.text = "SCORE: " + points.ToString();
                //Debug.Log("Collision: " + gameObject.name);
                //Debug.Log("Collision2: " + collision.gameObject.name);

                spawner.balls.Remove(collision.gameObject);

                if (spawner.balls.Count <= 0)
                    spawner.level_up = true;
            }
            else
            {
                GameController.gameOver = true;
            }
            //ResetPlayer();
            Destroy(collision.gameObject);
        }
    }

    private void ResetPlayer()
    {
        transform.localPosition = Vector3.zero;
        rigid.ResetInertiaTensor();
        rigid.ResetCenterOfMass();
        rigid.angularVelocity = Vector3.zero;
        rigid.velocity = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        shot = false;
    }
}
