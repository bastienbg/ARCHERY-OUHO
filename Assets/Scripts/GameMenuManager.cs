using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{
    public Transform head;
    public float distanceFromHead = 2;
    public GameObject menu;
    public InputActionProperty showButton;

    public Button noviceButton;
    public Button normalButton;
    public Button expertButton;
    public Button trainingButton;
    public Button restartButton; // Nouveau bouton pour redémarrer le jeu

    public Transform noviceSpawn; // Point de téléportation Novice
    public Transform normalSpawn; // Point de téléportation Normal
    public Transform expertSpawn; // Point de téléportation Expert
    public Transform trainingSpawn; // Point de téléportation Training

    void Start()
    {
        noviceButton.onClick.AddListener(TeleportToNovice);
        normalButton.onClick.AddListener(TeleportToNormal);
        expertButton.onClick.AddListener(TeleportToExpert);
        trainingButton.onClick.AddListener(TeleportToTraining);
        restartButton.onClick.AddListener(ReloadScene); // Lier le bouton de redémarrage
    }

    void Update()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);
            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * distanceFromHead;
        }

        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        menu.transform.forward *= -1;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TeleportToNovice() => TeleportTo(noviceSpawn);
    public void TeleportToNormal() => TeleportTo(normalSpawn);
    public void TeleportToExpert() => TeleportTo(expertSpawn);
    public void TeleportToTraining() => TeleportTo(trainingSpawn);

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
