using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static int Fish;
    public int startingFish = 200; // 160

    // public static int Money;
    // public int startingMoney = 400;

    // public static int Lives;
    // public int startingLives = 100;

    void Start()
    {
        Fish = startingFish;
        // Money = startingMoney;
        // Lives = startingLives;
    }

    public void Update()
    {
    }

    /* This doesn't work but idk why */
    public bool enoughFish(int cost) {
        return Fish >= cost;
    }
}