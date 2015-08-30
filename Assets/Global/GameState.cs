using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Global
{
	static class GameState
	{
		private static State State = AllGameStates.MyTurn_Battleground;
		public static StateNames CurrentStateName { get { return State.StateName; } }
		
		public static Player LocalPlayer;

		public static bool CantransitionTo(State toState)
		{
			return State.CantransitionTo(toState);
		}

		public static void TransitionTo(State toState)
		{
			var fromState = State;
			State = toState;
		}
	}

	static class AllGameStates
	{
		public static readonly State MyTurn_Battleground = new State(
			state: StateNames.myturn_battleground,
			allowedTransitionsTo: new[] { StateNames.myturn_cards }
		);
		public static readonly State MyTurn_Cards = new State(
			state: StateNames.myturn_cards,
			allowedTransitionsTo: new[] { StateNames.myturn_battleground }
		);
	}

	public enum StateNames
	{
		myturn_battleground,
		myturn_cards,
		notmyturn_battleground,
		notmyturn_cards,
		mainmenu
	}

	class State
	{
		public readonly StateNames StateName;
		StateNames[] AllowedTransitionsTo;

		public State(StateNames state, StateNames[] allowedTransitionsTo) 
		{ 
			StateName = state;
			AllowedTransitionsTo = allowedTransitionsTo;
		}

		public bool CantransitionTo(State toState)
		{
			return AllowedTransitionsTo.Any(allowedState => allowedState == toState.StateName);
		}
	}

	
}
