using System;
using Unity.Mathematics;
using static Unity.Mathematics.math;
using UnityEngine;

public class MeteorManager : MonoBehaviour
{
    [SerializeField]
    GameObject meteorPrefab;

    [SerializeField]
    Lane lane;

    [SerializeField]
    float spawnInterval;
    float _spawnInterval;
    [SerializeField]
    float spawnIntervalReductionRate;
    [SerializeField]
    float meteorSpeed;
    float _meteorSpeed;
    [SerializeField]
    float meteorSpeedIncreaseRate;
    [SerializeField]
    float2 meteorRotationSpeedRange;

    float lastSpawnedAt;

    Unity.Mathematics.Random random;

    void OnEnable()
    {
        var meteors = GetComponentsInChildren<Meteor>();
        foreach (var meteor in meteors)
        {
            Destroy(meteor.gameObject);
        }
        _meteorSpeed = meteorSpeed;
        _spawnInterval = spawnInterval;
        lastSpawnedAt = Time.time;
        random.InitState((uint)DateTime.Now.ToBinary());
        SpawnMeteor();
    }

    void Update()
    {
        SpawnMeteor();
    }

    void SpawnMeteor()
    {
        var elapsedFromLastSpawnedAt = Time.time - lastSpawnedAt;
        if (elapsedFromLastSpawnedAt < _spawnInterval) return;

        var transform = this.transform;
        var meteorGameObject = Instantiate(meteorPrefab, transform);

        var meteor = meteorGameObject.GetComponent<Meteor>();
        meteor.speed = _meteorSpeed;
        meteor.rotationAxis = normalize(random.NextFloat3());
        meteor.rotationSpeed = meteorRotationSpeedRange.x + (meteorRotationSpeedRange.x - meteorRotationSpeedRange.y) * random.NextFloat();

        var meteorPosition = meteorGameObject.transform.position;
        var lanePosition = (Lane.Position)(Mathf.Abs(random.NextInt()) % 3);
        meteorPosition.x = lane.GetTargetXFromPosition(lanePosition);
        meteorGameObject.transform.position = meteorPosition;

        _spawnInterval -= _spawnInterval * spawnIntervalReductionRate * elapsedFromLastSpawnedAt;
        _meteorSpeed += _meteorSpeed * meteorSpeedIncreaseRate * elapsedFromLastSpawnedAt;

        lastSpawnedAt = Time.time;
    }
}
