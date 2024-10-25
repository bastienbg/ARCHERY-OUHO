using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetZone : MonoBehaviour
{
    public int points = 10;  // Points attribu�s pour cette zone
    public MiniGameController gameController; // R�f�rence au contr�leur du mini-jeu

    private void OnCollisionEnter(Collision collision)
    {
        // V�rifier si la fl�che touche la cible
        if (collision.gameObject.CompareTag("Arrow"))
        {
            Debug.Log("Fl�che touch�e dans la zone : " + gameObject.name);
            gameController.AddPoints(points);  // Ajouter des points au score du joueur
        }
    }
}
