using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLogic : MonoBehaviour
{
    private bool haveBeenTagged = false;
    public float score = 0;


    public delegate void scoreEventDelegate(float score);
    public static event scoreEventDelegate OnScoreEvent;

    Collider2D platformCollider2D;
    [SerializeField]
    GameObject player;
    Collider2D playerCollider2D;

    [SerializeField]
    Color scoreColor;


    float triggerTime = 0.1f;
    int collitionTime = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        platformCollider2D = GetComponent<Collider2D>();
        playerCollider2D = player.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForPlatformScoring();
    }

    private void CheckForPlatformScoring()
    {
        if (haveBeenTagged == false)
        {
            if (player != null)
            {
                if (platformCollider2D.IsTouching(playerCollider2D))
                {
                    collitionTime++;
                    if (collitionTime * Time.deltaTime > triggerTime)
                    {

                        OnScoreEvent?.Invoke(score);
                        this.GetComponent<SpriteRenderer>().color = scoreColor;
                        haveBeenTagged = true;

                    }
                    else if (!platformCollider2D.IsTouching(playerCollider2D))
                    {
                        collitionTime = 0;
                    }
                }
            }

        }
    }
}

