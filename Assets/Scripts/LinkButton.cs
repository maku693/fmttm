using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;

class LinkButton : MonoBehaviour
{
    [SerializeField]
    Button button;
    public string url;

    async void Start()
    {
        await button.OnClickAsync();
        Application.OpenURL(url);
    }
}