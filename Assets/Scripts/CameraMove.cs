using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private CinemachineTrackedDolly dolly;

    [SerializeField] private float offset = 1f;


    //Vector3 vec = new (0f, 0f, -1f);
    //Vector3 vec1 = new (0f, 0f, 0f);

    private void Start()
    {
        if (virtualCamera != null) 
        {
            dolly = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        }

    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {



        if (dolly != null) 
        {
            dolly.m_PathPosition = player.transform.position.x + offset;
        }

        //GetComponent<PathP>
        //CinemachineVirtualCamera.

        //SumVec();

        //gameObject.GetComponent<Transform>().position = vec1;
        //gameObject.GetComponent<Transform>().position = player.transform.position;
        //position.x = player.transform.position.x;
    }

    //void SumVec() 
    //{ 
    //    vec1 = vec + player.transform.position; 
    //}
}
