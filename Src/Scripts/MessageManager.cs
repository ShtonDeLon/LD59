using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private List<AudioClip> clips;

    [SerializeField]
    private GameObject msg;

    [SerializeField, TextArea]
    private List<string> messages;

    [SerializeField, TextArea]
    private string aboutBullets, aboutDeath, aboutWin, aboutData, aboutLose, aboutWinRound;

    private int msgCounter = 0;

    public void SendMessage()
    {
        if (msgCounter < messages.Count)
        {
            TextWriter txtW = Instantiate(msg, transform).GetComponent<TextWriter>();
            txtW.Init(messages[msgCounter]);
            msgCounter++;
            PlayAudio();
        }
    }

    public void SendMessage_aboutBullets()
    {
        TextWriter txtW = Instantiate(msg, transform).GetComponent<TextWriter>();
        txtW.Init(aboutBullets);
        PlayAudio();
    }

    public void SendMessage_aboutDeath()
    {
        TextWriter txtW = Instantiate(msg, transform).GetComponent<TextWriter>();
        txtW.Init(aboutDeath);
        PlayAudio();
    }

    public void SendMessage_aboutWin()
    {
        TextWriter txtW = Instantiate(msg, transform).GetComponent<TextWriter>();
        txtW.Init(aboutWin);
        PlayAudio();
    }

    public void SendMessage_aboutData()
    {
        TextWriter txtW = Instantiate(msg, transform).GetComponent<TextWriter>();
        txtW.Init(aboutData);
        PlayAudio();
    }

    public void SendMessage_aboutLose()
    {
        TextWriter txtW = Instantiate(msg, transform).GetComponent<TextWriter>();
        txtW.Init(aboutLose);
        PlayAudio();
    }

    public void SendMessage_aboutWinRound()
    {
        TextWriter txtW = Instantiate(msg, transform).GetComponent<TextWriter>();
        txtW.Init(aboutWinRound);
        PlayAudio();
    }

    public void PlayAudio()
    {
        source.volume = PlayerPrefs.GetFloat("Effects");
        source.clip = clips[Random.Range(0, clips.Count)];
        source.pitch = Random.Range(0.6f, 1.2f);
        source.Play();
    }
}
