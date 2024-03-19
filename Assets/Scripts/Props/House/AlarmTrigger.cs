using System;
using UnityEngine;

public class AlarmTrigger : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;

    private void OnEnable()
    {
        if(_alarm == null)
        {
            enabled = false;
            throw new ArgumentNullException(nameof(_alarm));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Robber>())
            _alarm.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<Robber>())
            _alarm.Stop();
    }
}
