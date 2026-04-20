using UnityEngine;

public class Plane : MonoBehaviour
{
    private void Update()
    {
        Vector3 target = FPSController.player.transform.position;
        target.y = 0;
        transform.position = target;
    }
}
