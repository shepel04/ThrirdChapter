using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Serialization;

public class DownloadAssetBundles : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DownloadAssetBundleFromServer());
    }

    private IEnumerator DownloadAssetBundleFromServer()
    {
        GameObject targetGameObject = null;

        string url = "https://drive.usercontent.google.com/download?id=1TUhiv4Ja1cXDfvRPZU_uZTCTTgtgsN3g&export=download&authuser=0";

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
                targetGameObject = bundle.LoadAsset(bundle.GetAllAssetNames() [0]) as GameObject;
                bundle.Unload(false);
                yield return new WaitForEndOfFrame();
            }
            www.Dispose();
        }
        InstantiateGameObjectFromAssetBundle(targetGameObject);
    }

    private void InstantiateGameObjectFromAssetBundle(GameObject targetGameObject)
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
    }

}
