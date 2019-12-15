using UniRx.Async;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField]
    Masawada masawada;

    private void OnEnable()
    {
        masawada.gameObject.SetActive(false);
        masawada.gameObject.SetActive(true);
    }

    public async UniTask Run()
    {
    }
}
