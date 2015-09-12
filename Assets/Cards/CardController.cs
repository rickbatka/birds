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
		}

	}
}
