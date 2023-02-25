using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PatrollingEnemyPlayerDetection : MonoBehaviour
{
    public LayerMask targetLayer;

    [SerializeField]
    public UnityEvent<GameObject> OnPlayerDetected;

    [Range(0.1f, 1)]
    public float radius;

    [Header("Gizmo Parameters")]
    public Color gizmoColor = Color.green;
    public bool showGizmos = true;

    public bool PlayerDetected { get; internal set; }

    // Update is called once per frame
    void Update()
    {
        var collider = Physics2D.OverlapCircle(transform.position, radius, targetLayer);
        PlayerDetected = collider != null;
        if (PlayerDetected)
        {
            OnPlayerDetected?.Invoke(collider.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}
