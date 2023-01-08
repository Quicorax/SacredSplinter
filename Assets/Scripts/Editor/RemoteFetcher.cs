using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class RemoteFetcher
{
    private const string _questsModelURL = "https://script.google.com/macros/s/AKfycbxGbmnS-GeqQjTv_ZaGBcVgm1bImkV_Urs8DVQh1NCc0OCJ13dvTEoxXrF2qhwUJL6f/exec";
    private const string _shopModelURL = "https://script.google.com/macros/s/AKfycbw32OqovbZFHjLL8orXVDUm1-jV-jMYkG6Gk8Ldr1CaIRzrls7qo6ykd3ERSBfU8xDG/exec";
    private const string _locationsModelURL = "https://script.google.com/macros/s/AKfycbzXcPAfu6Dy9Rn5FwpXXJxHqqZ86q36vxcTelhm7RlODXl-vPZ4_xNqtUi6ebn5kUGPFQ/exec";
    private const string _heroesModelURL = "https://script.google.com/macros/s/AKfycbzTjE_0rfjTXXisaGzS1reMcXHGTZI-01GOztCzj5FzFR2NZUZ-XGt6Twj40O867Maq/exec";
    private const string _encyclopediaModelURL = "https://script.google.com/macros/s/AKfycbxuuy6tkUqver0jy5CFK-DMc1kV2BZuKvLm9czItyHw3G_TipeHa6taFS5nOMrjA8bg/exec";
    private const string _eventsModelURL = "https://script.google.com/macros/s/AKfycbwq7UJVLNq-1NUF2uvuq18wAf0G-0a6JblLlBYJvW9B9GfGPeOfpXolgkLLv0OrT6RHBg/exec";

    private const string _editorWindow = "Quicorax/RemoteData/Update ";

    [MenuItem(_editorWindow + "Quests Model")]
    public static void UpdateQuests() => UpdateRemoteResource("QuestsModel", _questsModelURL);

    [MenuItem(_editorWindow + "Shop Model")]
    public static void UpdateShop() => UpdateRemoteResource("ShopModel", _shopModelURL);
    
    [MenuItem(_editorWindow + "Locations Model")]
    public static void UpdateLocations() => UpdateRemoteResource("LocationsModel", _locationsModelURL);

    [MenuItem(_editorWindow + "Heroes Model")]
    public static void UpdateHeroes() => UpdateRemoteResource("HeroesModel", _heroesModelURL);
    
    [MenuItem(_editorWindow + "Encyclopedia Model")]
    public static void UpdateEncyclopedia() => UpdateRemoteResource("EncyclopediaModel", _encyclopediaModelURL);
    
    [MenuItem(_editorWindow + "Events Model")]
    public static void UpdateEvents() => UpdateRemoteResource("EventsModel", _eventsModelURL);

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
