using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleClawColliderHandler : MonoBehaviour
{
    [SerializeField] private ClawColliderHandler clawColliderHandler;

    private Transform grabbedTransform;

    private void OnTriggerStay(Collider other)
    {
        if (!PlayerController.Instance.IsCloseState())
        {
            grabbedTransform = null;
            return;
        };

        if (grabbedTransform) return;
        grabbedTransform = other.gameObject.transform;
        StartCoroutine(clawColliderHandler.SetGrabbedGameObject(grabbedTransform));
    }

}
