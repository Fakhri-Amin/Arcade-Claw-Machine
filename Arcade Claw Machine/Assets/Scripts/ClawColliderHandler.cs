using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawColliderHandler : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;
    private Transform grabbedTransform;

    private void Start()
    {
        PlayerController.Instance.OnGrabbedObjectReleased += PlayerController_OnGrabbedObjectReleased;
    }

    private void PlayerController_OnGrabbedObjectReleased()
    {
        if (!grabbedTransform) return;
        // grabbedTransform.gameObject.AddComponent<Rigidbody>();
        grabbedTransform.GetComponent<Rigidbody>().isKinematic = false;
        grabbedTransform = null;
    }

    public IEnumerator SetGrabbedGameObject(Transform grabbedTransform)
    {
        grabbedTransform.GetComponent<Rigidbody>().isKinematic = true;

        yield return new WaitForSeconds(.4f);

        float elapsedTime = 0f;
        float grabbingSpeed = 0.15f;

        Vector3 currentGrabbedObjectPosition = grabbedTransform.position;
        Vector3 targetPosition = grabPoint.position;

        while (elapsedTime < grabbingSpeed)
        {
            grabbedTransform.position = Vector3.Lerp(currentGrabbedObjectPosition, targetPosition, elapsedTime / grabbingSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        grabbedTransform.position = targetPosition;
        grabbedTransform.parent = transform;
        this.grabbedTransform = grabbedTransform;

    }
}
