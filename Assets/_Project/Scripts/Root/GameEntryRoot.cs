using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectCode
{
    public class GameEntryRoot : MonoBehaviour
    {
        [SerializeField] private InputRoot _inputRoot;
        [SerializeField] private PlayerRoot _playerRoot;
        [SerializeField] private GridRoot _gridRoot;

        private async void Start()
        {
            await Initializable();
            await Startable();
        }

        private async Task Initializable()
        {
            await _gridRoot.Initializable();
            await _inputRoot.Initializable();
            await _playerRoot.Initializable();
        }

        private async Task Startable()
        {
            await _inputRoot.Startable();
            await _playerRoot.Startable();
        }
    }
}