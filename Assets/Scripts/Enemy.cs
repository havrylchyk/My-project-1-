using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private float restartDelay = 2f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out var player))
        {
            player.TakeDamage(damageAmount);

            PlayerController.OnPlayerDied += player.SavePlayerData;

            StartCoroutine(RestartLevel());
        }
    }


    private IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(restartDelay);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
    }
}
