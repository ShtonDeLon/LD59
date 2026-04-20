using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(Kill());
    }

    IEnumerator Kill()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(GetComponent<SphereCollider>());
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
