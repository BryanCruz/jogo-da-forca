using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject letra;                         // prefab da letra no Game
	public GameObject centroDaTela;                  // objeto que indica o centro da tela

	private string palavraOculta = "";               // palavra oculta a ser descoberta
	private int tamanhoPalavraOculta = 0;            // tamanho da palavra oculta
	private char[] letrasOcultas;                    // letras da palavra oculta
	private bool[] letrasDescobertas;                // indicador de quais letras foram descobertas

	// Start is called before the first frame update
	void Start()
	{
		centroDaTela = GameObject.Find( "centroDaTela" );
		InitGame();
		InitLetras();
	}

	// Update is called once per frame
	void Update()
	{
	}

	void InitLetras()
	{
		int numLetras = tamanhoPalavraOculta;
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

	void InitGame()
	{
		palavraOculta = "Elefante";                            // definição da palavra a ser descoberta
		tamanhoPalavraOculta = palavraOculta.Length;           // número de letras da palavra
		palavraOculta = palavraOculta.ToUpper();               // transforma a palavra para maiúscula

		letrasOcultas = palavraOculta.ToCharArray();           // instancia-se o array de char e copia a palavra no array de char
		letrasDescobertas = new bool[tamanhoPalavraOculta];    // instancia-se o array de bool
	}
}
