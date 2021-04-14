using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int currentScore;

    public void AddScore(int value)
    {
        currentScore += value;
        scoreText.text = "x " + currentScore;
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            currentScore = PlayerPrefs.GetInt("Score");
            scoreText.text = "x " + currentScore;
            Debug.Log("Load Points");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlayerPrefs.SetInt("Score", currentScore);
        }
    }

    void OnEnable()
    {
        PlayerController.OnCoinCollected += AddScore;
    }

    void OnDisable()
    {
        PlayerController.OnCoinCollected -= AddScore;
    }
}
