using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MaganeButton : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		PlayerPrefs.SetInt( "score", 0 );
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void StartMundoGame()
	{
		// reinicia o jogo
		SceneManager.LoadScene( "Lab1" );
	}

	public void VoltaProInicioButton()
	{
		// volta para a tela inicial
		SceneManager.LoadScene( "Lab1_start" );
	}

	public void SairDoJogo()
	{
		// sai do jogo
		Application.Quit();
	}
}
