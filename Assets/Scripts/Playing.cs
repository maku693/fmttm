using System;
using UniRx.Async;
using UnityEngine;

public class Playing : MonoBehaviour
{
    [SerializeField]
    Masawada masawada;

    public async UniTask Run()
    {
        masawada.Launch();

        var explode = new UniTaskCompletionSource();

        Action onExplode = null;
        onExplode = () =>
        {
            explode.TrySetResult();
            masawada.onExplode -= onExplode;
        };
        masawada.onExplode += onExplode;

        await explode.Task;
    }
}
