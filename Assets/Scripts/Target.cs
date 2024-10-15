using UnityEngine;

public class Target : MonoBehaviour
{
    public Vector3 spawnPosition;
    public GameObject explodeEffect;
    public GameObject target;

    private void Start()
    {
        gameObject.transform.position = spawnPosition + new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-3f, 3f), Random.Range(-2f, 2f));
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            GameObject spawnedEffect = Instantiate(explodeEffect);
            spawnedEffect.transform.position = gameObject.transform.position;
            Instantiate(target);
            Destroy(gameObject);
        }
    }
}
