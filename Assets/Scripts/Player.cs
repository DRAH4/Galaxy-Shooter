using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject laserPrefab;
    [SerializeField]
    GameObject tripleShotPrefab;
    [SerializeField]
    GameObject sheildVisualizer;

    private SpawnManager spawnManager;
    private UIManager uiManager;
    //geting the speed, can fire, lives, and power ups for the player
    [SerializeField]
    private float speed = 1;
    private float speedMultiplier = 2;

    public float fireRate = .5f;
    public float canFire = -1f;

    public int lives = 3;

    [SerializeField]
    private int score;

    [SerializeField]
    bool isTripleShotActive = false;
    bool isSpeedBoostActive = false;
    bool isSheildsActive = false;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);      //having the player spawn in and and the directions it can move

        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (spawnManager == null)
        {
            Debug.LogError("the spawn manager is null.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire) //how fast the player can fire
        {
            FireLaser();
        }
    }
    void Movement() // This function deals with movement
    {
        float horizontalInput = Input.GetAxis("Horizontal"); //movement of the okayer
        float VerticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, VerticalInput, 0); //geting the speed of how fast player can move
        transform.Translate(direction * speed * Time.deltaTime);

        if (transform.position.x > 9.3f)    //where the player can move to
        {
            transform.position = new Vector3(-9.3f, transform.position.y, 0);
        }
        if (transform.position.x < -9.3f)
        {
            transform.position = new Vector3(9.3f, transform.position.y, 0);
        }

        if (transform.position.y > 0f)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        if (transform.position.y < -4)
        {
            transform.position = new Vector3(transform.position.x, -4, 0);
        }
    }

    public void Damage()        //player is invicible when sheild is active and player can take damage if otherwise
    {
        if (isSheildsActive == true)
        { 

        isSheildsActive = false;
            sheildVisualizer.SetActive(false);
        return;
        
        }
            
    

        lives--;        //amount of lives player has and dies when there is none left

        uiManager.UpdateLives(lives);

        if (lives < 1)
        {
            spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        

        }
    }

    public void FireLaser()     //firing regular lasers and having triple shot
    {
        if (isTripleShotActive == true)
        {
            canFire = Time.time + fireRate;
            Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
        }

        else
        {
            canFire = Time.time + fireRate;
            Instantiate(laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
    }
    public void TripleShot()        //triple shot activation and time
    {
        isTripleShotActive = true;

        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isTripleShotActive = false;
    }

    public void SpeedBoost()        //speed boot activation and time
    {
        isSpeedBoostActive = true;
        speed *= speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        speed /= speedMultiplier;
        isSpeedBoostActive = false;
    }

    public void SheildsActive()     //having sheild active or not
    {
        isSheildsActive = true;
        sheildVisualizer.SetActive(true);
    }

    public void AddScore(int points) //score counter
    {
        score += points;
        uiManager.UpdateScore(score);
    }



    
}
