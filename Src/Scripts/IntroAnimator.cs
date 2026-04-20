using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Splines;
using UnityEngine.SceneManagement;

public class IntroAnimator : MonoBehaviour
{
    [SerializeField]
    private GameObject title;
    [SerializeField]
    private GameObject body;
    [SerializeField]
    private ParticleSystem ps, fall;
    [SerializeField]
    private Color skyColor;
    [SerializeField]
    private SplineAnimate rocket;
    [SerializeField]
    private Material atmosphere;
    [SerializeField]
    private Transform planet, kernel;
    [SerializeField]
    private GameObject cosmic;

    [SerializeField]
    private Image splashImage;

    private Vector3 startPlanetScale;

    private void Awake()
    {
        title.SetActive(false);
        splashImage.transform.parent.gameObject.SetActive(true);
        atmosphere.SetFloat("_opacity", 1);
        startPlanetScale = planet.transform.lossyScale;
        cosmic.SetActive(false);
        StartCoroutine(StartAnimate());
    }

    IEnumerator StartAnimate()
    {
        yield return new WaitForSeconds(1f);
        yield return splashImage.DOColor(new Color(0, 0, 0, 0), 0.5f);
        //splashImage.gameObject.SetActive(false);
        rocket.Play();
        planet.GetComponent<SplineAnimate>().Play();
        yield return new WaitForSeconds(5f / 2.3f);
        cosmic.SetActive(true);
        cosmic.transform.DOScale(Vector3.one * 1.5f, 5f).SetEase(Ease.InQuart);
        cosmic.transform.DOMove(Vector3.zero, 5f).SetEase(Ease.InQuart);
        cosmic.transform.DOLocalRotate(new Vector3(-180, 0, 100), 5f);
        planet.DOLocalRotate(new Vector3(60, 120, 0), 9f);
        kernel.DOScale(Vector3.one * 0.3f, 5f).SetEase(Ease.InQuart);
        planet.DOScale(startPlanetScale * 10f, 5f).SetEase(Ease.InQuart);

        float p = 1;
        Camera c = Camera.main;
        Color delta = (skyColor - c.backgroundColor);
        bool flag = false;
        yield return new WaitForSeconds(0.5f);
        fall.Play();
        while (p > 0)
        {
            c.backgroundColor += delta * Time.deltaTime * 0.2f;
            yield return new WaitForFixedUpdate();
            p -= Time.deltaTime * 0.2f;
            atmosphere.SetFloat("_opacity", p);
            if (p < 0.2f && !flag)
            {
                flag = true;
                kernel.DOLocalRotate(new Vector3(30, 60, 0), 7f);
                ps.Play();
                title.SetActive(true);
                body.SetActive(false);
                fall.Stop();
            }
        }

        c.backgroundColor = skyColor;
        planet.GetComponent<MeshRenderer>().enabled = false;
        atmosphere.SetFloat("_opacity", 0);
    }
}
