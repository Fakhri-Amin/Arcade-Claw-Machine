using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManagerUI : MonoBehaviour
{
    public static ScoreManagerUI Instance;
    [SerializeField] private TMP_Text scoreText;

    private int currentScore;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateUI(currentScore);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateUI(int scoreNumber)
    {
        currentScore += scoreNumber;
        scoreText.text = currentScore.ToString();
    }
}
