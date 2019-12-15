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
    [SerializeField]
    float spawnIntervalReductionRate;
    [SerializeField]
    float meteorSpeed;
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
        if (elapsedFromLastSpawnedAt < spawnInterval) return;

        var transform = this.transform;
        var meteorGameObject = Instantiate(meteorPrefab, transform);

        var meteor = meteorGameObject.GetComponent<Meteor>();
        meteor.speed = meteorSpeed;
        meteor.rotationAxis = normalize(random.NextFloat3());
        meteor.rotationSpeed = meteorRotationSpeedRange.x + (meteorRotationSpeedRange.x - meteorRotationSpeedRange.y) * random.NextFloat();

        var meteorPosition = meteorGameObject.transform.position;
        var lanePosition = (Lane.Position)(Mathf.Abs(random.NextInt()) % 3);
        meteorPosition.x = lane.GetTargetXFromPosition(lanePosition);
        meteorGameObject.transform.position = meteorPosition;

        spawnInterval -= spawnInterval * spawnIntervalReductionRate * elapsedFromLastSpawnedAt;
        meteorSpeed += meteorSpeed * meteorSpeedIncreaseRate * elapsedFromLastSpawnedAt;

        lastSpawnedAt = Time.time;
    }
}
