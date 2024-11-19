using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //[SerializeField] float speed;
    [SerializeField] float angle;

    Vector3 vec = new Vector3 (0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        vec.x = Input.GetAxis("Horizontal");
        vec.z = Input.GetAxis("Vertical");        

        transform.position += vec * Time.deltaTime;

        transform.Rotate(Vector3.up * angle * Time.deltaTime, Space.Self);

        //transform.rotation = Quaternion.Euler(vec);
    }

}

