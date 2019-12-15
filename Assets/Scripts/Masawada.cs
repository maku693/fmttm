using System;
using UniRx.Async;
using UnityEngine;
using UnityEngine.InputSystem;

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

    bool isRunning;
    public float traveledLength => _traveledLength;
    float _traveledLength;
    MovingDirection? movingDirection;
    Lane.Position targetLanePosition;

    public Action onExplode;

    void OnEnable()
    {
        _traveledLength = 0.0F;
        isRunning = false;
        movingDirection = null;
        targetLanePosition = Lane.Position.Center;

        var position = transform.position;
        var targetX = lane.GetTargetXFromPosition(targetLanePosition);
        position.x = targetX;
        transform.position = position;

        body.SetActive(true);
        thrusterParticle.SetActive(false);
        explodeParticle.SetActive(false);
    }

    void Update()
    {
        // Pseudo-forward movement
        if (isRunning)
        {
            _traveledLength += speed * Time.deltaTime;
        }

        // Lateral movement
        var targetX = lane.GetTargetXFromPosition(targetLanePosition);
        var position = transform.position;
        if (movingDirection != null)
        {
            if (Mathf.Abs(position.x - targetX) > 0.1)
            {
                position.x += (float)movingDirection * laneChangeSpeed * Time.deltaTime;
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
        var meteor = other.GetComponentInParent<Meteor>();
        if (meteor)
        {
            Explode();
        }
    }

    void OnMove(InputValue value)
    {
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

    public void Launch()
    {
        isRunning = true;
        thrusterParticle.SetActive(true);
    }

    private async void Explode()
    {
        onExplode();
        isRunning = false;
        thrusterParticle.SetActive(false);
        explodeParticle.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(3));
        explodeParticle.SetActive(false);
        body.SetActive(false);
    }
}
