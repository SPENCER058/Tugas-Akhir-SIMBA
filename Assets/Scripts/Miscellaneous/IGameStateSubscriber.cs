using SIMBA.Enum;

public interface IGameStateSubscriber
{
	void HandleStateChangeEvent (GameState state);
}
