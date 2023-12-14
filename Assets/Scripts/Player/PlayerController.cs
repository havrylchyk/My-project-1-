using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int Coints;
    [SerializeField] private AudioSource audioSource;
    public Action<int> OnCointAmountChanged;
    [SerializeField] private GameObject collectEffectPrefab;

    private GameHUD coinsCounter;

    private void Start()
    {
        OnCointAmountChanged?.Invoke(Coints);

        coinsCounter = FindObjectOfType<GameHUD>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Catch>(out var cat))
        {
            var cointValue = cat.Collect();
            Coints += cointValue;

            if (audioSource != null)
            {
                audioSource.Play();
            }

            OnCointAmountChanged?.Invoke(Coints);

            if (coinsCounter != null)
            {
                coinsCounter.UpdateCoinsCount(Coints);
            }
        }

        if (collectEffectPrefab != null)
        {
            Instantiate(collectEffectPrefab, collision.transform.position, Quaternion.identity);
        }
    }

}
