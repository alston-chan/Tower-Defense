using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishUI : MonoBehaviour
{
    public TextMeshProUGUI fishText;

    void Update()
    {
        fishText.text = PlayerStats.Fish.ToString();
    }
}
