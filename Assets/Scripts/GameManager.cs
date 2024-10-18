using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Dropdown difficultyDropdown; // Référence au Dropdown
    public Transform noviceSpawn; // Point de téléportation Novice
    public Transform normalSpawn; // Point de téléportation Normal
    public Transform expertSpawn; // Point de téléportation Expert
    public Transform trainingSpawn; // Point de téléportation Training

    public void TeleportPlayer()
    {
        if (difficultyDropdown == null)
        {
            Debug.LogError("Difficulty Dropdown is not assigned!");
            return;
        }

        switch (difficultyDropdown.value)
        {
            case 0: // Novice
                TeleportTo(noviceSpawn);
                break;
            case 1: // Normal
                TeleportTo(normalSpawn);
                break;
            case 2: // Expert
                TeleportTo(expertSpawn);
                break;
            case 3: // Training Mode
                TeleportTo(trainingSpawn);
                break;
            default:
                Debug.LogError("Invalid difficulty selected!");
                break;
        }
    }

    private void TeleportTo(Transform spawnPoint)
    {
        if (spawnPoint == null)
        {
            Debug.LogError("Spawn point is not assigned!");
            return;
        }

        GameObject player = GameObject.Find("XR Origin (VR)"); // Vérifie le nom de l'objet joueur
        if (player != null)
        {
            player.transform.position = spawnPoint.position;
        }
        else
        {
            Debug.LogError("Player object not found!");
        }
    }
}