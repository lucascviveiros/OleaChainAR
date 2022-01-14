namespace Mapbox.Examples
{
	using Mapbox.Unity.MeshGeneration.Interfaces;
	using System.Collections.Generic;
	using UnityEngine;

	public class LabelTextSetter : MonoBehaviour, IFeaturePropertySettable
	{
		[SerializeField]
		TextMesh _textMesh;

		[SerializeField]
		TextMesh _testLatitude;

		private enum TypeInfo { Oliveira = 0, Arte = 1 }

		//Olive trees database
		private string ID;
		private string info;
		private int age;
		private string plantingDate;
		private double LatitudeID;

		public void Set(Dictionary<string, object> props)
		{
			_textMesh.text = "";

			if (props.ContainsKey("name"))
			{
				_textMesh.text = props["name"].ToString();
			}
			else if (props.ContainsKey("house_num"))
			{
				_textMesh.text = props["house_num"].ToString();
			}
			else if (props.ContainsKey("type"))
			{
				_textMesh.text = props["type"].ToString();
			}
		}

		public void SetText(string text)
        {
			_textMesh.text = text;
			ID = text;
        }

		public void SetLatitude(double Latitude)
		{
			LatitudeID = Latitude;
			//_testLatitude.text = LatitudeID.ToString();
		}
		public void SetDate(string receivedDate)
        {
			plantingDate = receivedDate;
        }

		public string GetId()
        {
			return ID;
        }

		public string GetDate()
        {
			return plantingDate;
        }

		public double GetLatitude()
        {
			return LatitudeID;
        }
    }
}