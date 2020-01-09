using AGames.Spillz.Scripts.Input.InputProcessors;
using AGames.Spillz.Scripts.Manager;
using ProjectCore.Input;
using ProjectCore.Misc;
using ProjectCore.ServiceLocator;

public class LevelController : CachedBehaviour, IInputReceiver<LevelControlArgs>
{
    private InputManager _inputManager;
    private ManagerLevel _managerLevel;
    private void Start()
    {
        _inputManager = ServiceLocator.Resolve<InputManager>();
        _inputManager.Subscribe(new InputReceiver<LevelControlArgs>(this));
        _managerLevel = ServiceLocator.Resolve<ManagerLevel>();
    }

    public void Execute(LevelControlArgs args)
    {
        if (args.NextLevel) _managerLevel.NextLevel();
        if (args.RestartLevel) _managerLevel.RestartLevel();
    }
}
