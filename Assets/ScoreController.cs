using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text scoreText;
    public Altimeter altimeter;

    private void Update()
    {
        scoreText.text = altimeter.GetLastAltitude().ToString();
    }
}
