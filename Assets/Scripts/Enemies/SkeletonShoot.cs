using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonShoot : MonoBehaviour
{
    [field: SerializeField]
    public bool PlayerDetected { get; private set; }

    [Header("OverlapBox Params")]
    [SerializeField]
    private Transform detectorOrigin;
    public Vector2 detectorSize = Vector2.one;
    public Vector2 detectorOriginOffset = Vector2.zero;

    public float detectionDelay = 1.5f;

    public LayerMask detectorLayerMask;

    [Header("Gizmo Params")]
    public Color gizmoIdleColor = Color.green;
    public Color gizmoDetectedColor = Color.red;
    public bool showGizmos = true;

    private GameObject target;

    [Header("Bullet Params")]
    public Animator anim;
    public float speed;
    //public float rotationSpeed;
    public GameObject bone;
    public Transform bonePos;
    //private float shootDelay = 0;
    //public float fireRate;
    

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
    void Start()
    {
        StartCoroutine(DetectionCoroutine());
    }

    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionDelay);
        PerformDetection();
        StartCoroutine(DetectionCoroutine());
    }

    public void PerformDetection()
    {
        Collider2D collider = Physics2D.OverlapBox((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize, 0, detectorLayerMask);
        if (collider != null)
        {
            Target = collider.gameObject;
            Shoot();
        }
        else
        {
            Target = null;
            //anim.SetBool("isThrowing", false);
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmos && detectorOrigin != null)
        {
            Gizmos.color = gizmoIdleColor;
            if (PlayerDetected)
            {
                Gizmos.color = gizmoDetectedColor;
            }
            Gizmos.DrawCube((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize);
        }
    }

    void Shoot()
    {
        Instantiate(bone, bonePos.position, Quaternion.identity);
        anim.SetTrigger("isThrowing");
        AudioManagerScript.PlaySound("throwbone");

    }
}
