using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public GameObject letra;                         // prefab da letra no Game
	public GameObject centroDaTela;                  // objeto que indica o centro da tela

	//	private readonly string[] palavrasOcultas = new string[] { "carro", "elefante", "futebol" }; // array de palavras ocultas
	private string palavraOculta;                    // palavra oculta a ser descoberta

	private int tamanhoPalavraOculta;                // tamanho da palavra oculta
	private char[] letrasOcultas;                    // letras da palavra oculta
	private bool[] letrasDescobertas;                // indicador de quais letras foram descobertas

	private int score;                               // score do jogo
	private int numTentativas;                       // número de tentativas gastas
	private int maxNumtentativas;                    // número máximo de tentativas

	// Start is called before the first frame update
	void Start()
	{
		centroDaTela = GameObject.Find( "centroDaTela" );
		numTentativas = 0;
		maxNumtentativas = 10;
		score = PlayerPrefs.GetInt( "score" );

		InitGame();
		InitLetras();
		UpdateNumTentativas();
		UpdateScore();
	}

	// Update is called once per frame
	void Update()
	{
		CheckTeclado();
	}

	void InitLetras()
	{
		// inicia as componentes das letras da palavra na tela com interrogação
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
		// inicia as variáveis do jogo
		palavraOculta = pegaPalavraDoArquivo();      // definição da palavra a ser descoberta

		tamanhoPalavraOculta = palavraOculta.Length;           // número de letras da palavra
		palavraOculta = palavraOculta.ToUpper();               // transforma a palavra para maiúscula

		letrasOcultas = palavraOculta.ToCharArray();           // instancia-se o array de char e copia a palavra no array de char
		letrasDescobertas = new bool[tamanhoPalavraOculta];    // instancia-se o array de bool
	}

	// Checa se o usuário digitou as letras corretas da palavra
	// e as exibe se o usuário acertar
	void CheckTeclado()
	{
		// checa se o usuário digitou alguma coisa
		if ( Input.anyKeyDown )
		{
			char letraTeclada = Input.inputString.ToCharArray()[0];
			int letraTecladaComoInt = System.Convert.ToInt32( letraTeclada );

			if ( letraTecladaComoInt >= 97 && letraTecladaComoInt <= 122 )
			{
				// se digitou uma letra, pega a letra digitada e atualiza as variáveis do jogo
				// com as regras do Jogo da Forca

				numTentativas++;
				UpdateNumTentativas();
				if ( numTentativas > maxNumtentativas )
				{
					// se excedeu o número máximo de tentativas, abre a tela de Game Over
					SceneManager.LoadScene( "Lab1_forca" );
				}

				letraTeclada = char.ToUpper( letraTeclada );
				for ( int i = 0; i < tamanhoPalavraOculta; i++ )
				{
					if ( !letrasDescobertas[i] && letrasOcultas[i] == letraTeclada )
					{
						// se acertou a letra digitada, aumenta o score e verifíca se a palavra foi descoberta
						letrasDescobertas[i] = true;

						score = PlayerPrefs.GetInt( "score" );
						score += 1;
						PlayerPrefs.SetInt( "score", score );
						UpdateScore();

						Text componenteDaLetraI = GameObject.Find( "letra" + (i + 1) ).GetComponent<Text>();
						componenteDaLetraI.text = letraTeclada.ToString();

						verificaSePalavraDescoberta();
					}
				}
			}
		}
	}

	void UpdateNumTentativas()
	{
		// atualiza o componente de número de tentativas na tela
		Text componenteNumTentativas = GameObject.Find( "numTentativas" ).GetComponent<Text>();
		componenteNumTentativas.text = numTentativas + " | " + maxNumtentativas;

		if ( numTentativas > maxNumtentativas )
		{
			// se excedeu o número máximo de tentativas, deixa o componente com a cor vermelha
			componenteNumTentativas.color = Color.red;
		}
	}

	void UpdateScore()
	{
		// atualiza o componente de score na tela
		Text componenteScore = GameObject.Find( "scoreUI" ).GetComponent<Text>();
		componenteScore.text = "Score: " + score;
	}

	void verificaSePalavraDescoberta()
	{
		// verifica se a palavra foi edscoberta
		bool condicao = true;

		for ( int i = 0; condicao && i < tamanhoPalavraOculta; i++ )
		{
			condicao = condicao && letrasDescobertas[i];
		}

		if ( condicao )
		{
			// se a palavra foi descoberta, envia pra tela de Game Over - Success
			PlayerPrefs.SetString( "ultimaPalavraOculta", palavraOculta );
			SceneManager.LoadScene( "Lab1_salvo" );
		}
	}

	string pegaPalavraDoArquivo()
	{
		// pega uma palavra aleatória de um arquivo de palavras para ser carregada nesse jogo
		TextAsset textAsset = (TextAsset)Resources.Load( "palavras", typeof( TextAsset ) );
		string fileContent = textAsset.text;

		string[] palavras = fileContent.Split( ' ' );
		int nPalavraAleatoria = Random.Range( 0, palavras.Length );

		return palavras[nPalavraAleatoria];
	}
}
