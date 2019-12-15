using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Result : MonoBehaviour
{
    [SerializeField]
    Masawada masawada;

    [SerializeField]
    GameObject ui;

    [SerializeField]
    Button returnToTitleButton;
    [SerializeField]
    TMP_Text traveledLengthText;
    [SerializeField]
    string traveledLengthTextFormat;

    void OnEnable()
    {
        ui.SetActive(false);
    }

    public async UniTask Run()
    {
        ui.SetActive(true);

        traveledLengthText.text = masawada.traveledLength.ToString(traveledLengthTextFormat);
        await returnToTitleButton.OnClickAsync();

        ui.SetActive(false);
    }
}
