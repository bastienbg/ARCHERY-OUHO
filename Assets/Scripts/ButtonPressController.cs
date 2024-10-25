using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;


public class ButtonPressController : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;

    private AudioSource sound;
    private GameObject presser;
    private bool isPressed;

    
    

    private void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(0, 0.003f, 0);
            presser = other.gameObject;
            onPress.Invoke();
            sound.Play();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser)
        {
            button.transform.localPosition = new Vector3(0, 0.01f, 0);
            onRelease.Invoke();
            isPressed = false;
        }
    }
    
}
