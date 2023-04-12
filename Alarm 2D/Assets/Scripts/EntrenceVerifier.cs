using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EntrenceVerifier : MonoBehaviour
{
    private const string EnemyLayer = "Enemy";

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private UnityEvent _entered;
    [SerializeField] private UnityEvent _left;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(Movement))
            _entered.Invoke();

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(EnemyLayer))
        {
            _left.Invoke();
        }
    }
}