using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GameController>();
            }
            return instance;
        }
    }

    public Player player;

    public Vector2 lastCheckpoint = new Vector2();

    void Update()
    {
    }
}
