using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleClawColliderHandler : MonoBehaviour
{
    [SerializeField] private ClawColliderHandler clawColliderHandler;

    private GameObject grabbedObject;

    private void OnTriggerStay(Collider other)
    {
        if (!PlayerController.Instance.IsCloseState())
        {
            grabbedObject = null;
            return;
        };

        if (grabbedObject) return;

        grabbedObject = other.gameObject;
        grabbedObject.GetComponent<MeshCollider>().enabled = false;
        grabbedObject.GetComponent<Rigidbody>().useGravity = false;
        StartCoroutine(clawColliderHandler.SetGrabbedGameObject(grabbedObject.transform));
    }

}
