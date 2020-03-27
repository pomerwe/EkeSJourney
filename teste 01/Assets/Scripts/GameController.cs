using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Player player;
    public Text playerScore;

    void Update()
    {
        playerScore.text = player.score.ToString();
    }
}
