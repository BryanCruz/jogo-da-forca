using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public GameObject letra;                         // prefab da letra no Game
	public GameObject centroDaTela;                  // objeto que indica o centro da tela

	private readonly string[] palavrasOcultas = new string[] { "carro", "elefante", "futebol" }; // array de palavras ocultas
	private string palavraOculta;               // palavra oculta a ser descoberta

	private int tamanhoPalavraOculta;            // tamanho da palavra oculta
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
		CheckTeclado();
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

			GameObject letterObject = Instantiate( letra, novaPosicao, Quaternion.identity );
			letterObject.name = "letra" + (i + 1);
			letterObject.transform.SetParent( GameObject.Find( "Canvas" ).transform );
		}
	}

	void InitGame()
	{
		int numeroAleatorio = Random.Range( 0, palavrasOcultas.Length );
		palavraOculta = palavrasOcultas[numeroAleatorio];      // definição da palavra a ser descoberta

		tamanhoPalavraOculta = palavraOculta.Length;           // número de letras da palavra
		palavraOculta = palavraOculta.ToUpper();               // transforma a palavra para maiúscula

		letrasOcultas = palavraOculta.ToCharArray();           // instancia-se o array de char e copia a palavra no array de char
		letrasDescobertas = new bool[tamanhoPalavraOculta];    // instancia-se o array de bool
	}

	// Checa se o usuário digitou as letras corretas da palavra
	// e as exibe se o usuário acertar
	void CheckTeclado()
	{
		if ( Input.anyKeyDown )
		{
			char letraTeclada = Input.inputString.ToCharArray()[0];
			int letraTecladaComoInt = System.Convert.ToInt32( letraTeclada );

			if ( letraTecladaComoInt >= 97 && letraTecladaComoInt <= 122 )
			{
				letraTeclada = char.ToUpper( letraTeclada );
				for ( int i = 0; i < tamanhoPalavraOculta; i++ )
				{
					if ( !letrasDescobertas[i] && letrasOcultas[i] == letraTeclada )
					{
						letrasDescobertas[i] = true;

						Text componenteDaLetraI = GameObject.Find( "letra" + (i + 1) ).GetComponent<Text>();
						componenteDaLetraI.text = letraTeclada.ToString();
					}
				}
			}
		}
	}
}
