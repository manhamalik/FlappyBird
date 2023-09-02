using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Player player;
    private Spawner spawner;

    public Text scoreText;
    public Text highScoreText;
    public Text scoreOverText;

    public GameObject scoreTextGameObject;
    public GameObject playButton;
    public GameObject gameOver;
    public GameObject flappyBirdText;

    public int score { get; private set; }
    private int highScore;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();
        gameOver.SetActive(false);
        scoreTextGameObject.SetActive(false);
        flappyBirdText.SetActive(true);

        // Load the high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        Pause();
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);
        flappyBirdText.SetActive(false);
        scoreTextGameObject.SetActive(true);

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }

    public void GameOver()
    {
        playButton.SetActive(true);
        gameOver.SetActive(true);

        // Update high score if necessary
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        // Update score and high score texts
        scoreText.text = score.ToString();
        highScoreText.text = highScore.ToString();

        // Set the final score in the scoreOver text
        scoreOverText.text = score.ToString();

        scoreTextGameObject.SetActive(false);
        flappyBirdText.SetActive(false);

        Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}