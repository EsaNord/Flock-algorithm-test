using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    public int m_iAmount;
    public float m_fSpawnRadius;
    public float m_fVelocity;
    public float m_fRotationSpeed;
    public float m_fDistance;
    public LayerMask m_lmLayer;
    public GameObject m_goObject;
    public float m_fMoveSpeed;
    public float m_fRotateSpeed;
    public float m_fAngle;

    private float m_fBAngle;

    public List<GameObject> birds;

    private void Start()
    {
        Spawn(m_iAmount);
    }

    private void Update()
    {
        m_fBAngle += Random.Range(0, 2f) * Time.deltaTime;
        m_fDistance = Mathf.Abs(Mathf.Sin(m_fBAngle) * 3f);

        Move();       
        Debug.Log("Amount of birds: " + birds.Count);
    }

    private void Move()
    {
        transform.Rotate(transform.up * m_fRotateSpeed * Time.deltaTime);
        m_fAngle = m_fRotateSpeed * Time.deltaTime;
        float x = 10 * Mathf.Sin(m_fAngle);
        float y = 10 * Mathf.Cos(m_fAngle);
        transform.localPosition = new Vector3(x,y, transform.position.z + m_fMoveSpeed * Time.deltaTime);
    }

    private void Spawn(int amount)
    {    
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(m_goObject, transform.position +
                (Random.insideUnitSphere * m_fSpawnRadius), Quaternion.identity);
            obj.transform.rotation = Quaternion.Euler(Random.Range(-10f, 10f),
                Random.Range(-10f, 10f), Random.Range(-10f, 10f));
            obj.GetComponent<AgentBehaviour>().SetController = this;
            birds.Add(obj);
        }
    }

    public void SpawnNewBird()
    {
        GameObject obj = Instantiate(m_goObject, transform.position +
                (Random.insideUnitSphere * m_fSpawnRadius), Quaternion.identity);
        obj.transform.rotation = Quaternion.Euler(Random.Range(-10f, 10f),
            Random.Range(-10f, 10f), Random.Range(-10f, 10f));
        obj.GetComponent<AgentBehaviour>().SetController = this;
        birds.Add(obj);
    }

    public void Spawn10Birds()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(m_goObject, transform.position +
                    (Random.insideUnitSphere * m_fSpawnRadius), Quaternion.identity);
            obj.transform.rotation = Quaternion.Euler(Random.Range(-10f, 10f),
                Random.Range(-10f, 10f), Random.Range(-10f, 10f));
            obj.GetComponent<AgentBehaviour>().SetController = this;
            birds.Add(obj);
        }
    }
}
