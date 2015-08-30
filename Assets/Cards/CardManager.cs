using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Cards
{
	class CardManager
	{
		GameObject CardCanvas;
		Vector3 hideByMoving = new Vector3(18, 0, 0);
		Vector3 activatedPosition;
		public CardManager(GameObject cardCanvas)
		{
			CardCanvas = cardCanvas;
			activatedPosition = CardCanvas.transform.position;
			Deactivate();
		}

		public void Activate()
		{
			CardCanvas.transform.position = activatedPosition;
			CardCanvas.transform.position -= hideByMoving;
			CardCanvas.SetActive(true);
			for (int i = 0; i < 4; i++)
			{

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
