﻿namespace Nexus.Crypto.SDK;

public interface INexusApiGetAccessToken
{
    /// <summary>
    /// This method should return the Bearer Token needed to authenticate with the Nexus Label API
    /// </summary>
    /// <returns></returns>
    Task<string> GetAccessToken();
}
