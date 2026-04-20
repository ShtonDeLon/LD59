using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timeline : MonoBehaviour
{
    [SerializeField]
    private ProgressBar pb;

    [SerializeField]
    private FPSController controller;

    [SerializeField]
    private Image signalBar;

    [SerializeField]
    private MessageManager messageManager;
    [SerializeField]
    private GameObject pausePanel, deathPanel;

    [SerializeField]
    private Slider music, effects, sensetivity;

    public static bool isPaused;
    private float progress = 0;

    private void Awake()
    {
        sensetivity.value = PlayerPrefs.GetFloat("Sensetivity");
        if (sensetivity.value == 0)
        {
            sensetivity.value = 0.6f;
            PlayerPrefs.SetFloat("Sensetivity", sensetivity.value);
        }

        effects.value = PlayerPrefs.GetFloat("Effects");
        if (sensetivity.value == 0)
        {
            PlayerPrefs.SetFloat("Effects", effects.value);
        }

        MouseLook.mouseSensitivity = sensetivity.value * 200f + 10f;
        isPaused = false;
        pausePanel.SetActive(false);
        deathPanel.SetActive(false);
        StartCoroutine(MainTimeline());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !controller.death)
        {
            isPaused = !isPaused;

            Time.timeScale = isPaused ? 0 : 1;

            Cursor.lockState = !isPaused ? CursorLockMode.Locked : CursorLockMode.None;

            pausePanel.SetActive(isPaused);

            MouseLook.mouseSensitivity = sensetivity.value * 200f + 10f;
            PlayerPrefs.SetFloat("Sensetivity", sensetivity.value);
            PlayerPrefs.SetFloat("Effects", effects.value);
        }
    }

    private IEnumerator MainTimeline()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(FirstPart());
        messageManager.SendMessage();
        yield return new WaitForSeconds(3f);
        messageManager.SendMessage();
        yield return new WaitForSeconds(4f);
        messageManager.SendMessage();
    }

    private IEnumerator FirstPart()
    {
        yield return new WaitForSeconds(6f);
        while (progress < 60f)
        {
            progress += Time.deltaTime;
            signalBar.fillAmount = progress / 60f;
            yield return new WaitForEndOfFrame();
        }

        if (pb.progressPoint == 6)
        {
            messageManager.SendMessage_aboutWinRound();
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("3_FinalCutScene");
        }
        else
        {
            messageManager.SendMessage_aboutLose();
            yield return new WaitForSeconds(2f);
            FPSController.player.Kill();
        }
    }

    public void OpenDeathPanel()
    {
        deathPanel.SetActive(true);
    }
}
