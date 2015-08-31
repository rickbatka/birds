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

		void Awake()
		{
			HeaderText = this.transform.Find("header").Find("headertext").GetComponent<Text>();
		}

		void Start() 
		{
			HeaderText.text = Card.Name;
		}

	}
}
