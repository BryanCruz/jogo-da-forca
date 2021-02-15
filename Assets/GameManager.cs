using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject letra;
	public GameObject centroDaTela;

	// Start is called before the first frame update
	void Start()
	{
		centroDaTela = GameObject.Find( "centroDaTela" );
		InitLetras();
	}

	// Update is called once per frame
	void Update()
	{
	}

	void InitLetras()
	{
		int numLetras = 5;
		for ( int i = 0; i < numLetras; i++ )
		{
			float x = centroDaTela.transform.position.x + ((i - numLetras / 2.0f) * 80);
			float y = centroDaTela.transform.position.y;
			float z = centroDaTela.transform.position.z;

			Vector3 novaPosicao = new Vector3( x, y, z );

			GameObject l = Instantiate( letra, novaPosicao, Quaternion.identity );
			l.name = "letra" + (i + 1);
			l.transform.SetParent( GameObject.Find( "Canvas" ).transform );
		}
	}
}
