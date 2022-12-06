using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Board Generator")]
    [SerializeField] private int rowLength;
    [SerializeField] private int columnLength;
    [SerializeField] private float xStart;
    [SerializeField] private float yStart;
    [SerializeField] private float xStart2;
    [SerializeField] private float xSpacing;
    [SerializeField] private float ySpacing;
    [SerializeField] GameObject pegPrefab;

    [Header("Score Handling")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Canvas gameOverScreen;
    private int globalScore;

    [Header("Ball Handling")]
    private Vector3 mousePosition;
    [SerializeField] private GameObject ball;
    private GameObject thisBall;
    private bool ballDropped;

    public void Awake()
    {
        //Singleton housekeeping
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Populate pegs
        StartCoroutine(ResetBoard());
    }
    private void Update()
    {
        //If the player clicks the mouse, the ball is dropped
        if(Input.GetMouseButtonDown(0) && !ballDropped && !gameOverScreen.enabled)
        {
            thisBall.GetComponent<Rigidbody>().velocity = Vector3.zero;               
            ballDropped = true;
        }

        //Move ball back and forth at the top of the screen waiting for player to click mouse
        if(!ballDropped)
        {
            if(thisBall == null)
            {
                return;
            }
            else
            {
                thisBall.transform.position = Vector3.Lerp(new Vector3(-2.15f, 4.5f, 0), new Vector3(2.4f, 4.5f, 0), Mathf.Abs(Mathf.Sin(Time.time)));
            }
        }
    }

    //Function to add to the player's score every time a collision is detected
    public void SetScore(int score)
    {
        globalScore += score;
        scoreText.text = "Total Score: " + globalScore.ToString();
    }

    //Resets the board with new pegs with different score values and creates a new ball ready to drop
    public IEnumerator ResetBoard()
    {
        yield return new WaitForSeconds(1);
        globalScore = 0;
        gameOverScreen.enabled = false;
        ballDropped = false;
        thisBall = Instantiate(ball, new Vector3(0, 4.4f, 0), Quaternion.identity);
        Vector3 pegPosition = Vector3.zero;

        //Code to generate the peg board. Also changes the color of the peg depending on its score value
        for (int i = 0; i < rowLength; i++)
        {

            for (int j = 0; j < columnLength; j++)
            {
                if (i % 2 == 0)
                {
                    pegPosition = new Vector3(xStart + (xSpacing * (j % columnLength)), yStart + (-ySpacing * i), 0);

                }
                else
                {
                    pegPosition = new Vector3(xStart2 + (xSpacing * (j % columnLength)), yStart + (-ySpacing * i), 0);
                }

                GameObject thisPeg = Instantiate(pegPrefab, pegPosition, pegPrefab.transform.rotation);
                PegController thisController = (PegController)thisPeg.GetComponent(typeof(PegController));
                Debug.Log(thisController.getPointValue());
                thisPeg.GetComponent<Renderer>().material.color = new Color(thisController.getPointValue() / 255, thisController.getPointValue() / 100, thisController.getPointValue());

            }


        }
    }

    //Activates game over screen once ball has finished its fall
    public void GameOverScreen()
    {
        gameOverScreen.enabled = true;
    }

    //reset button function, resets game board
    public void OnClick()
    {
        StartCoroutine(ResetBoard());
    }

    //checks to see if game is over
    public bool GameOver()
    {
        return gameOverScreen.enabled;
    }


}
