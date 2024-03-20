using System;
using UnityEngine;

public class DoorOpenTrigger : MonoBehaviour
{
    [SerializeField] private Door _door;

    private void OnEnable()
    {
        if (_door == null)
        {
            enabled = false;
            throw new ArgumentNullException(nameof(_door));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DoorOpener _))
            _door.Open();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out DoorOpener _))
            _door.Close();
    }
}
