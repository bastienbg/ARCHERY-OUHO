using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;  


public class MiniGameController : MonoBehaviour
{
    public float gameDuration = 30f; // Durée du mini-jeu en secondes
    public TextMeshProUGUI timerText; // Affichage du timer
    public GameObject[] arrows; // Tableau des flèches dans la scène
    public int score = 0;
    public TextMeshProUGUI scoreText;

    private float timeRemaining;
    private bool gameActive = false;

  

    void Update()
    {
        if (gameActive)
        {
            // Gérer le décompte du timer
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
        Debug.Log("Mini-jeu démarré !");
    }

    void EndGame()
    {
        gameActive = false;
        timerText.text = "Time's Up!";
        DestroyArrows(); // Supprimer les flèches à la fin du temps
        Debug.Log("Mini-jeu terminé !");
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
        scoreText.text = "Score: " + score.ToString();  // Mettre à jour l'affichage du score
        Debug.Log("Score ajouté : " + points);
    }
}
