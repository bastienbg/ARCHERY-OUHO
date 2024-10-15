using UnityEngine;

public class BowStringController : MonoBehaviour
{
    public Transform startPoint;     // Sphère Start
    public Transform endPoint;       // Sphère End
    public Transform notch;          // Notch (encoche)
    public LineRenderer bowString;   // Line Renderer de la corde

    void Start()
    {
        // Initialiser les positions de la corde
        bowString.positionCount = 3;
        bowString.SetPosition(0, bowString.transform.InverseTransformPoint(startPoint.position));
        bowString.SetPosition(1, bowString.transform.InverseTransformPoint(notch.position));
        bowString.SetPosition(2, bowString.transform.InverseTransformPoint(endPoint.position));
    }

    void Update()
    {
        // Mettre à jour les positions des extrémités de la corde
        bowString.SetPosition(0, bowString.transform.InverseTransformPoint(startPoint.position));
        bowString.SetPosition(2, bowString.transform.InverseTransformPoint(endPoint.position));

        // Si la corde n'est pas en train d'être tirée, recentrer le point médian
        if (!notch.GetComponent<NotchInteraction>().isSelected)
        {
            bowString.SetPosition(1, bowString.transform.InverseTransformPoint(notch.position));
        }
    }
}
