using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetZone : MonoBehaviour
{
    public int points = 10;  // Points attribués pour cette zone
    public MiniGameController gameController; // Référence au contrôleur du mini-jeu

    private void OnCollisionEnter(Collision collision)
    {
        // Vérifier si la flèche touche la cible
        if (collision.gameObject.CompareTag("Arrow"))
        {
            Debug.Log("Flèche touchée dans la zone : " + gameObject.name);
            gameController.AddPoints(points);  // Ajouter des points au score du joueur
        }
    }
}
