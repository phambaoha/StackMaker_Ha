using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject target;
    public float speed;
    Vector3 offset;
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
       
        offset = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, speed * Time.deltaTime);
    }
}
