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
    [SerializeField]
    string traveledLengthTextFormat;

    void OnEnable()
    {
        ui.SetActive(false);
    }

    void Update()
    {
        traveledLengthText.text = masawada.traveledLength.ToString(traveledLengthTextFormat);
    }

    public async UniTask Run()
    {
        ui.SetActive(true);

        meteorManager.gameObject.SetActive(true);

        masawada.Launch();
        await masawada.onExplode;

        ui.SetActive(false);
    }
}
