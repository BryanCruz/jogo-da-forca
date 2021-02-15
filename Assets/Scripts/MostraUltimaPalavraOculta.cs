using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostraUltimaPalavraOculta : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		// mostra a palavra descoberta na tela de Game Over - Success
		Text componentePalavraOculta = GetComponent<Text>();
		componentePalavraOculta.text = PlayerPrefs.GetString( "ultimaPalavraOculta" );
	}
}
