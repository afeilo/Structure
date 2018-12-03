using Assets.FrameWork;
public class PluginModule {
	public static IStateManager GetStateManager(){
		return new UIStateManager();
	}
}
