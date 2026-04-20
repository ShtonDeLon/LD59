using UnityEngine;

public class SpawnMachine : MonoBehaviour
{
    [SerializeField]
    private GameObject alienPrefab;

    public int count = 10;

    private void Awake()
    {
        for (int i = 0; i < count; i++)
            Instantiate(alienPrefab, new Vector3(Random.Range(-30f, 30f), 0, Random.Range(-30f, 30f)), Quaternion.Euler(Vector3.up*Random.Range(-180f, 180f)));
    }
}
