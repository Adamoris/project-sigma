using UnityEngine;
using System.Collections;

public class CameraRig : MonoBehaviour
{
    public float speed = 3f;
    [SerializeField] Transform _follow;
    [SerializeField] Transform _altFollow;
    Transform _transform;

    void Awake()
    {
        _transform = transform;
    }

    void Update()
    {
        _transform.position = Vector3.Lerp(_transform.position, _follow.position, speed * Time.deltaTime);
    }
}
