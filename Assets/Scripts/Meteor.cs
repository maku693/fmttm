using UnityEngine;
using Unity.Mathematics;

public class Meteor : MonoBehaviour
{
    public float speed;
    public float3 rotationAxis;
    public float rotationSpeed;

    void Update()
    {
        var position = transform.position;
        position.z -= speed * Time.deltaTime;
        transform.position = position;
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }
}