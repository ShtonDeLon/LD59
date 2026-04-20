using DG.Tweening;
using System.Collections;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField]
    private Timeline timeline;
    [SerializeField]
    private MessageManager messageManager;
    public CharacterController controller;

    public float speed = 2f;
    public float gravity = -9.81f;
    public float jumpHeight =3f;

    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    public bool death;

    public static FPSController player;

    private void Awake()
    {
        if (player == null)
            player = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -0.05f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (death)
            move = Vector3.zero;


        if (Input.GetButtonDown("Jump") && isGrounded && !death)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        if (!Timeline.isPaused)
        {
            controller.Move(move * speed * Time.deltaTime);
            controller.Move(velocity);
        }
        else
        {
            controller.Move(Vector3.zero);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Explosion>())
            Kill();
    }

    IEnumerator Death()
    {
        death = true;
        timeline.OpenDeathPanel();
        yield return transform.DOLocalRotate(new Vector3(0, 0, 80f), 2f);
        messageManager.SendMessage_aboutDeath();
    }

    public void Kill()
    {
        if (!death)
            StartCoroutine(Death());
    }
}
