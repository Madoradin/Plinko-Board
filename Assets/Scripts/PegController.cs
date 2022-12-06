using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PegController : MonoBehaviour
{

    [SerializeField] private int pointValue;
    private Material pegMaterial;
    [SerializeField] private TMP_Text scoreText;



    // generate a random point value for the peg
    void Awake()
    {
        pointValue = Random.Range(10, 255);
    }

    // If game is over, this peg is destroyed
    void Update()
    {
        if (GameManager.Instance.GameOver())
        {
            Destroy(gameObject);
        }
    }

    //If a ball collides with this peg, update the game score
    private void OnCollisionEnter(Collision collision)
    {
        GameManager.Instance.SetScore(pointValue);
    }

    public int getPointValue()
    {
        return pointValue;
    }

 
}
