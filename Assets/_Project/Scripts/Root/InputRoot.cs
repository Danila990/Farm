using System.Threading.Tasks;
using UnityEngine;

namespace ProjectCode
{
    public enum InputType
    {
        Pc = 0,
        Mobile = 1,
    }

    public class InputRoot : MonoBehaviour
    {
        [SerializeField] private InputType _inputType;

        private IInputService _inputService;

        public async Task Initializable()
        {
            await CreateInput();
            ServiceLocator.Register(_inputService);
        }

        public async Task Startable()
        {
            _inputService.Activate();
        }

        private async Task CreateInput()
        {
            string loadPath = $"Input/{_inputType.ToString()}Input";
            if (_inputType == InputType.Pc)
                _inputService = await AssetFactory.Create<PcInput>(loadPath);
            else
                _inputService = await AssetFactory.Create<MobileInput>(loadPath);
        }
    }
}