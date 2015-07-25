using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Aiming
{
	class AimingLine : MonoBehaviour
	{
		public float TimePerSegmentInSeconds = 0.1f;
		//public float MaxTravelDistance = 10f;
		public int MaxPoints = 100;
		public float AnimateSpeed = 2.0f;

		private LineRenderer _lineRenderer;
		private Vector3[] lastPoints;

		void Start()
		{
			_lineRenderer = GetComponent<LineRenderer>();
			_lineRenderer.useWorldSpace = true;
		}

		void Update()
		{

		}
		public void kill() 
		{
			_lineRenderer.SetVertexCount(0);
		}
		public void UpdateTrajectory(Vector3 initialPosition, Vector3 initialVelocity)
		{
			var points = new List<Vector3>();
			Vector3 currentPosition = initialPosition;
			Vector3 velocity = initialVelocity;
			//float distanceTraveled = 0f;
			int numPoints = 0;
			while (numPoints < MaxPoints) // && distanceTraveled < MaxTravelDistance && 
			{
				var newPosition = currentPosition + velocity * TimePerSegmentInSeconds + 0.5f * Physics.gravity * TimePerSegmentInSeconds * TimePerSegmentInSeconds;
				//distanceTraveled += (newPosition - currentPosition).magnitude;
				points.Add(newPosition);
				currentPosition = newPosition;
				velocity += Physics.gravity * TimePerSegmentInSeconds;
				numPoints++;
			}
			var newPoints = points.ToArray();
			animateLineTo(newPoints);
			lastPoints = newPoints;
		}

		private void animateLineTo(Vector3[] newPoints)
		{
			if (lastPoints != null)
			{
				var numSharedPoints = Math.Min(lastPoints.Length, newPoints.Length);
				for (int i = 0; i < numSharedPoints; i++)
				{
					newPoints[i] = iTween.Vector3Update(lastPoints[i], newPoints[i], AnimateSpeed);
				}
			}

			_lineRenderer.SetVertexCount(newPoints.Length);
			for (int i = 0; i < newPoints.Length; i++)
			{
				_lineRenderer.SetPosition(i, newPoints[i]);
			}
		}
	}
}
