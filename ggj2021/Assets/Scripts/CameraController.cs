using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 offset;
    public Transform[] pointOfInterests;
    public Transform target;
    public float speed = 1;
    public bool blocked = false;
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (blocked) return;
        if (GetCloserToTarget() == false) {
            transform.position = Vector3.Lerp(
                transform.position,
                Vector3.right * (target.position.x + offset.x) + Vector3.up * 4 + Vector3.forward * (target.position.z + offset.z),
                Time.deltaTime * speed);
        }
    }

    bool GetCloserToTarget() {
        RaycastHit hit;
        float distance = Vector3.Magnitude(transform.position - target.transform.position);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance - 2)) {
            transform.position = Vector3.Lerp(transform.position, target.position + Vector3.up * 2, Time.deltaTime * speed * 0.5f);
            return true;
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 2)) {
            transform.position = Vector3.Lerp(transform.position, target.position + Vector3.up * 2, Time.deltaTime * speed * 0.5f);
            return true;
        }
        foreach (Transform point in pointOfInterests) {
            if (point.gameObject.activeSelf && Vector3.Magnitude(point.position - target.transform.position) < 2) {
                transform.position = Vector3.Lerp(transform.position, point.position + Vector3.back * 5, Time.deltaTime * speed * 0.5f);
                return true;
            }
        }
        return false;
    }
}
