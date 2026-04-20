using System.Collections;
using UnityEngine;

public class Alien : MonoBehaviour
{
    [SerializeField]
    private GameObject head2, head1;

    [SerializeField]
    private GameObject signal1, signal2, signal3;

    [HideInInspector]
    public bool isDownload = false;

    private float scale = 0;

    private Material signalMaterial;
    private Animator animator;

    [HideInInspector]
    public bool die = false;

    public float speed = 0.75f;

    private Coroutine life, kill;


    private void Awake()
    {
        kill = null;
        animator = GetComponent<Animator>();
        signalMaterial = new Material(signal1.GetComponent<MeshRenderer>().material);
        signal1.GetComponent<MeshRenderer>().material = signalMaterial;
        signal2.GetComponent<MeshRenderer>().material = signalMaterial;
        signal3.GetComponent<MeshRenderer>().material = new Material(signal1.GetComponent<MeshRenderer>().material);
        life = StartCoroutine(Life());
    }

    private void Update()
    {
        if (!die)
        {
            if (isDownload)
            {
                signal1.transform.localScale = Vector3.one * scale;
                signal2.transform.localScale = Vector3.one * scale;
                scale += Time.deltaTime / 50;
                scale = scale % 0.02f;
                signalMaterial.SetFloat("_Opacity", Mathf.Clamp(1 - scale * 30, 0, 1));
            }
            else
            {
                signalMaterial.SetFloat("_Opacity", 0);
                signal1.transform.localScale = Vector3.zero;
                signal2.transform.localScale = Vector3.zero;
            }
        }
        else
        {
            StopCoroutine(life);
            signalMaterial = new Material(signal3.GetComponent<MeshRenderer>().material);
            signalMaterial.SetColor("_Color", new Color(1, 70f/255, 70f / 255));
            signal3.GetComponent<MeshRenderer>().material = signalMaterial;
            if (kill == null)
                kill = StartCoroutine(AngryKill());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Explosion>())
        {
            if (!die)
            {
                Kill();
            }
            else
            {
                StartCoroutine(Death());
            }
        }
    }


    public void Kill()
    {
        die = true;
        head1.SetActive(false);
        head2.SetActive(true);
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }

    IEnumerator Life()
    {
        while (true)
        {
            Vector3 target = new Vector3(Random.Range(-30f, 30f), 0, Random.Range(-30f, 30f));
            while (Vector3.Distance(transform.position, target) < 20f)
                target = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f) * 30f);

            while (Vector3.Distance(transform.position, target) > 2f)
            {
                animator.Play("AlienWalk");
                yield return new WaitForEndOfFrame();
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime / 5f);

                Vector3 direction = target - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = targetRotation;
            }

            animator.Play("AlienIdle");
            yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator AngryKill()
    {
        while (true)
        {
            Vector3 target = FPSController.player.transformHandle.position;

            animator.Play("AlienWalk");
            yield return new WaitForEndOfFrame();
            target.y = 0;
            transform.position = Vector3.MoveTowards(transform.position, target, 2 * speed * Time.deltaTime / 5f);

            Vector3 direction = target - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;

            if (Vector3.Distance(transform.position, target) < 5f)
            {
                animator.Play("AlienIdle");
                yield return new WaitForSeconds(2f);
                if (Vector3.Distance(transform.position, FPSController.player.transformHandle.position) < 10f)
                {
                    FPSController.player.Kill();
                }
            }
        }
    }
}
