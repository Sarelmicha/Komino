using BestHTTP;
using ServerResponses.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ResourcesVersionsAPI : MonoBehaviour
{
    public Task<FilesRoot> GetResources(int versionNumber)
    {
        Debug.Log(versionNumber);
        HTTPRequest request = new HTTPRequest(new Uri(Config.GetServerUrl("resources-version/v/" + versionNumber)), HTTPMethods.Get);
        return request.GetFromJsonResultAsync<FilesRoot>();
    }
}
