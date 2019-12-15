using UniRx.Async;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField]
    Masawada masawada;
    [SerializeField]
    MeteorManager meteorManager;

    [SerializeField]
    GameObject ui;

    [SerializeField]
    Button startButton;

    public async UniTask Run()
    {
        ui.SetActive(true);

        masawada.gameObject.SetActive(false);
        masawada.gameObject.SetActive(true);
        meteorManager.gameObject.SetActive(false);

        await startButton.OnClickAsync();

        ui.SetActive(false);
    }
}
