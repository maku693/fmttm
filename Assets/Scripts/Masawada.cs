using System;
using UniRx.Async;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;

class Masawada : MonoBehaviour
{
    enum MovingDirection
    {
        Left = -1,
        Right = 1
    }

    [SerializeField]
    Lane lane;

    [SerializeField]
    GameObject body;
    [SerializeField]
    GameObject thrusterParticle;
    [SerializeField]
    GameObject explodeParticle;
    [SerializeField]
    float speed;
    [SerializeField]
    float laneChangeSpeed;

    [SerializeField]
    float explosionForce;
    [SerializeField]
    float explosionRadius;

    public float traveledLength => _traveledLength;
    float _traveledLength;
    MovingDirection? movingDirection;
    Lane.Position targetLanePosition;

    UniTaskCompletionSource explodeCompletionSource;
    public UniTask onExplode => explodeCompletionSource.Task;

    Unity.Mathematics.Random random = new Unity.Mathematics.Random();

    void OnEnable()
    {
        _traveledLength = 0.0F;
        movingDirection = null;
        targetLanePosition = Lane.Position.Center;

        explodeCompletionSource = new UniTaskCompletionSource();

        random.InitState((uint)DateTime.Now.ToBinary());

        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;

        var position = transform.position;
        var targetX = lane.GetTargetXFromPosition(targetLanePosition);
        transform.position = new float3(targetX, 0, 0);
        transform.rotation = quaternion.identity;

        body.SetActive(true);
        thrusterParticle.SetActive(true);
    }

    void Update()
    {
        if (onExplode.IsCompleted) return;

        // Pseudo-forward movement
        _traveledLength += speed * Time.deltaTime;

        // Lateral movement
        var targetX = lane.GetTargetXFromPosition(targetLanePosition);
        var position = transform.position;
        if (movingDirection != null)
        {
            var movement = (float)movingDirection * laneChangeSpeed * Time.deltaTime;
            if (Math.Abs(position.x - targetX) > Math.Abs(movement))
            {
                position.x += movement;
            }
            else
            {
                position.x = targetX;
                movingDirection = null;
            }
        }
        transform.position = position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (onExplode.IsCompleted) return;

        var meteor = other.GetComponentInParent<Meteor>();
        if (meteor)
        {
            Explode();
        }
    }

    void OnMove(InputValue value)
    {
        if (onExplode.IsCompleted) return;

        if (movingDirection != null) return;

        var v = value.Get<float>();
        if (v == 0) return;

        movingDirection = v > 0 ? MovingDirection.Right : MovingDirection.Left;

        Lane.Position nextLanePosition;
        switch (targetLanePosition)
        {
            case Lane.Position.Left:
                nextLanePosition = v > 0 ? Lane.Position.Center : Lane.Position.Left;
                break;
            case Lane.Position.Right:
                nextLanePosition = v > 0 ? Lane.Position.Right : Lane.Position.Center;
                break;
            default: // Lane.Position.Center
                nextLanePosition = v > 0 ? Lane.Position.Right : Lane.Position.Left;
                break;
        }
        targetLanePosition = nextLanePosition;
    }

    void Explode()
    {
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.AddExplosionForce(explosionForce, random.NextFloat3(), explosionRadius);

        explodeCompletionSource.TrySetResult();
    }
}
