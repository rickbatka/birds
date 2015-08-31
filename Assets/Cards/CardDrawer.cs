using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Global;
using UnityEngine;

namespace Assets.Cards
{
	class CardDrawer : MonoBehaviour
	{
		public Transform CardPrefab;
		GameObject CardCanvas;
		GameObject CardsPanel;
		Vector3 hideByMoving = new Vector3(18, 0, 0);
		Vector3 activatedPosition;

		void Awake()
		{
			CardCanvas = this.gameObject;
			CardsPanel = this.transform.Find("CardsPanel").gameObject;
			activatedPosition = CardCanvas.transform.position;
			Deactivate();
		}

		public void Activate()
		{
			CardCanvas.transform.position = activatedPosition;
			CardCanvas.transform.position -= hideByMoving;
			CardCanvas.SetActive(true);

			foreach (var card in GameState.LocalPlayer.Cards)
			{
				var newCard = Instantiate(CardPrefab);
				newCard.SetParent(CardsPanel.transform, false);
			}

			iTween.MoveTo(CardCanvas, new Hashtable()
			{
				{"position", CardCanvas.transform.position + hideByMoving},
				{"time", 1.0f},
				{"easetype", iTween.EaseType.spring}
			});

		}

		public void Deactivate()
		{
			CardCanvas.SetActive(false);
		}

	}
}
