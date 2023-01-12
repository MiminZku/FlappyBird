using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject inGameScreen;
    public GameObject gameOverScreen;
    public GameObject[] obstacles;
    public GameObject bird;
    public GameObject scoreText;
    public int difficulty = 1;

    float timeAfterSpawn = 0;
    float spawnTime = 2;
    float time = 0;
    int obstacleCount = 0;
    public bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeAfterSpawn += Time.deltaTime;
        GenerateObstacle();
        if (time > 10 * difficulty)
        {
            if(difficulty < 6)
            {
                difficulty++;
            }
            if(spawnTime > 1)
            {
                spawnTime -= 0.2f;
            }
            Debug.Log(difficulty);
        }

        if (bird.GetComponent<Bird>().isDead)
        {
            int bestScore = obstacleCount;
            if(PlayerPrefs.HasKey("Best Score"))
            {
                bestScore = PlayerPrefs.GetInt("Best Score");
            }
            if (bestScore < obstacleCount)
            {
                PlayerPrefs.SetInt("Best Score", obstacleCount);
            }
            gameOverScreen.SetActive(true);
            gameOverScreen.transform.GetChild(0).GetComponent<Text>().text = 
                "Best Score : " + bestScore.ToString();
            Time.timeScale = 0;
            GetComponent<AudioSource>().Stop();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                isPaused = false;
                inGameScreen.SetActive(false);
                GetComponent<AudioSource>().Play();
                Time.timeScale = 1;
            }
            else
            {
                isPaused = true;
                inGameScreen.SetActive(true);
                GetComponent<AudioSource>().Pause();
                Time.timeScale = 0;
            }
        }
    }

    void GenerateObstacle()
    {
        if (timeAfterSpawn > spawnTime)
        {
            int obstacle = Random.Range(0, obstacles.Length);
            float y = 0;
            switch (obstacle)
            {
                case 0:
                    y = Random.Range(-8f, difficulty - 7.5f);
                    break;
                case 1:
                    y = Random.Range(9f - difficulty, 9.5f);
                    break;
                case 2:
                    y = Random.Range(-1f, 3f);
                    break;
            }
            GameObject obj = Instantiate(obstacles[obstacle].gameObject);
            obj.GetComponent<Wall>().og = GetComponent<ObstacleGenerator>();
            obj.transform.position = new Vector2(15, y);
            obstacleCount++;
            timeAfterSpawn = 0;
            scoreText.GetComponent<Text>().text = "Score : " + obstacleCount.ToString();
        }
    }

    public void OnStartButtonClick()
    {
        GetComponent<AudioSource>().Play();
        startScreen.SetActive(false);
        scoreText.SetActive(true);
        Time.timeScale= 1.0f;
    }

    public void OnGoToMainButtonClick()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
