using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrow;
    public GameObject notch;

    private XRGrabInteractable _bow;
    private bool _arrowNotched = false;
    private GameObject _currentArrow;

    // Start is called before the first frame update
    void Start()
    {
        _bow = GetComponentInParent<XRGrabInteractable>();
        PullInteraction.PullActionReleased += NotchEmpty;
        Debug.Log("ArrowSpawner initialized");

        
    }


    void OnDestroy()
    {
        PullInteraction.PullActionReleased -= NotchEmpty;
    }

    // Update is called once per frame
    void Update()
    {
        if (_bow.isSelected && !_arrowNotched)
        {
            Debug.Log("Bow is selected, spawning arrow");
            StartCoroutine(DelayedSpawn());

            Debug.Log("StartCoroutine(DelayedSpawn()) called");
        }
        if (!_bow.isSelected && _currentArrow != null)
        {
            Debug.Log("Bow is deselected, destroying arrow");
            Destroy(_currentArrow);
            NotchEmpty(1f);
            Debug.Log("Arrow destroyed and Notch reset");
        }
    }

    public void NotchEmpty(float value)
    {
        _arrowNotched = false;
        _currentArrow = null;
    }

    IEnumerator DelayedSpawn()
    {
        Debug.Log("DelayedSpawn() called");
        _arrowNotched = true;
        yield return new WaitForSeconds(0.1f);
        if (arrow == null)
        {
            Debug.LogError("Arrow prefab is not assigned!");
            yield break;
        }
        _currentArrow = Instantiate(arrow, notch.transform);
        Debug.Log("Arrow spawned at position: " + _currentArrow.transform.position);
    }
}
