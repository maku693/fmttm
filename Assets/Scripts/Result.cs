using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField]
    Masawada masawada;

    [SerializeField]
    GameObject ui;

    [SerializeField]
    Button returnToTitleButton;
    [SerializeField]
    Text traveledLengthText;

    void OnEnable()
    {
        ui.SetActive(false);
    }

    public async UniTask Run()
    {
        ui.SetActive(true);

        traveledLengthText.text = masawada.traveledLength.ToString();
        await returnToTitleButton.OnClickAsync();

        ui.SetActive(false);
    }
}
