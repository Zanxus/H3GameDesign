using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float highScore;
    private float travelScore;
    public static float pickUpScore;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Camera mainCamera;
    Vector2 startPos;
    Vector2 currentPos;
    public TextMeshProUGUI HighscoreGUI;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y,-10);
        startPos = player.transform.position;
        currentPos = startPos;
        travelScore = 0;
        pickUpScore = 0;
        highScore = 0;
        HighscoreGUI.text = $"HighScore:\n{highScore}";
        
    }

    // Update is called once per frame
    void Update()
    {

        mainCamera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y,-10);
        HighscoreGUI.text = $"HighScore:\n{highScore}";
        currentPos = player.transform.position;

        travelScore = Mathf.Floor(Vector2.Distance(startPos, currentPos));
        highScore = travelScore + pickUpScore;
    }
}
