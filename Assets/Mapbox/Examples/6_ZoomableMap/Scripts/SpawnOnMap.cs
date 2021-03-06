namespace Mapbox.Examples
{
	using UnityEngine;
	using Mapbox.Utils;
	using Mapbox.Unity.Map;
	using Mapbox.Unity.MeshGeneration.Factories;
	using Mapbox.Unity.Utilities;
	using System.Collections.Generic;
	using UnityEngine.UI;
	using TMPro;

	public class SpawnOnMap : MonoBehaviour
	{
		[SerializeField]
		AbstractMap _map;

		private GameObject sphereChild;

		private double[] _LatitudeID = new double[5]; //155

		[Geocode]
		string[] _locationStrings = new string[5]   //155 antes
		{
			"41.802921, -6.774131", //near home 2
			"41.802901, -6.774131", //near home
			"41.796782, -6.769779", //Arvore 1 ipb
			"41.797185, -6.770038", //Arvore 2 ipb
			"41.802016, -6.768048", //unidade hospitalar de braganca	
									/*								
			"41.014915, -6.956071", //Freguesia: Escalhão / Barca d´alva 
			"41.014825, -6.956130",
			"41.015108, -6.955879",
			"41.015191, -6.955884",
			"41.015265, -6.955784",
			"41.015287, -6.955612",
			"41.015413, -695561",
			"41.015503, -6.955646",
			"41.016026, -6.955386",
			"41.016102, -6.955354",
			"41.016185,-6.955432",
			"41.016367, -6.955308",
			"41.014648, -6.956252",
			"41.014551, -6.956211",
			"41.014433, -6.956271",
			"41.014328, -6.956343",
			"41.014242, -6.956194",
			"41.014209, -6.956288",
			"41.014208, -6.956461",
			"41.014165, -6.956522",				
			"41.011997,	-7.019437", //Almendra
			"41.011073, -7.019701",
			"41.011136, -7.019744",
			"41.011109, -7.019878",
			"41.011039, -7.019884",
			"41.011082, -7.020135",
			"41.010945, -7.020159",
			"41.010954, -7.019827",
			"41.010905, -7.019684",
			"41.010948, -7.019534",
			"41.010759, -7.019382",
			"41.010865, -7.019261",
			"41.010898, -7.019172",
			"41.010815, -7.019055",
			"41.010851, -7.018647",
			"41.010771, -7.018578",
			"41.010507, -7.018310",
			"41.010974, -7.019092",
			"41.001603, -7.059390",
			"41.001538, -7.059408",
			"41.001457, -7.059451",
			"41.001322, -7.059253",
			"41.001364, -7.059471",
			"41.001354, -7.059639",
			"41.001348, -7.059816",
			"41.001338, -7.059880",
			"41.001346, -7.059965",
			"41.001462, -7.059657",					
			"41.025230, -7.138571", //Muxagata
			"41.025171, -7.138520",
			"41.025092, -7.138122",
			"41.025021, -7.138323",
			"41.024921, -7.138238",
			"41.024967, -7.138542",
			"41.025072, -7.138611",
			"41.024985, -7.138664",
			"41.024999, -7.138749",
			"41.025005, -7.138855",
			"41.025025, -7.138954",
			"41.025162, -7.138657",
			"41.037742, -7.162662",
			"41.037800, -7.162659",
			"41.037900, -7.162607",
			"41.037803, -7.162427",
			"41.037964, -7.162555",
			"41.038042, -7.162522",
			"41.037898, -7.162400",
			"41.037959, -7.162358",
			"41.038042, -7.162419",
			"41.038127, -7.162473",
			"41.038180, -7.162429",
			"41.038098, -7.162351",
			"41.038095, -7.162254",
			"41.038197, -7.162241",
			"41.038221, -7.162325",
			"41.038250, -7.162152",
			"41.038246, -7.162080",
			"41.038380, -7.161879",
			"41.038437, -7.161829",
			"41.038345, -7.161367",						
			"41.074144, -7.132837", //Salgueiro
			"41.074073, -7.132765",
			"41.074097, -7.132613",
			"41.074032, -7.132664",
			"41.074028, -7.132809",
			"41.073959, -7.132616",
			"41.073997, -7.132548",
			"41.073952, -7.132329",
			"41.073961, -7.132203",
			"41.073875, -7.131955",
			"41.073944, -7.131900",
			"41.074027, -7.131870",
			"41.074007, -7.132022",
			"41.074056, -7.132158",
			"41.074086, -7.132277",
			"41.074107, -7.132397",
			"41.074064, -7.132417",
			"41.073437, -7.132498",
			"41.074209, -7.132654",
			"41.074250, -7.132762",			
			"41.103861, -7.140085", //Vila Nova de Foz Côa - Entrada da costa Foz côa (Pocinho)
			"41.103799, -7.140085",
			"41.103724, -7.140103",
			"41.103652, -7.140073",
			"41.103590, -7.140095",
			"41.103457, -7.140126",
			"41.103408, -7.140133",
			"41.103340, -7.140174",
			"41.103552, -7.140213",
			"41.103598, -7.140272",
			"41.103678, -7.140259",
			"41.103748, -7.140259",
			"41.103825, -7.140284",
			"41.103827, -7.140372",
			"41.103854, -7.140454",
			"41.103664, -7.140486",
			"41.103681, -7.140541",
			"41.103760, -7.140574",
			"41.103987, -7.140294",
			"41.103917, -7.140118",					
			"41.117250, -7.136816", //Vila Nova de Foz Côa - Vale Verde, Pocinho
			"41.117323, -7.136775",
			"41.117431, -7.136828",
			"41.117435, -7.136779",
			"41.117740, -7.136708",
			"41.117795, -7.136747",
			"41.117852, -7.136747",
			"41.118000, -7.136837",
			"41.118063, -7.136698",
			"41.118079, -7.136383",
			"41.118206, -7.136143",
			"41.118215, -7.136058",
			"41.117914, -7.136060",
			"41.117795, -7.136029",
			"41.117784, -7.135922",
			"41.117652, -7.136104",
			"41.117653, -7.136244",
			"41.117555, -7.136493",
			"41.117484, -7.136456",
			"41.117361, -7.136349",
			"41.117304, -7.136355",
			"41.117239, -7.136379",
			"41.117302, -7.136477",
			"41.117240, -7.136503",
			"41.117256, -7.136705",
			"41.117434, -7.136676",
			"41.117436, -7.136479",
			"41.117612, -7.136611",
			"41.117565, -7.136557",
			"41.117556, -7.136727"  */
		};

		private double[,] _locationDoubles = new double[5, 2] //155 antes
		{
			{41.802921, -6.774121}, //near home 2
			{41.802901, -6.774131}, //near home
			{41.796782, -6.769779},  //Arvore 1 ipb
			{41.797185, -6.770038}, //Arvore 2 ipb
			{41.802016, -6.768048}, //unidade hospitalar de braganca
									/*
			{41.014915, -6.956071},	//Freguesia: Escalhão / Barca d´alva
			{41.014825, -6.956130},
			{41.015108, -6.955879},
			{41.015191, -6.955884},
			{41.015265, -6.955784},
			{41.015287, -6.955612},
			{41.015413, -6.955611},
			{41.015503, -6.955646},
			{41.016026, -6.955386},
			{41.016102, -6.955354},
			{41.016185, -6.955432},
			{41.016367, -6.955308},
			{41.014648, -6.956252},
			{41.014551, -6.956211},
			{41.014433, -6.956271},
			{41.014328, -6.956343},
			{41.014242, -6.956194},
			{41.014209, -6.956288},
			{41.014208, -6.956461},
			{41.014165, -6.956522},
			{41.011997, -7.019437}, //Almendra
			{41.011073, -7.019701},
			{41.011136, -7.019744},
			{41.011109, -7.019878},
			{41.011039, -7.019884},
			{41.011082, -7.020135},
			{41.010945, -7.020159},
			{41.010954, -7.019827},
			{41.010905, -7.019684},
			{41.010948, -7.019534},
			{41.010759, -7.019382},
			{41.010865, -7.019261},
			{41.010898, -7.019172},
			{41.010815, -7.019055},
			{41.010851, -7.018647},
			{41.010771, -7.018578},
			{41.010507, -7.018310},
			{41.010974, -7.019092},
			{41.001603, -7.059390},
			{41.001538, -7.059408},
			{41.001457, -7.059451},
			{41.001322, -7.059253},
			{41.001364, -7.059471},
			{41.001354, -7.059639},
			{41.001348, -7.059816},
			{41.001338, -7.059880},
			{41.001346, -7.059965},
			{41.001462, -7.059657},
			{41.025230, -7.138571}, //Muxagata
			{41.025171, -7.138520},
			{41.025092, -7.138122},
			{41.025021, -7.138323},
			{41.024921, -7.138238},
			{41.024967, -7.138542},
			{41.025072, -7.138611},
			{41.024985, -7.138664},
			{41.024999, -7.138749},
			{41.025005, -7.138855},
			{41.025025, -7.138954},
			{41.025162, -7.138657},
			{41.037742, -7.162662},
			{41.037800, -7.162659},
			{41.037900, -7.162607},
			{41.037803, -7.162427},
			{41.037964, -7.162555},
			{41.038042, -7.162522},
			{41.037898, -7.162400},
			{41.037959, -7.162358},
			{41.038042, -7.162419},
			{41.038127, -7.162473},
			{41.038180, -7.162429},
			{41.038098, -7.162351},
			{41.038095, -7.162254},
			{41.038197, -7.162241},
			{41.038221, -7.162325},
			{41.038250, -7.162152},
			{41.038246, -7.162080},
			{41.038380, -7.161879},
			{41.038437, -7.161829},
			{41.038345, -7.161367},
			{41.074144, -7.132837}, //Salgueiro
			{41.074073, -7.132765},
			{41.074097, -7.132613},
			{41.074032, -7.132664},
			{41.074028, -7.132809},
			{41.073959, -7.132616},
			{41.073997, -7.132548},
			{41.073952, -7.132329},
			{41.073961, -7.132203},
			{41.073875, -7.131955},
			{41.073944, -7.131900},
			{41.074027, -7.131870},
			{41.074007, -7.132022},
			{41.074056, -7.132158},
			{41.074086, -7.132277},
			{41.074107, -7.132397},
			{41.074064, -7.132417},
			{41.073437, -7.132498},
			{41.074209, -7.132654},
			{41.074250, -7.132762},
			{41.103861, -7.140085}, //Vila Nova de Foz Côa	Entrada da costa Foz côa (Pocinho)
			{41.103799, -7.140085},
			{41.103724, -7.140103},
			{41.103652, -7.140073},
			{41.103590, -7.140095},
			{41.103457, -7.140126},
			{41.103408, -7.140133},
			{41.103340, -7.140174},
			{41.103552, -7.140213},
			{41.103598, -7.140272},
			{41.103678, -7.140259},
			{41.103748, -7.140259},
			{41.103825, -7.140284},
			{41.103827, -7.140372},
			{41.103854, -7.140454},
			{41.103664, -7.140486},
			{41.103681, -7.140541},
			{41.103760, -7.140574},
			{41.103987, -7.140294},
			{41.103917, -7.140118},
			{41.117250, -7.136816}, //Vila Nova de Foz Côa //   Vale Verde, Pocinho
			{41.117323, -7.136775},
			{41.117431, -7.136828},
			{41.117435, -7.136779},
			{41.117740, -7.136708},
			{41.117795, -7.136747},
			{41.117852, -7.136747},
			{41.118000, -7.136837},
			{41.118063, -7.136698},
			{41.118079, -7.136383},
			{41.118206, -7.136143},
			{41.118215, -7.136058},
			{41.117914, -7.136060},
			{41.117795, -7.136029},
			{41.117784, -7.135922},
			{41.117652, -7.136104},
			{41.117653, -7.136244},
			{41.117555, -7.136493},
			{41.117484, -7.136456},
			{41.117361, -7.136349},
			{41.117304, -7.136355},
			{41.117239, -7.136379},
			{41.117302, -7.136477},
			{41.117240, -7.136503},
			{41.117256, -7.136705},
			{41.117434, -7.136676},
			{41.117436, -7.136479},
			{41.117612, -7.136611},
			{41.117565, -7.136557},
			{41.117556, -7.136727} */
		};

		Vector2d[] _locations;

		[SerializeField]
		float _spawnScale = 100f;
		
		[SerializeField]
		GameObject _markerPrefab;

		List<GameObject> _spawnedObjects;

		private int myIndex;


		public void CompareNextTreeLocationsWithSpawnedObjects(double Latitude, double Longitude)
        {
			//foreach ()
			//Debug.Log("Latitude recebida: " + Latitude + "Length: " + _locationDoubles.Length);

			for (int lines = 0; lines <= _locationDoubles.GetLength(0) - 1; lines++)
            {
				//Debug.Log("Location Doubles: " + _locationDoubles[lines, 0]);

				if (Latitude == _locationDoubles[lines, 0])
                {
					Debug.Log("SpawnObject__ RECEIVED: " + _locationDoubles[lines, 0] + " lines: " + lines);

					myIndex = lines;
				}
            }

			int i = 0;

			foreach (GameObject go in _spawnedObjects) ///CHANGE NEAR TREE ID NUMBER COLOR
			{

				LabelTextSetter myLabel = _spawnedObjects[i].GetComponentInChildren<LabelTextSetter>();

				//int num = i - 1;
				if(i == myIndex)
                {
					MeshRenderer meshRenderer = _spawnedObjects[i].GetComponentInChildren<MeshRenderer>();
					meshRenderer.material.color = Color.black;
				}
                else
                {
					MeshRenderer meshRenderer = _spawnedObjects[i].GetComponentInChildren<MeshRenderer>();
					meshRenderer.material.color = Color.white;
				}
				Debug.Log("myLabel.GetLat: " + myLabel.GetLatitude());
				if (Latitude == myLabel.GetLatitude())
				{
					//Debug.Log("Achou o prefab");
					//Debug.Log("Label: " + myLabel.GetLatitude());
					///MeshRenderer meshRenderer = _spawnedObjects[i].GetComponentInChildren<MeshRenderer>();
					///meshRenderer.material.color = Color.black;
				}

				i++;


			}

		}

        private void Start()
		{

			_locations = new Vector2d[_locationStrings.Length];
			_spawnedObjects = new List<GameObject>();

			/*
			foreach (string lines in _locationStrings)
            {
				Debug.Log("Li : " + _locationStrings.ToString());
				//double ParsedString = double.Parse(_locationStrings[lines]);
				//Debug.Log("Locations: " + ParsedString.ToString());
            }*/

			for (int i = 0; i < _locationStrings.Length; i++)
			{
				var locationString = _locationStrings[i];
				//Debug.Log("coordenadas: " + locationString.ToString());
				_locations[i] = Conversions.StringToLatLon(locationString);
				var instance = Instantiate(_markerPrefab);
				instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
				instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);

				//Adding name to the Tree instances
				instance.transform.name = "tree_"+i.ToString();			
				_spawnedObjects.Add(instance);
			}

			SetLabelsOnPoints();
		}

		private void SetLabelsOnPoints()
        {
			int i = 0;

			//Debug.Log("Spw qnt: " + _spawnedObjects.Count.ToString());
			foreach (GameObject go in _spawnedObjects)
            {
				LabelTextSetter myLabel = _spawnedObjects[i].GetComponentInChildren<LabelTextSetter>();
				int num = i - 1;
				myLabel.SetText(""+i); //Passando IDLabel e Latitude

				myLabel.SetDate("12/12/2012");

				//Debug.Log("LocationDoubles: " + _locationDoubles[i, 0].ToString() );
				int lines = i;
				//Debug.Log("lines: " + lines);
				myLabel.SetLatitude(_locationDoubles[lines, 0]);

				GameObject sphere = GameObject.Find("Sphere");
				//int count = i - 1;
				sphere.name = "Collider_" + num;
				

				i++;
            }
        }

		private void Update()
		{
			int count = _spawnedObjects.Count;
			for (int i = 0; i < count; i++)
			{
				var spawnedObject = _spawnedObjects[i];
				var location = _locations[i];
				spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
				spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
			}
		}
	}

}