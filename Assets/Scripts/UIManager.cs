using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour          //this has all the UI texts, lives and points managed
{
    [SerializeField]
    Text scoreText;

    [SerializeField]
    Text gameOverText;

    [SerializeField]
    Text restartText;

    [SerializeField]
    Sprite[] livesSprites;

    [SerializeField]
    Image livesImage;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + 0;
        gameOverText.gameObject.SetActive(false);       //this is all the score stuf

        gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int playerScore)        //updating each kill to a score
    {
        scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)       //updating lives when lost
    {
        livesImage.sprite = livesSprites[currentLives];

        if(currentLives == 0)
        {
            gameOverSequence();
        }
    }

    public void gameOverSequence()      //when no lives this is a gameover
    {
        gameManager.GameOver();
        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlicker());

    }




    IEnumerator GameOverFlicker()       //having the screen flicker when gameover
    {
        while(true)
        {
            gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(.5f);

            gameOverText.text = " ";
            yield return new WaitForSeconds(.5f);
        }
    }

}
