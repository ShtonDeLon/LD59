using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private FPSController controller;
    [SerializeField]
    private Camera playerCam;
    [SerializeField]
    private Transform playerBody, sphere, emptyAim;
    [SerializeField]
    private LayerMask aimColliderLayerMask = new LayerMask();

    public static float mouseSensitivity = 100f;

    float xRotation = 0f;

    private Alien current;
    [SerializeField]
    private GameObject signal;
    private float sigSize = 0;

    private Material signalMaterial;

    [SerializeField]
    private ProgressBar pb;

    private void Start()
    {
        PlayerPrefs.SetFloat("Effects", 0.6f);
        signalMaterial = new Material(signal.GetComponent<MeshRenderer>().material);
        signal.GetComponent<MeshRenderer>().material = signalMaterial;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (!controller.death)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 60f);

            transform.Rotate(Vector3.up * mouseX);
            playerBody.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = playerCam.ScreenPointToRay(screenCenterPoint);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
            {
                sphere.position = raycastHit.point;
                if (raycastHit.transform.TryGetComponent<Alien>(out Alien alien))
                {
                    current = alien;
                    if (!current.die && Vector3.Distance(current.transform.position, transform.position) < 20f)
                    {
                        current.isDownload = true;

                        if (!source.isPlaying)
                        {
                            source.volume = PlayerPrefs.GetFloat("Effects") * 0.3f;
                            source.Play();
                        }
                        sigSize += Time.deltaTime;
                        pb.load = true;
                        sigSize %= 1f;
                        signal.transform.localScale = Vector3.one * sigSize * 0.003f;
                        signalMaterial.SetFloat("_Opacity", Mathf.Clamp(1 - sigSize, 0, 1));
                    }
                    else
                    {
                        if (current != null)
                        {
                            source.Stop();
                            pb.load = false;
                            current.isDownload = false;
                            current = null;
                            signal.transform.localScale = Vector3.zero;
                            signalMaterial.SetFloat("_Opacity", 0);
                        }
                    }
                }
                else
                {
                    if (current != null)
                    {
                        source.Stop();
                        pb.load = false;
                        current.isDownload = false;
                        current = null;
                        signal.transform.localScale = Vector3.zero;
                        signalMaterial.SetFloat("_Opacity", 0);
                    }
                }
            }
            else
            {
                sphere.position = emptyAim.position;
                if (current != null)
                {
                    source.Stop();
                    pb.load = false;
                    current.isDownload = false;
                    current = null;
                    signal.transform.localScale = Vector3.zero;
                    signalMaterial.SetFloat("_Opacity", 0);
                }
            }
        }
    }

    public void KillAlien()
    {
        if (current != null)
        {
            pb.load = false;
            current.Kill();
            current = null;
        }
    }
}
