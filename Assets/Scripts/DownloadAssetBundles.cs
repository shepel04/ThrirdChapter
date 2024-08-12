using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.Serialization;

public class DownloadAssetBundles : MonoBehaviour
{

    [SerializeField] private Sprite _loadAssetSprite;
    void Start()
    {
        var myLoadedAssetBundle 
            = AssetBundle.LoadFromFile("Assets/AssetBundles/test");
        if (myLoadedAssetBundle == null) {
            Debug.Log("Failed to load AssetBundle!");
            return;
        }
        _loadAssetSprite = myLoadedAssetBundle.LoadAsset<Sprite>("pngtree-watermelon-fruit-vector-illustration-png-image_6099706");
        //Instantiate(prefab);
    }

    private IEnumerator DownloadAssetBundleFromServer()
    {

        string url = "https://drive.usercontent.google.com/u/0/uc?id=1fG-jTqFM1B5IPpT7ox62jKMFDYJ6djsw&export=download";

        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning("Error on the get request at : " + url + " " + www.error);
            }
            else
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
                var targetGameObject = bundle.LoadAssetAsync(bundle.GetAllAssetNames() [0]);
                //bundle.Unload(false);
                yield return targetGameObject;
            }
            www.Dispose();
        }
        //InstantiateGameObjectFromAssetBundle(targetGameObject);
    }

    /*private void InstantiateGameObjectFromAssetBundle(Sprite targetGameObject)
    {
        if (targetGameObject != null)
        {
            GameObject instanceTarget = Instantiate(targetGameObject);
            instanceTarget.transform.position = Vector3.zero;
        }
        else
        {
            Debug.LogWarning("Asset bundle is null");
        }
    }*/

}
