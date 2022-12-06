using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    //Destroy this object and bring up reset button once the ball is on the ground
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            GameManager.Instance.GameOverScreen();
            Destroy(gameObject);
        }
    }
}
