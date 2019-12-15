using System;
using System.Linq;
using UniRx.Async;
using UnityEngine;

class Countdown : MonoBehaviour
{
    public async UniTask Run()
    {
        var counts = Enumerable.Range(1, 3).Reverse();
        foreach (var count in counts)
        {
            Debug.Log(count.ToString());
            await UniTask.Delay(TimeSpan.FromSeconds(1));
        }
    }
}
