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
    public ParticleSystem particles;

    public AudioSource audio_shoot;
    public AudioSource audio_match;
    public AudioSource audio_error;

    public int points = 0;

    public int color = 0;
    public int collisions = 1;
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
        points = 0;
        collisions = 1;
        //ChangeColor();
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
        audio_shoot.Play();
        //rigid.AddForce(new Vector3(0, 0, -slider.value));
        rigid.AddForce(transform.forward.normalized * slider.value);
        shot = true;
        collisions = 1;

    }

    public void ChangeColor(int c = -1)
    {
        if (c == -1)
            color = (color + 1) % (int)Spawner.Colors.MAX;
        else
            color = c;
        GetComponent<MeshRenderer>().material = spawner.materials[color];
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (GameController.gameOver) return;

        if (collision.transform.tag == "Missile")
        {
            if (shot && collision.gameObject.GetComponent<Missile>().color == this.color)
            {
                points += collisions * 100;
                collisions++;
                score.text = "SCORE: " + points.ToString();
                particles.transform.position = collision.transform.position;
                particles.GetComponent<ParticleSystemRenderer>().material = spawner.materials[this.color];
                Instantiate(particles);//.GetComponent<ParticleSystem>().Play() ;

                audio_match.Play();
                //Debug.Log("Collision: " + gameObject.name);
                //Debug.Log("Collision2: " + collision.gameObject.name);

                spawner.balls.Remove(collision.gameObject);

                if (spawner.balls.Count <= 0)
                    spawner.level_up = true;
            }
            else
            {
                audio_error.Play();
                GameController.gameOver = true;
            }
            //ResetPlayer();
            Destroy(collision.gameObject);
        }
    }

    public void ResetPlayer()
    {

        transform.localPosition = Vector3.zero;
        rigid.ResetInertiaTensor();
        rigid.ResetCenterOfMass();
        rigid.angularVelocity = Vector3.zero;
        rigid.velocity = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        shot = false;
        collisions = 1;

        List<int> colors = new List<int>();

        for (int i=0;i<spawner.balls.Count;i++)
        {
            int c = spawner.balls[i].GetComponent<Missile>().color;
            if(!colors.Contains(c))
                colors.Add(c);
        }

        ChangeColor(colors[Random.Range(0, colors.Count)]);
    }
}
