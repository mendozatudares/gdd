using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private static int score;
    private static Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        score = 0;
        UpdateText();
    }

    public static void UpdateScore()
    {
        score += 1;
        UpdateText();
    }

    private static void UpdateText()
    {
        scoreText.text = string.Format("Score: {0}", score);
    }
}
