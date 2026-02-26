using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public PaddleController paddle;
    public BallController ball;

    [Header("Brick Spawn")]
    public GameObject brickPrefab;
    public int rows = 6;
    public int cols = 10;
    public Vector2 startPos = new Vector2(-6.3f, 3.5f);
    public Vector2 spacing = new Vector2(1.45f, 0.6f);

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text livesText;
    public TMP_Text centerMessage;

    int _score;
    int _lives = 3;

    readonly List<GameObject> _bricks = new List<GameObject>();

    void Start()
    {
        NewGame();
    }

    void NewGame()
    {
        _score = 0;
        _lives = 3;
        UpdateUI();

        SpawnBricks();

        centerMessage.gameObject.SetActive(true);
        centerMessage.text = "Press Space";

        ball.AttachToPaddle(paddle.transform);
    }

    void SpawnBricks()
    {
        foreach (var b in _bricks)
            if (b) Destroy(b);
        _bricks.Clear();

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                Vector2 pos = startPos + new Vector2(c * spacing.x, -r * spacing.y);
                var brick = Instantiate(brickPrefab, pos, Quaternion.identity);
                var hit = brick.GetComponent<BrickHit>();
                if (hit != null) hit.gm = this;
                _bricks.Add(brick);
            }
        }
    }

    void UpdateUI()
    {
        scoreText.text = $"Score: {_score}";
        livesText.text = $"Lives: {_lives}";
    }

    public void OnBrickDestroyed(int points)
    {
        _score += points;
        UpdateUI();

        bool anyAlive = false;
        for (int i = 0; i < _bricks.Count; i++)
        {
            if (_bricks[i] != null)
            {
                anyAlive = true;
                break;
            }
        }

        if (!anyAlive)
        {
            centerMessage.gameObject.SetActive(true);
            centerMessage.text = "YOU WIN!\nPress R to Restart";
        }
    }

    public void OnBallLost()
    {
        _lives--;
        UpdateUI();

        if (_lives <= 0)
        {
            centerMessage.gameObject.SetActive(true);
            centerMessage.text = "GAME OVER\nPress R to Restart";
        }

        ball.AttachToPaddle(paddle.transform);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            centerMessage.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            NewGame();
        }
    }
}