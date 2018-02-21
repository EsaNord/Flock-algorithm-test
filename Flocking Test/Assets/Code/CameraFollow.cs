using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float m_fDistance = 5f;
    public float m_fSpeed = 5f;
    public float m_fRotationSpeed = 10f;
    public Transform m_tTarget;
    public AgentController m_acController;
    private Vector3 m_vCenterPos;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(m_vCenterPos, new Vector3(1,1,1));
    }

    private void Awake()
    {        
        transform.position = m_tTarget.position;
        transform.rotation = Quaternion.identity;
        transform.position -= transform.forward * m_fDistance;  
    }

    private void Update()
    {
        m_vCenterPos = Vector3.zero;
        List<GameObject> birds = m_acController.birds;
        foreach (GameObject obj in birds)
        {
            m_vCenterPos += obj.transform.position;
        }
        m_vCenterPos /= birds.Count;

        Vector3 newPos = m_vCenterPos - (transform.forward * m_fDistance);
        transform.position = Vector3.Slerp(transform.position, newPos, m_fSpeed * Time.deltaTime);
        transform.LookAt(m_vCenterPos);
    }
}
