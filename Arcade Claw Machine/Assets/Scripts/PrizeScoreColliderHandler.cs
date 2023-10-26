using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeScoreColliderHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<DollController>(out DollController dollController))
        {
            ScoreManagerUI.Instance.UpdateUI(dollController.GetDollScore());
            Destroy(dollController.gameObject);
        }
    }
}
