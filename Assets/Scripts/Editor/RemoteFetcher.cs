using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class RemoteFetcher
{
    private const string _questsModelURL = "https://script.google.com/macros/s/AKfycbxGbmnS-GeqQjTv_ZaGBcVgm1bImkV_Urs8DVQh1NCc0OCJ13dvTEoxXrF2qhwUJL6f/exec";
    private const string _shopModelURL = "https://script.google.com/macros/s/AKfycbw32OqovbZFHjLL8orXVDUm1-jV-jMYkG6Gk8Ldr1CaIRzrls7qo6ykd3ERSBfU8xDG/exec";

    [MenuItem("Quicorax/RemoteData/Update Quests Model")]
    public static void UpdateQuests() => UpdateRemoteResource("QuestsModel", _questsModelURL);
    [MenuItem("Quicorax/RemoteData/Update Shop Model")]
    public static void UpdateShop() => UpdateRemoteResource("ShopModel", _shopModelURL);

    private static void UpdateRemoteResource(string resource, string url)
    {
        UnityWebRequest request = WebRequest(url);
        request.SendWebRequest().completed += asyncOp =>
        {
            if (!string.IsNullOrEmpty(request.error))
            {
                Debug.LogError(request.error);
                return;
            }

            System.IO.File.WriteAllText(Application.dataPath + "/Resources/" + resource + ".json", request.downloadHandler.text);
            Debug.Log(resource + " updated with -> " + request.downloadHandler.text);

            AssetDatabase.Refresh();
        };
    }
    public static UnityWebRequest WebRequest(string url) => new(url, "GET", new DownloadHandlerBuffer(), null);
}
