namespace Assets.Global
{
	public class NewGameInitInfo
	{
		// player IDs, any game options / params, etc
		// previous turn data
		public readonly int LocalPlayerNumber;
		public readonly int NextUpPlayer;
		
		public NewGameInitInfo(int localPlayerNumber, int nextUpPlayer)
		{
			LocalPlayerNumber = localPlayerNumber;
			NextUpPlayer = nextUpPlayer;
		} 
	}
}
