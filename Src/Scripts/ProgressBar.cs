using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private MouseLook ml;
    [SerializeField]
    private Image bar;

    [HideInInspector]
    public bool load;

    private float target = 6;
    private float target1 = 3;
    private float progress;
    private int level = 1;
    public int progressPoint = 0;

    private void Update()
    {
        if (load)
        {
            progress += Time.deltaTime * 0.25f / level;
            int newProgress = Mathf.FloorToInt(progress);

            if (progressPoint < newProgress)
            {
                progressPoint = newProgress;
                ml.KillAlien();
            }
            if (progressPoint == 6)
            {

            }
        }
        else
        {
            if (progress > progressPoint)
                progress -= Time.deltaTime * 0.5f;
        }

        float p = 0;
        switch (level)
        {
            case 1:
                bar.fillAmount = progress / target;
                break;
            case 2:
                bar.fillAmount = progress / target1;
                break;
            case 3:
                bar.fillAmount = progress;
                break;
        }

    }

    public void UpdateLevel(int level)
    {
        this.level = level;
        progress = 0;
    }
}
