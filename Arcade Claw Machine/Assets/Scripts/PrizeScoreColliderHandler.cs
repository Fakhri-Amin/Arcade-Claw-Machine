using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeScoreColliderHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem explodeVFX;

    private void Start()
    {
        explodeVFX.Stop();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<DollController>(out DollController dollController))
        {
            explodeVFX.Play();
            SoundManager.Instance.PlayExplodeSound();
            ScoreManagerUI.Instance.UpdateUI(dollController.GetDollScore());
            Destroy(dollController.gameObject);
        }
    }
}
