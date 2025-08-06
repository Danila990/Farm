using System;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ProjectCode
{
    public static class AssetLoader
    {
        public static Task<T> LoadResourceAsync<T>(string path) where T : Object
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

        public static async Task<T> LoadInstantiate<T>(string path) where T : Object
        {
            T data = await LoadResourceAsync<T>(path);
            return Object.Instantiate<T>(data);
        }
    }
}