using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using UnityEngine.UI;
using AppState.Game;

public class BundleWebLoader : MonoBehaviour
{
    [SerializeField] GameObject bundlesHolder = null;

    private List<string> bundlesUrls;
    private PersistenceDataManager persistenceDataManager = null;

    private void Awake()
    {
        bundlesUrls = new List<string>();
        persistenceDataManager = new PersistenceDataManager();


        //Koalas
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/koala/coco");
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/koala/foto");
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/koala/nokotonocho");
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/koala/kita");
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/koala/woro");
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/koala/nojo");
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/koala/wera");
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/koala/goro");

        //Squirels
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/squirrel/aagha");
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/squirrel/kamila");
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/squirrel/yona");
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/squirrel/sigma");
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/squirrel/tory");
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/squirrel/leep");
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/squirrel/johnson");
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/squirrel/shien");

        //Mouses
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/mouses/nicht");
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/mouses/kai");
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/mouses/kei");
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/mouses/nina");
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/mouses/mira");
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/mouses/lora");
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/mouses/nol");
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/mouses/max");

        //Platypus
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/platypus/apoca");
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/platypus/tara");
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/platypus/jok");
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/platypus/hope");
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/platypus/smithy");
        bundlesUrls.Add("https://storage.googleapis.com/greed-resources/StreamingAssets/characters/platypus/dleihs");
    }

    public void GetBundleObject(string bundleURL, string bundleName, Action<GameObject> onLoadAction, Transform bundleParent)
    {
        StartCoroutine(GetBundle(bundleURL, bundleName, onLoadAction, bundleParent));
    }

    private IEnumerator GetBundle(string bundleURL, string bundleName, Action<GameObject> onLoadAction,Transform bundleParent)
    {
        print("im in GetBundle and bundleURL is " + bundleURL);
        //  get the AssetBundle
        AssetBundle bundle = null;
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(bundleURL);
        yield return request.SendWebRequest();
        print("im here after request.SendWebRequest()");

        if (request.isNetworkError)
        {
            Debug.Log("network error");
            yield break;
        }

        print("im here before download bundle");
        bundle = DownloadHandlerAssetBundle.GetContent(request);
        print("im here after download bundle");

        print("bundle is " + bundle);

        if (bundle != null)
        {
            string rootAssetPath = bundle.GetAllAssetNames()[0];
            print("rootAssetPath is " + rootAssetPath);
            GameObject serverBundle = Instantiate(bundle.LoadAsset(rootAssetPath) as GameObject, bundleParent);     
            bundle.Unload(false);
            onLoadAction(serverBundle);
        }     
    }

    public bool GetAllBundles()
    {

        GetComponent<Loader>().SetInfoText("Load assets for the first time... this may take a minute.");

        //Make an API call to check wether there is new texture resources to download.
        int loadedBundles = 0;

        for (int i = 0; i < bundlesUrls.Count; i++)
        {
            //If there are download the bundle object           
            GetBundleObject(bundlesUrls[i], name,
               (bundle) =>
               {
                   print("inside callback and k = " + loadedBundles + "and bundleUrls.Count = " + bundlesUrls.Count);
                   persistenceDataManager.SaveImagesToDisk(bundle);
                   if (loadedBundles == bundlesUrls.Count - 1)
                   {
                       print("all bundles are loaded.");
                       PlayerPrefs.SetInt("isBundleLoaded", 1);
                   }

                   loadedBundles++;
               },
               bundlesHolder.transform);
        }

        return false;
    }

    
}
