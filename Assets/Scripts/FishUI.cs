using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishUI : MonoBehaviour
{
    public Text fishText;

    void Update()
    {
        fishText.text = PlayerStats.Fish.ToString();
    }
}
