using Assets.CardModels;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Assets.Global;

namespace Assets.Cards
{
	public class CardController : MonoBehaviour
	{
		public ICardModel Card;
		public Player Player;

		private Text HeaderText;
		private Button PlayButton;

		void Start() 
		{
			HeaderText = this.GetComponentsInChildren<Text>().First(t => t.name == "headertext");
			HeaderText.text = Card.Name;
			PlayButton = this.GetComponentsInChildren<Button>().First(i => i.name == "PlayButton");
			PlayButton.onClick.AddListener(HandleClick);
		}
		
		public void HandleClick()
		{
			Debug.Log("clicked me: " + this.GetInstanceID());
			Card.Use();
			Director.Instance.CardWasUsed(Player, this);

			//remove card fomr hand and destroy card viewmodel
			Player.Cards.Remove(Card);
			Destroy(this.gameObject);
		}

	}
}
