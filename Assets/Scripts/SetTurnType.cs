using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetTurnType : MonoBehaviour
{
    public ActionBasedContinuousTurnProvider continuousTurn;
    public ActionBasedSnapTurnProvider snapTurn;

    public void setTypeFromIndex(int index)
    {
        if (index == 0)
        {
            continuousTurn.enabled = true;
            snapTurn.enabled = false;
        }
        else if (index == 1)
        {
            continuousTurn.enabled = false;
            snapTurn.enabled = true;
        }
    }
}
