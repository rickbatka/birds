﻿using System;
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
		int CardPlacementOffset = 200;
		
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

			for(int i = 0; i < GameState.LocalPlayer.Cards.Count; i++)
			{
				var newCard = Instantiate(CardPrefab);
				newCard.GetComponent<CardController>().Card = GameState.LocalPlayer.Cards[i];
				newCard.SetParent(CardsPanel.transform, false);
				newCard.transform.localPosition += new Vector3(i*CardPlacementOffset, 0, 1);
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
			var childrenToDelete = new List<GameObject>();
			foreach (Transform child in CardsPanel.transform)
			{
				childrenToDelete.Add(child.gameObject);
			}
			childrenToDelete.ForEach(child => GameObject.Destroy(child));
		}

	}
}
