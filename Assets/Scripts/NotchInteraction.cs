using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NotchInteraction : XRGrabInteractable
{
    public Transform attachPoint;          // Point d'attache de la corde sur l'arc
    public Transform bowTransform;         // Transform de l'arc
    public LineRenderer bowString;         // Line Renderer de la corde
    public GameObject arrowPrefab;         // Prefab de la flèche
    public float maxDrawDistance = 0.5f;   // Distance maximale de tirage
    public float maxForce = 1000f;         // Force maximale de tir

    private bool isDrawing = false;        // Indique si la corde est en train d'être tirée
    private GameObject currentArrow;       // Référence à la flèche actuelle
    private Rigidbody arrowRigidbody;      // Rigidbody de la flèche

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        isDrawing = true;

        // Instancier une nouvelle flèche et l'attacher au notch
        currentArrow = Instantiate(arrowPrefab, transform.position, transform.rotation, transform);
        arrowRigidbody = currentArrow.GetComponent<Rigidbody>();
        arrowRigidbody.isKinematic = true;

        // Jouer un son de tirage si nécessaire
        // GetComponent<AudioSource>().Play();
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        isDrawing = false;

        // Calculer la force en fonction de la distance de tirage
        float currentDrawDistance = Vector3.Distance(transform.position, attachPoint.position);
        float appliedForce = (currentDrawDistance / maxDrawDistance) * maxForce;

        // Limiter la force maximale
        appliedForce = Mathf.Clamp(appliedForce, 0, maxForce);

        // Détacher la flèche et appliquer la force
        arrowRigidbody.isKinematic = false;
        arrowRigidbody.transform.parent = null;
        arrowRigidbody.AddForce(bowTransform.forward * appliedForce);

        // Réinitialiser la position du notch
        transform.position = attachPoint.position;

        // Réinitialiser la corde
        bowString.SetPosition(1, bowString.transform.InverseTransformPoint(transform.position));

        // Jouer un son de relâchement si nécessaire
        // GetComponent<AudioSource>().PlayOneShot(releaseSound);
    }

    void Update()
    {
        if (isDrawing)
        {
            // Limiter la distance de tirage
            float currentDrawDistance = Vector3.Distance(transform.position, attachPoint.position);
            if (currentDrawDistance > maxDrawDistance)
            {
                Vector3 direction = (transform.position - attachPoint.position).normalized;
                transform.position = attachPoint.position + direction * maxDrawDistance;
            }

            // Mettre à jour la position du point médian de la corde
            bowString.SetPosition(1, bowString.transform.InverseTransformPoint(transform.position));
        }
    }
}
