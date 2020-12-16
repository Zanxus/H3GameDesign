using System.Collections;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static float highScore;
    private float travelScore;
    public static float pickUpScore;

    public int platformAmount;

    [SerializeField]
    public GameObject player;
    [SerializeField]
    private Camera mainCamera;
    Vector2 startPos;
    public TextMeshProUGUI HighscoreGUI;

    public GameObject[] platforms;
    public SortedList<int, string> highscoreList = new SortedList<int, string>(11);


    // Start is called before the first frame update
    void Start()
    {
        if (highscoreList.Count >= 11)
        {
            highscoreList.RemoveAt(10);
        }
        PlatformSpawner(platformAmount);
        InitScore();
        InitPos();
    }

    private void Platform_OnScoreEvent(float score)
    {
        pickUpScore += score;
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            UpdateCamera();
            UpdateHighscore(player.transform.position);
        }

    }

   void InitScore()
    {
        PlatformLogic.OnScoreEvent += Platform_OnScoreEvent;
        travelScore = 0;
        pickUpScore = 0;
        highScore = 0;
        HighscoreGUI.text = $"HighScore:\n{highScore}";
    }
    void InitPos()
    {
        mainCamera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        startPos = player.transform.position;
    }
    void UpdateCamera()
    {
        mainCamera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }
    void UpdateHighscore(Vector2 currentPos)
    {
        HighscoreGUI.text = $"HighScore:\n{highScore}";
        travelScore = Mathf.Floor(Vector2.Distance(startPos, currentPos));
        highScore = travelScore + pickUpScore;
    }

    void PlatformSpawner(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if(i%100 == 0 && i != 0)
            {
                Instantiate(platforms[2], new Vector3(Random.Range(-50, 50), Random.Range(i-100, i)), Quaternion.Euler(0, 0, -180));
                Instantiate(platforms[3], new Vector3(Random.Range(-50, 50), Random.Range(i - 100, i)), Quaternion.Euler(0, 0, -180));
                Instantiate(platforms[3], new Vector3(Random.Range(-50, 50), Random.Range(i - 100, i)), Quaternion.Euler(0, 0, -180));
            }

            if(i < amount*0.5)
            {
                if(Random.Range(0,100) > 10)
                {
                    Instantiate(platforms[0], new Vector3(Random.Range(-50, 50), Random.Range(0, 400)), Quaternion.Euler(0, 0, -180));
                }
                else
                {
                    Instantiate(platforms[1], new Vector3(Random.Range(-50, 50), Random.Range(0, 400)), Quaternion.Euler(0, 0, -180));
                }
                
            }
            else if (i < amount * 0.6)
            {
                if (Random.Range(0, 100) > 20)
                {
                    Instantiate(platforms[0], new Vector3(Random.Range(-50, 50), Random.Range(400, 500)), Quaternion.Euler(0, 0, -180));
                }
                else
                {
                    Instantiate(platforms[1], new Vector3(Random.Range(-50, 50), Random.Range(400, 500)), Quaternion.Euler(0, 0, -180));
                }
            }
            else
            {
                Instantiate(platforms[Random.Range(0, 2)], new Vector3(Random.Range(-50, 50), Random.Range(500, 1000)), Quaternion.Euler(0, 0, -180));
            }

            
        }
         
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    struct highscoreSave
    {
        string name;
        int score;
    }
}
