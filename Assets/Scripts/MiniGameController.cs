using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;  


public class MiniGameController : MonoBehaviour
{
    public float gameDuration = 30f; // Dur�e du mini-jeu en secondes
    public TextMeshProUGUI timerText; // Affichage du timer
    public GameObject[] arrows; // Tableau des fl�ches dans la sc�ne
    public int score = 0;
    public TextMeshProUGUI scoreText;

    private float timeRemaining;
    private bool gameActive = false;

  

    void Update()
    {
        if (gameActive)
        {
            // G�rer le d�compte du timer
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                timerText.text = "Time: " + Mathf.Floor(timeRemaining).ToString(); // Afficher le timer
            }
            else
            {
                EndGame();
            }
        }
    }

    public void StartGame()
    {
        gameActive = true;
        timeRemaining = gameDuration;
        timerText.text = "Time: " + gameDuration.ToString();
        Debug.Log("Mini-jeu d�marr� !");
    }

    void EndGame()
    {
        gameActive = false;
        timerText.text = "Time's Up!";
        DestroyArrows(); // Supprimer les fl�ches � la fin du temps
        Debug.Log("Mini-jeu termin� !");
    }

    void DestroyArrows()
    {
        GameObject[] arrowsInScene = GameObject.FindGameObjectsWithTag("Arrow");
        foreach (GameObject arrow in arrowsInScene)
        {
            Destroy(arrow);
        }
    }

    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = "Score: " + score.ToString();  // Mettre � jour l'affichage du score
        Debug.Log("Score ajout� : " + points);
    }
}
