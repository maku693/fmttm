using System;
using UniRx.Async;
using UnityEngine;
using TMPro;

public class Playing : MonoBehaviour
{
    [SerializeField]
    Masawada masawada;
    [SerializeField]
    MeteorManager meteorManager;

    [SerializeField]
    GameObject ui;
    [SerializeField]
    TMP_Text traveledLengthText;

    void OnEnable()
    {
        ui.SetActive(false);
    }

    void Update()
    {
        traveledLengthText.text = masawada.traveledLength.ToString();
    }

    public async UniTask Run()
    {
        ui.SetActive(true);

        meteorManager.gameObject.SetActive(true);

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

        ui.SetActive(false);
    }
}
