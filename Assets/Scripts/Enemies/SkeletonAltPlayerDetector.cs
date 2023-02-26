using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAltPlayerDetector : MonoBehaviour
{
    [field: SerializeField]
    public bool PlayerDetected { get; private set; }
    public Vector2 DirectionToTarget => target.transform.position - detectorOrigin.position;

    [Header("OverlapBox Params")]
    [SerializeField]
    private Transform detectorOrigin;
    public Vector2 detectorSize = Vector2.one;
    public Vector2 detectorOriginOffset;

    public float detectionDelay = 0.3f;

    public LayerMask detectorLayerMask;

    [Header("Gizmo Params")]
    public Color gizmoIdleColor = Color.green;
    public Color gizmoDetectedColor = Color.red;
    public bool showGizmos = true;

    private GameObject target;

    public GameObject Target
    {
        get => target;
        private set
        {
            target = value;
            PlayerDetected = target != null;
        }
    }

    // Start is called before the first frame update
    //void Start()
    //{
    //    StartCoroutine(DetectionCoroutine());
    //}

    //IEnumerable DetectionCoroutine()
    //{
    //    yield return new WaitForSeconds(detectionDelay);
    //    PerformDetection();
    //    StartCoroutine(DetectionCoroutine());
    //}

    public void PerformDetection()
    {
        Collider2D collider = Physics2D.OverlapBox((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize, 0, detectorLayerMask);
        if (collider != null)
        {
            Target = collider.gameObject;
        }
        else
        {
            Target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
