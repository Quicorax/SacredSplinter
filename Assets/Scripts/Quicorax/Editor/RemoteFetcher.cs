using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace Quicorax.Editor
{
    public abstract class RemoteFetcher
    {
        private static readonly Dictionary<string, string> Urls = new()
        {
            {
                "QuestsModel",
                "https://script.google.com/macros/s/AKfycbxGbmnS-GeqQjTv_ZaGBcVgm1bImkV_Urs8DVQh1NCc0OCJ13dvTEoxXrF2qhwUJL6f/exec"
            },
            {
                "ShopModel",
                "https://script.google.com/macros/s/AKfycbw32OqovbZFHjLL8orXVDUm1-jV-jMYkG6Gk8Ldr1CaIRzrls7qo6ykd3ERSBfU8xDG/exec"
            },
            {
                "LocationsModel",
                "https://script.google.com/macros/s/AKfycbzXcPAfu6Dy9Rn5FwpXXJxHqqZ86q36vxcTelhm7RlODXl-vPZ4_xNqtUi6ebn5kUGPFQ/exec"
            },
            {
                "HeroesModel",
                "https://script.google.com/macros/s/AKfycbzTjE_0rfjTXXisaGzS1reMcXHGTZI-01GOztCzj5FzFR2NZUZ-XGt6Twj40O867Maq/exec"
            },
            {
                "HeroesDataModel",
                "https://script.google.com/macros/s/AKfycbzTT1V7ossRWyxm5_EqmXEXs6UwFUsJFf_siHH6qy12SvMrWeywTPlmiCcreIwjA2KMcg/exec"
            },
            {
                "EncyclopediaModel",
                "https://script.google.com/macros/s/AKfycbxuuy6tkUqver0jy5CFK-DMc1kV2BZuKvLm9czItyHw3G_TipeHa6taFS5nOMrjA8bg/exec"
            },
            {
                "EventsModel",
                "https://script.google.com/macros/s/AKfycbwq7UJVLNq-1NUF2uvuq18wAf0G-0a6JblLlBYJvW9B9GfGPeOfpXolgkLLv0OrT6RHBg/exec"
            },
        };

        private const string EditorWindow = "Quicorax/RemoteData/Update ";

        [MenuItem(EditorWindow + "ALL")]
        public static void UpdateAll()
        {
            foreach (var url in Urls.Keys)
            {
                UpdateRemoteResource(url);
            }
        }

        [MenuItem(EditorWindow + "QuestsModel")]
        public static void UpdateQuests() => UpdateRemoteResource("QuestsModel");

        [MenuItem(EditorWindow + "ShopModel")]
        public static void UpdateShop() => UpdateRemoteResource("ShopModel");

        [MenuItem(EditorWindow + "LocationsModel")]
        public static void UpdateLocations() => UpdateRemoteResource("LocationsModel");

        [MenuItem(EditorWindow + "HeroesModel")]
        public static void UpdateHeroes() => UpdateRemoteResource("HeroesModel");
        
        [MenuItem(EditorWindow + "HeroesDataModel")]
        public static void UpdateHeroesData() => UpdateRemoteResource("HeroesDataModel");

        [MenuItem(EditorWindow + "EncyclopediaModel")]
        public static void UpdateEncyclopedia() => UpdateRemoteResource("EncyclopediaModel");

        [MenuItem(EditorWindow + "EventsModel")]
        public static void UpdateEvents() => UpdateRemoteResource("EventsModel");

        private static void UpdateRemoteResource(string resource)
        {
            var request = WebRequest(Urls[resource]);
            request.SendWebRequest().completed += asyncOp =>
            {
                if (!string.IsNullOrEmpty(request.error))
                {
                    Debug.LogError(request.error);
                    return;
                }

                System.IO.File.WriteAllText(Application.dataPath + "/Resources/" + resource + ".json",
                    request.downloadHandler.text);
                Debug.Log(resource + " updated with -> " + request.downloadHandler.text);

                AssetDatabase.Refresh();
            };
        }

        private static UnityWebRequest WebRequest(string url) => new(url, "GET", new DownloadHandlerBuffer(), null);
    }
}