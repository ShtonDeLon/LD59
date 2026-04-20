using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;

    private Rigidbody rb;
    public float lifetime = 3f;
    public float speed = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (lifetime > 0)
        {
            rb.linearVelocity = transform.forward * speed;

            lifetime -= Time.deltaTime;
            speed -= Time.deltaTime * 20f;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Border"))
            Explosion();
    }

    private void Explosion()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
