using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour  //this is managing all the powerups in the game
{
   
    [SerializeField]
    int speed = 1;

    [SerializeField]
    int powerupID;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)     //saying when it collides with player they activate it and it disapears
    {
        switch (powerupID)
        {
            case 0:
                if (other.tag == "Player")
                {
                    Player player = other.transform.GetComponent<Player>();
                    if (player != null)
                    {
                        player.TripleShot();
                    }

                    Destroy(this.gameObject);

                }
                break;


            case 1:
                if (other.tag == "Player")
                {
                    Player player = other.transform.GetComponent<Player>();
                    if (player != null)
                    {
                        player.SpeedBoost();
                    }

                    Destroy(this.gameObject);

                }
                break;


            case 2:
                if (other.tag == "Player")
                {
                    Player player = other.transform.GetComponent<Player>();
                    if (player != null)
                    {
                        player.SheildsActive();
                    }

                    Destroy(this.gameObject);

                }
                break;
        }








    }

}
