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
        PlayerController.Instance.OnObjectFinishedGrabbed += PlayerController_OnObjectFinishedGrabbed;
    }

    private void PlayerController_OnObjectFinishedGrabbed()
    {
        grabbedTransform = null;
    }

    void FixedUpdate()
    {
        if (grabbedTransform == null) return;
        grabbedTransform.position = grabPoint.position;
    }

    public IEnumerator SetGrabbedGameObject(Transform grabbedTransform)
    {
        yield return new WaitForSeconds(.5f);

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
        this.grabbedTransform = grabbedTransform;
    }
}
