using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject[] obstacles;
    public Text scoreText;
    public int difficulty = 1;

    float timeAfterSpawn = 0;
    float spawnTime = 2;
    float time = 0;

    int obstacleCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(difficulty);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeAfterSpawn += Time.deltaTime;
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
        }
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
        scoreText.text = "Score : " + obstacleCount.ToString();
    }

    void Generate()
    {
    }
}
