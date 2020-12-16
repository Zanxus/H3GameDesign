using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Lava : MonoBehaviour
{

    public float speed;
    public float acceleration;
    public int pauseTime;
    private int timer = 0;
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    Button restartGameButton;

    Collider2D Lavacollider2D;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveLava();
    }


    private void MoveLava()
    {
        timer++;
        if (timer * Time.deltaTime > pauseTime)
        {
            transform.position = new Vector3(mainCamera.transform.position.x, transform.position.y + speed, transform.position.z);
            if(timer% Time.deltaTime*10 == 0)
            {
                speed += acceleration;
                acceleration += acceleration;
            }
                
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.tag == "Player" || collision.tag == "Ground")
        {
            Destroy(collision.gameObject);
            if (collision.tag == "Player")
            {
                restartGameButton.gameObject.SetActive(true);
            }
            
        }
    }
}

