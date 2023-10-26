using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeScoreColliderHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<DollController>(out DollController dollController))
        {
            Destroy(dollController.gameObject);
        }
    }
}
