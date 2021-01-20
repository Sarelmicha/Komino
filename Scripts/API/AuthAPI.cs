using BestHTTP;
using ServerResponses.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AuthAPI
{
    public Task<PlayerInfoResponse> LoginWithDeviceId()
    {
        HTTPRequest request = new HTTPRequest(new Uri(Config.GetServerUrl("auth/login/mobile")), HTTPMethods.Post);
        request.AddField("device_id", SystemInfo.deviceUniqueIdentifier);
        return request.GetFromJsonResultAsync<PlayerInfoResponse>();
    } 
}
