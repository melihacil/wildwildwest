using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [Range(15, 60)]
    [SerializeField]
    private float viewRadius = 11f;
    [SerializeField]
    private float detectionCheckDelay = 0.1f;
    [SerializeField]
    public LayerMask playerLayerMask;
    private Transform target = null;
    public bool TargetVisible { get; private set; }
    public Transform Target
    {
        get => target;
        set
        {
            target = value;
            TargetVisible = false;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(DetectionCourutine());
    }

    // Update is called once per frame
    void Update()
    {
       if (Target != null)
        {
            //Debug.Log("Player Detected");
            //Shoot 
        }
    }
    private void DetectIfOutOfRange()
    {
        if (Target == null || Target.gameObject.activeSelf == false || Vector2.Distance(Target.transform.position, Target.transform.position) > viewRadius) 
        {
            Debug.Log("Player out of range");
            Target = null;
        }
    }
    private void CheckPlayerInRange()
    {
        Collider2D collision = Physics2D.OverlapCircle(transform.position, viewRadius, playerLayerMask);
        if (collision != null)
        {
            Target = collision.transform;
        }
    }
    private void DetectTarget() 
    { 
        if (Target == null)
            CheckPlayerInRange();
        else if (Target != null)
        {
            DetectIfOutOfRange();
        }
    }
    IEnumerator DetectionCourutine()
    {
        yield return new WaitForSeconds(detectionCheckDelay);
        DetectTarget();
        StartCoroutine(DetectionCourutine());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
