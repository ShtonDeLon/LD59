using System.Collections;
using UnityEngine;

public class Revolver : MonoBehaviour
{
    [SerializeField]
    private FPSController controller;
    [SerializeField]
    private MessageManager mm;

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform spawnPoint, directionPoint;
    [SerializeField]
    private Animator animator;

    private bool reload = false;

    private bool firstShot = false;


    private void Awake()
    {
        firstShot = PlayerPrefs.GetInt("FirstShot") == 0 ? false : true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !Timeline.isPaused && !reload && !controller.death)
        {
            if (!firstShot)
            {
                mm.SendMessage_aboutBullets();
                PlayerPrefs.SetInt("FirstShot", 1);
                firstShot = true;
            }

            Vector3 aimDirection = (directionPoint.position - spawnPoint.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.LookRotation(aimDirection, Vector3.up));

            animator.Play("RevolverShot");
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        reload = true;
        yield return new WaitForSeconds(0.5f);
        animator.Play("Reload");
        yield return new WaitForSeconds(1f);
        reload = false;
    }
}
