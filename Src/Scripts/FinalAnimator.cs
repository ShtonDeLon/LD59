using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;

public class FinalAnimator : MonoBehaviour
{
    [SerializeField]
    private Color spaceColor;

    [SerializeField]
    private GameObject main, second, starShip;

    [SerializeField]
    private ParticleSystem ps;
    [SerializeField]
    private GameObject stare, rocket1, rocket2;

    [SerializeField]
    private GameObject player1, player2;

    private void Awake()
    {
        rocket1.SetActive(true);
        rocket2.SetActive(false);
        main.gameObject.SetActive(true);
        second.gameObject.SetActive(false);
        starShip.gameObject.SetActive(false);
        StartCoroutine(FinalScene());
    }

    IEnumerator FinalScene()
    {
        yield return new WaitForSeconds(4f);
        ps.Stop();
        stare.SetActive(true);
        yield return new WaitForSeconds(1f);
        player1.SetActive(false);
        player2.SetActive(true);
        yield return new WaitForSeconds(1f);
        stare.SetActive(false);
        player2.SetActive(false);
        yield return new WaitForSeconds(1f);
        rocket2.SetActive(true);
        rocket1.SetActive(false);
        yield return new WaitForSeconds(2f);
        second.gameObject.SetActive(true);
        main.gameObject.SetActive(false);

        float p = 1;
        Camera c = Camera.main;
        Color delta = (spaceColor - c.backgroundColor);
        while (p > 0)
        {
            c.backgroundColor += delta * Time.deltaTime * 0.5f;
            yield return new WaitForFixedUpdate();
            p -= 0.5f * Time.deltaTime;
        }
        starShip.SetActive(true);
        second.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("1_CutScene");
    }
}
