using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBehaviour : MonoBehaviour
{
    public AgentController m_acController;       

    public AgentController SetController { set { m_acController = value; } }

    private Vector3 GetSeparationVector(Transform target)
    {
        Vector3 diff = transform.position - target.position;
        float diffLen = diff.magnitude;
        float scaler = Mathf.Clamp01(1f - diffLen / m_acController.m_fDistance);
        return diff * (scaler / diffLen);
    }

    private void Update()
    {
        Transform currentTransform = transform;
        float velocity = m_acController.m_fVelocity;
        Vector3 separation = Vector3.zero;
        Vector3 alignment = m_acController.transform.forward;
        Vector3 cohesion = m_acController.transform.position;

        Collider[] colliders = Physics.OverlapSphere(
            currentTransform.position, m_acController.m_fDistance, m_acController.m_lmLayer);
        
        foreach (Collider col in colliders)
        {
            if (col.gameObject == this.gameObject) continue;
            
            separation += GetSeparationVector(col.transform);
            alignment += col.transform.forward;
            cohesion += col.transform.position;            
        }

        float average = 1f / colliders.Length;
        alignment *= average;
        cohesion *= average;
        cohesion = (cohesion - currentTransform.position).normalized;

        Vector3 newDirection = separation + alignment + cohesion;
        Quaternion newRotation = Quaternion.FromToRotation(Vector3.forward, newDirection.normalized);
        
        if (newRotation != currentTransform.rotation)
        {
            transform.rotation = Quaternion.Slerp(newRotation, currentTransform.rotation,
                Mathf.Exp(-m_acController.m_fRotationSpeed * Time.deltaTime));
        }

        transform.position = currentTransform.position + transform.forward * (velocity * Time.deltaTime);
    }
}
