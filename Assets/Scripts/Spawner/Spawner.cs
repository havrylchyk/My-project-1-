using UnityEngine;

public class Spawner : MonoBehaviour
{
    private string path = "Apples/Apple";
    [SerializeField] private Transform[] spawnpoints;

    private bool used = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (used) return;
        if (collision.gameObject.TryGetComponent<PlayerController>(out var  character))
        {
            foreach (var p in spawnpoints)
            {
                var apples = Resources.Load<GameObject>(path);
                Instantiate(apples, p.position, Quaternion.identity);
            }
            used = true;
        }
    }
}
