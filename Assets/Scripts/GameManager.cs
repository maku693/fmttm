using UniRx.Async;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    [SerializeField]
    Title title;
    [SerializeField]
    Countdown countdown;
    [SerializeField]
    Playing playing;
    [SerializeField]
    Result result;

    async void OnEnable()
    {
        await GameLoop();
    }

    async UniTask GameLoop()
    {
        await title.Run();
        await countdown.Run();
        await playing.Run();
        await result.Run();
        await GameLoop();
    }
}
