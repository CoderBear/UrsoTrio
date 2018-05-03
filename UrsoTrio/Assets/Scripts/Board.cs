﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

	public int width;
	public int height;

	public int borderSize;

	public GameObject tilePrefab;
	public GameObject[] gamePiecePrefabs;

	public float swapTime = 0.5f;

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

	public void PlaceGamePiece(GamePiece gamePiece, int x, int y){
		if (gamePiece == null) {
			Debug.LogWarning ("Invalid GamePiece!");
			return;
		}

		gamePiece.transform.position = new Vector3 (x, y, 0);
		gamePiece.transform.rotation = Quaternion.identity;
		if (IsWithinBounds (x,y)) {
			m_allGamePieces [x, y] = gamePiece;
		}
		gamePiece.SetCoord (x, y);
	}

	bool IsWithinBounds(int x, int y) {
		return (x >= 0 && x < width && y >= 0 && y < height);
	}

	void FillRandom(){
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				GameObject randomPiece = Instantiate (GetRandomGamePiece (), Vector3.zero, Quaternion.identity) as GameObject;

				if (randomPiece != null) {
					randomPiece.GetComponent<GamePiece> ().Init (this);
					PlaceGamePiece (randomPiece.GetComponent<GamePiece> (), i, j);
					randomPiece.transform.parent = transform;
				}
			}
		}
	}
	#endregion

	#region Tile Methods
	public void ClickTile(Tile tile){
		if (m_clickedTile == null) {
			m_clickedTile = tile;
//			Debug.Log ("Clicked tile: " + tile.name);
		}
	}

	public void DragToTile(Tile tile)
	{
		if (m_clickedTile != null && IsNextTo (tile, m_clickedTile)) {
			m_targetTile = tile;
		}
	}

	public void ReleaseTile() {
		if (m_clickedTile != null && m_targetTile != null) {
			Debug.Log ("Clicked Tile: " + m_clickedTile.name + " | Target Tile: " + m_targetTile.name);
			SwitchTiles (m_clickedTile, m_targetTile);
			Debug.Log ("Clicked Tile: " + m_clickedTile.name + " | Target Tile: " + m_targetTile.name);
//			Debug.Log ("Tiles switched!");
		}

		m_clickedTile = null;
		m_targetTile = null;
	}

	void SwitchTiles(Tile clickedTile, Tile targetTile) {

		GamePiece targetPiece = m_allGamePieces [targetTile.xIndex, targetTile.yIndex];
		GamePiece clickedPiece = m_allGamePieces [clickedTile.xIndex, clickedTile.yIndex];

		clickedPiece.Move (targetTile.xIndex, targetPiece.yIndex, swapTime);
		targetPiece.Move (clickedTile.xIndex, clickedTile.yIndex, swapTime);
	}

	bool IsNextTo(Tile start, Tile end) {
		if(Mathf.Abs (start.xIndex - end.xIndex) == 1 && start.yIndex == end.yIndex) {
			return true;
		}

		if(Mathf.Abs (start.yIndex - end.yIndex) == 1 && start.xIndex == end.xIndex) {
			return true;
		}

		return false;
	}
	#endregion
}
