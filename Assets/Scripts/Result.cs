using System;
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


    [SerializeField]
    LinkButton tweetButton;
    [SerializeField]
    string tweetTextFormat;

    void OnEnable()
    {
        ui.SetActive(false);
    }

    public async UniTask Run()
    {
        ui.SetActive(true);

        var traveledLengthString = masawada.traveledLength.ToString(traveledLengthTextFormat);
        traveledLengthText.text = traveledLengthString;
        tweetButton.url = String.Format(tweetTextFormat, traveledLengthString);

        await returnToTitleButton.OnClickAsync();

        ui.SetActive(false);
    }
}
