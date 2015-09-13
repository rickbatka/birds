using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Global
{
	public static class GameState
	{
		private static State _state;
		public static State State 
		{
			get
			{
				return _state;	
			} 
			set
			{
				var fromState = _state;
				_state = value;
				if(fromState != null)
				{
					Debug.Log(string.Format("changed state from {0} to {1}", fromState.StateName, value.StateName));
				}
			}
		}
		public static StateNames CurrentStateName { get { return State.StateName; } }
		
		public static Player LocalPlayer;

		public static bool CantransitionTo(State toState)
		{
			return State.CantransitionTo(toState);
		}
	}

	public static class AllGameStates
	{
		public static readonly State MyTurn_Battleground = new State(
			state: StateNames.myturn_battleground,
			allowedTransitionsTo: new[] { StateNames.myturn_cards }
		);
		public static readonly State MyTurn_Cards = new State(
			state: StateNames.myturn_cards,
			allowedTransitionsTo: new[] { StateNames.myturn_battleground }
		);
		public static readonly State TheirTurn_Battleground = new State(
			state: StateNames.theirturn_battleground,
			allowedTransitionsTo: new[] { StateNames.theirturn_cards }
		);
		public static readonly State TheirTurn_Cards = new State(
			state: StateNames.theirturn_cards,
			allowedTransitionsTo: new[] { StateNames.theirturn_battleground }
		);
	}

	public enum StateNames
	{
		myturn_battleground,
		myturn_cards,
		theirturn_battleground,
		theirturn_cards,
		mainmenu
	}

	public class State
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
