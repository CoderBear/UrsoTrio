﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

	public int width;
	public int height;

	public int borderSize;

	public GameObject tilePrefab;
	public GameObject[] gamePiecePrefabs;

	Tile[,] m_allTiles;
	GamePiece[,] m_allGamePieces;

	Tile m_clickedTile;
	Tile m_targetTile;

	// Use this for initialization
	void Start () {
		m_allTiles = new Tile[width, height];
		m_allGamePieces = new GamePiece[width, height];
		SetupTiles ();
		SetupCamera ();
		FillRandom ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SetupTiles()
	{
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				GameObject tile = Instantiate (tilePrefab, new Vector3 (i, j, 0), Quaternion.identity) as GameObject;
				tile.name = "Title (" + i + "," + i + ")";

				m_allTiles [i, j] = tile.GetComponent<Tile> ();
				tile.transform.parent = transform;
				m_allTiles [i, j].Init (i, j, this);
			}
		}
	}

	void SetupCamera()
	{
		Camera.main.transform.position = new Vector3 ((float)(width - 1) / 2f, (float)(height - 1) / 2f, -10f);

		float aspectRatio = (float)Screen.width / (float)Screen.height;
		float verticalSize = (float)height / 2f + (float)borderSize;
		float horizontalSize = (float)width / 2f + (float)borderSize / aspectRatio;

		Camera.main.orthographicSize = (verticalSize > horizontalSize) ? verticalSize : horizontalSize;
	}

	#region GamePiece Methods
	GameObject GetRandomGamePiece() {
		int randomIdx = Random.Range (0, gamePiecePrefabs.Length);

		if (gamePiecePrefabs [randomIdx] == null) {
			Debug.LogWarning ("BOARD: " + randomIdx + " does not contain a valid GamePiece prefab!");
		}

		return gamePiecePrefabs [randomIdx];
	}

	void PlaceGamePiece(GamePiece gamePiece, int x, int y){
		if (gamePiece == null) {
			Debug.LogWarning ("Invalid GamePiece!");
			return;
		}

		gamePiece.transform.position = new Vector3 (x, y, 0);
		gamePiece.transform.rotation = Quaternion.identity;
		gamePiece.SetCoord (x, y);
	}

	void FillRandom(){
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				GameObject randomPiece = Instantiate (GetRandomGamePiece (), Vector3.zero, Quaternion.identity) as GameObject;

				if (randomPiece != null) {
					PlaceGamePiece (randomPiece.GetComponent<GamePiece> (), i, j);
				}
			}
		}
	}
	#endregion

	#region Tile Methods
	public void ClickTile(Tile tile){
		if (m_clickedTile == null) {
			m_clickedTile = tile;
			Debug.Log ("Clicked tile: " + m_clickedTile);
		}
	}

	public void DragToTile(Tile tile)
	{
		if (m_clickedTile != null) {
			m_targetTile = tile;
		}
	}

	public void ReleaseTile() {
		if (m_clickedTile != null && m_targetTile != null) {
			SwitchTile (m_clickedTile, m_targetTile);
		}
	}

	void SwitchTile(Tile clickedTile, Tile targetTile) {
		m_clickedTile = null;
		m_targetTile = null;

	}
	#endregion
}
