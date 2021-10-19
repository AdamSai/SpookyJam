using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [SerializeField]
    Text scoreText;

    void Start()
    {
        RedrawScore();
    }

    void RedrawScore()
    {
        scoreText.text = "Score: " + GameManager.score.ToString();
    }
}
