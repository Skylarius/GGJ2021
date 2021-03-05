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
    void FixedUpdate()
    {
        if (blocked) return;
        if (GetCloserToTarget() == true) {
            return;
        }
        transform.position = Vector3.Lerp(
            transform.position,
            Vector3.right * (target.position.x + offset.x) + Vector3.up * 4 + Vector3.forward * (target.position.z + offset.z),
            Time.deltaTime * speed);
    }

    bool GetCloserToTarget() {
        RaycastHit hit;
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance > Vector3.Distance(offset, Vector3.zero) + 1) {
            return false;
        }
        Ray ray1 = new Ray(transform.position + Vector3.back * 0.5f + Vector3.left * 0.2f, transform.TransformDirection(Vector3.forward));
        if (Physics.Raycast(ray1.origin, ray1.direction, out hit, distance)) {
            print(hit.transform.gameObject.name);
            Debug.DrawRay(ray1.origin, ray1.direction * (distance - 1f), Color.green);
            transform.position = Vector3.Lerp(
                transform.position,
                Vector3.right * target.position.x + Vector3.up * 4 + Vector3.forward * target.position.z,
                Time.deltaTime * speed);
            return true;
        }
        Ray ray2 = new Ray(transform.position + Vector3.forward * 1f + Vector3.right * 0.2f, transform.TransformDirection(Vector3.back));
        if (Physics.Raycast(ray2.origin, ray2.direction, out hit, 2f)) {
            Debug.DrawRay(ray2.origin, ray2.direction * 2f, Color.red);
            transform.position = Vector3.Lerp(
                transform.position,
                Vector3.right * target.position.x + Vector3.up * 4 + Vector3.forward * transform.position.z,
                Time.deltaTime * speed * 0.5f);
            return true;
        }
        foreach (Transform point in pointOfInterests) {
            if (point.gameObject.activeSelf && Vector3.Magnitude(point.position - target.transform.position) < 2) {
                Debug.DrawRay(transform.position, point.position + Vector3.back * 5 - transform.position, Color.yellow);
                transform.position = Vector3.Lerp(transform.position, point.position + Vector3.back * 5, Time.deltaTime * speed * 0.5f);
                return true;
            }
        }
        return false;
    }
}
