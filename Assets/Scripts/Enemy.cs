using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy : MonoBehaviour
{

    public float speed = 4.0f;

    private Player player;

    private Animator anim;  

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();  //the bot is trying to spawn where the player is
                                                                     
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float randomx = Random.Range(-8.5f, 8.5f);       //giving random positions and timeframe to spawn

        if (transform.position.y < -5.5f)
        {
            transform.position = new Vector3(randomx, 7, 0);
        }
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
     if(other.tag == "Player")
     {
           
            other.transform.GetComponent<Player>().Damage(); //gives damage to the player when it comes in contact

            anim.SetTrigger("OnEnemyDeath");
            speed = 0;

            Destroy(this.gameObject, 2f); //destroying itself when it comes in contact

            
     }

     if(other.tag == "Laser")
     {
            Destroy(other.gameObject);

            if(player != null)
            {
                player.AddScore(10);
            }

            anim.SetTrigger("OnEnemyDeath"); //gets destroyed when the laser hits it
            speed = 0;

            Destroy(this.gameObject, 2f);

     }


    }












}
