using System;
using System.Linq;
using UniRx.Async;
using UnityEngine;
using TMPro;

class Countdown : MonoBehaviour
{
    [SerializeField]
    GameObject ui;

    [SerializeField]
    TMP_Text countdownText;

    void OnEnable()
    {
        ui.SetActive(false);
    }

    public async UniTask Run()
    {
        ui.SetActive(true);

        var counts = Enumerable.Range(1, 3).Reverse();
        foreach (var count in counts)
        {
            countdownText.text = count.ToString();
            await UniTask.Delay(TimeSpan.FromSeconds(1));
        }

        ui.SetActive(false);
    }
}
