using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;

public class GetTextureButton : MonoBehaviour
{

    [SerializeField, Header("反映させたいRawImage")]
    private RawImage _targetImage;

    [SerializeField, Header("取得したい画像URI")]
    private string _uri;

    void Awake()
    {
        var token = this.GetCancellationTokenOnDestroy();
        GetComponent<Button>().onClick.AddListener(() => GetTexture(token).Forget());
    }

    private async UniTaskVoid GetTexture(CancellationToken cancellationToken)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(_uri);
        await www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
            Debug.Log(www.error);
        else
            _targetImage.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
    }
}
