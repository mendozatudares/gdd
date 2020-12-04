using Assets.Code;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static AudioClip ScoreAudioClip;
    private static AudioSource _audioSource;
    private static int _score;
    private static Text _scoreText;
    private static int _goal;

    // Start is called before the first frame update
    void Start()
    {
        ScoreAudioClip = Resources.Load<AudioClip>("Sounds/score");
        _audioSource = GetComponent<AudioSource>();
        _scoreText = GetComponent<Text>();
        _score = 0;
        _goal = Camera.main.GetComponent<LevelManager>().enemyNumber;
        UpdateText();
    }

    public static void UpdateScore()
    {
        _score += 1;
        _audioSource.PlayOneShot(ScoreAudioClip);
        UpdateText();
        if (_score >= _goal)
            Camera.main.GetComponent<LevelManager>().CallNextLevel();
    }

    private static void UpdateText()
    {
        _scoreText.text = $"Score: {_score}";
    }
}
