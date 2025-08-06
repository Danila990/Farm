using System.Threading.Tasks;
using UnityEngine;

namespace ProjectCode
{
    public class InputController : MonoBehaviour
    {
        private IInputService _inputService;

        public async Task Initialize()
        {
            _inputService = await AssetFactory.Create<PcInput>($"Input/PcInput");
            ServiceLocator.Register(_inputService);
        }

        public void Startable()
        {
            _inputService.Activate();
        }
    }
}