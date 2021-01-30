using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 offset;
    public Transform target;
    public float speed = 1;
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetCloserToPlayer() == false) {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * speed);
        }
    }

    bool GetCloserToPlayer() {
        RaycastHit hit;
        float distance = Vector3.Magnitude(transform.position - target.transform.position);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance - 2)) {
            transform.position = Vector3.Lerp(transform.position, target.position + Vector3.up * 2, Time.deltaTime * speed * 0.5f);
            return true;
        }
        return false;
    }
}
