using UnityEngine;
using System.Collections;
using Assets.UnityLogic;

public class ResourceLoader : MonoBehaviour {

    public string TexturePath;

	// Use this for initialization
	void Start () {
        TextureDictionary.LoadTexturesFrom(TexturePath);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
