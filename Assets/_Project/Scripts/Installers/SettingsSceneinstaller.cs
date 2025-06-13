using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SettingsGameInstaller", menuName = "Installers/SettingsGameInstaller")]
public class SettingsGameInstaller : ScriptableObjectInstaller<SettingsGameInstaller>
{
    public override void InstallBindings()
    {
    }
}