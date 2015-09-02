using Assets.CardViewModels;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Assets.Cards
{
	public class CardController : MonoBehaviour
	{
		public ICardViewModel Card;

		private Text HeaderText;

		void Start() 
		{
			HeaderText = this.GetComponentsInChildren<Text>().First(t => t.name == "headertext");
			HeaderText.text = Card.Name;
		}

	}
}
