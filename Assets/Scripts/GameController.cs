using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    private int score;
    private bool gameOver;
    private bool restart;

    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;

    void Start()
    {
        gameOver = false;
        restart = false;

        // Remove text fields
        restartText.text = "";
        gameOverText.text = "";

        // Reset score
        score = 0; 
        UpdateScore(); 
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown (KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];

                // Set random range to spawn hazards randomly along the x axis
                float randomXSpawnPosition = Random.Range(-spawnValues.x, spawnValues.x);
                Vector3 spawnPosition = new Vector3(randomXSpawnPosition, spawnValues.y, spawnValues.z);

                // Quaternion used to measure rotation much like Vector is used for position
                Quaternion spawnRotation = Quaternion.identity; // spawn object with no rotation .identity
                Instantiate(hazard, spawnPosition, spawnRotation);

                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                // Break out of infinite while loop to stop game
                restartText.text = "Press 'R' for restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        // Add score and update the Score text field
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Sirenie " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over - Pacient !";
        gameOver = true;
    }
}