using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectCode
{
    public static class AssetFactory
    {
        public static async Task<T> Create<T>(string key, Vector3 pos = default, bool activeState = true) where T : MonoBehaviour
        {
            var loadObject = await AssetLoader.LoadResourceAsync<T>(key);
            var createObject = Object.Instantiate(loadObject);
            createObject.transform.position = pos;
            createObject.gameObject.SetActive(activeState);
            return createObject;
        }
    }
}