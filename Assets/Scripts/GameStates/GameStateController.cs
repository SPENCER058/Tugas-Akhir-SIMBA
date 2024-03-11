using SIMBA.Enum;
using UnityEngine;

namespace SIMBA.StateController
{
	public class GameStateController
	{
		public System.Action<GameState> OnGameStateChanged;

		[SerializeField] private GameState _currentGameState;

		public GameState GetGameState
		{
			get { return _currentGameState; }
		}

		public void SetGameState(GameState gameState)
		{
			if (_currentGameState != gameState)
			{
				_currentGameState = gameState;
				OnGameStateChanged?.Invoke(_currentGameState);
			}
		}
	}

}
