using System;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectCode
{
    public static class AssetLoader
    {
        public static Task<T> LoadResourceAsync<T>(string path) where T : UnityEngine.Object
        {
            var tcs = new TaskCompletionSource<T>();
            ResourceRequest request = Resources.LoadAsync<T>(path);

            request.completed += _ =>
            {
                if (request.asset is T asset)
                    tcs.SetResult(asset);
                else
                    tcs.SetException(new Exception($"Failed to load resource at path: {path}"));
            };

            return tcs.Task;
        }
    }
}