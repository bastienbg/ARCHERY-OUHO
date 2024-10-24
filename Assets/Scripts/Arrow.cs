using FiveRabbitsDemo;
using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public Transform tip;
    public new ParticleSystem particleSystem;
    public TrailRenderer trailRenderer;

    private Rigidbody _rigidBody;
    private bool _inAir = false;
    private Vector3 _lastPosition = Vector3.zero;

    private ParticleSystem _particleSystem;
    private TrailRenderer _trailRenderer;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _particleSystem = particleSystem;
        _trailRenderer = trailRenderer;

        PullInteraction.PullActionReleased += Release;
        Stop();
    }

    private void OnDestroy()
    {
        PullInteraction.PullActionReleased -= Release;
    }

    private void Release(float value)
    {
        Debug.Log("Arrow released with force multiplier: " + value);
        PullInteraction.PullActionReleased -= Release;

        // Set the Rigidbody to non-kinematic so it can be affected by physics
        _rigidBody.isKinematic = false;

        gameObject.transform.parent = null;
        _inAir = true;
        SetPhysics(true);

        // Apply the force for the arrow to fly
        Vector3 force = transform.forward * value * speed;
        _rigidBody.AddForce(force, ForceMode.Impulse);
        Debug.Log("Arrow force applied: " + force);

        StartCoroutine(RotateWithVelocity());

        _lastPosition = tip.position;

        _particleSystem.Play();
        _trailRenderer.emitting = true;
    }

    private IEnumerator RotateWithVelocity()
    {
        yield return new WaitForFixedUpdate();
        while (_inAir)
        {
            Quaternion newRotation = Quaternion.LookRotation(_rigidBody.velocity, transform.up);
            transform.rotation = newRotation;
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        if (_inAir)
        {
            CheckCollision();
            _lastPosition = tip.position;
        }
    }

    private void CheckCollision()
    {
        if (Physics.Linecast(_lastPosition, tip.position, out RaycastHit hitInfo))
        {
            Debug.Log("Arrow collision detected at: " + hitInfo.point);

            if (hitInfo.transform.CompareTag("Rabbit"))  
            {
                Debug.Log("Arrow hit a rabbit!");
                AnimatorParametersChange rabbitScript = hitInfo.transform.GetComponent<AnimatorParametersChange>();
                if (rabbitScript != null)
                {
                    rabbitScript.Die();  
                }
            }

            if (hitInfo.transform.gameObject.layer != 8)
            {
                if (hitInfo.transform.TryGetComponent(out Rigidbody body))
                {
                    Debug.Log("Arrow hit a rigidbody: " + hitInfo.transform.name);
                    _rigidBody.interpolation = RigidbodyInterpolation.None;
                    transform.parent = hitInfo.transform;
                    body.AddForce(_rigidBody.velocity, ForceMode.Impulse);
                }
                Stop();
            }
        }
    }

    private void Stop()
    {
        Debug.Log("Arrow stopped in air");
        _inAir = false;
        SetPhysics(false);  // Stop movement by setting physics to inactive

        _particleSystem.Stop();
        _trailRenderer.emitting = false;
    }

    private void SetPhysics(bool usePhysics)
    {
        _rigidBody.useGravity = usePhysics;
        _rigidBody.isKinematic = !usePhysics;  // Set the Rigidbody to non-kinematic if physics is used
        Debug.Log("Physics set: useGravity = " + _rigidBody.useGravity + ", isKinematic = " + _rigidBody.isKinematic);
    }
}
