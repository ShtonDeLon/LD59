using UnityEngine;
using UnityEngine.EventSystems;

public class Quit : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Application.Quit();
    }
}
