using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
	private static MusicScript instance;

	// Start is called before the first frame update
	void Awake()
	{
		// mantém o objeto de música entre as cenas e previne ele de ser reinstanciado
		if ( instance != null )
		{
			Destroy( gameObject );
		}
		else
		{
			instance = this;
			DontDestroyOnLoad( transform.gameObject );
		}
	}
}
