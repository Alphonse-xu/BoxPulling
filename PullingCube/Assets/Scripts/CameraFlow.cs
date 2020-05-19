using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    Vector3 distance;
    public GameObject player;
    public float smoothNum = 2F;

    // Start is called before the first frame update
    void Start()
    {
        distance = player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = player.transform.position - distance;
        transform.position = Vector3.Lerp(transform.position, player.transform.position - distance, Time.deltaTime * smoothNum);

    }
}
