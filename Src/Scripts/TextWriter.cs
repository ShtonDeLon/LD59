using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
    private string msg;

    [SerializeField]
    private TextMeshProUGUI outputText;

    private string visibleText = "";
    private int characterIndex;
    [SerializeField]
    private float timePerSymbol = 0.03f;

    [SerializeField]
    private Image container, triangle;

    public void Init(string text)
    {
        msg = text;
        StartCoroutine(Print());
    }

    IEnumerator Print()
    {
        while (characterIndex < msg.Length)
        {
            characterIndex++;
            visibleText = msg.Substring(0, characterIndex);
            outputText.text = visibleText;
            yield return new WaitForSeconds(timePerSymbol);
        }
        yield return new WaitForSeconds(5f);

        float p = 1;
        while (p > 0)
        {
            triangle.color = new Color(1, 1, 1, p);
            container.color = new Color(1, 1, 1, p);
            outputText.color = new Color(0, 0, 0, p);
            p -= Time.deltaTime * 2f;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
