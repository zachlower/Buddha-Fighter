using UnityEngine;
using System.Collections;

public class SmoothCamera2D : MonoBehaviour
{

    public float clampXMin, clampXMax, clampYMin, clampYMax;
    public float interpVelocity;
    public float minDistance;
    public float followDistance;
    public GameObject target;
    public Vector3 offset;
    Vector3 targetPos;
    // Use this for initialization
    void Start()
    {
        targetPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;

            Vector3 targetDirection = (target.transform.position - posNoZ);

            interpVelocity = targetDirection.magnitude * 5f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            Vector3 pos = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);
            pos.x = Mathf.Clamp(pos.x, clampXMin, clampXMax);
            pos.y = Mathf.Clamp(pos.y, clampYMin, clampYMax);

            transform.position = pos;

        }
    }
}