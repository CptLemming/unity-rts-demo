﻿using UnityEngine;
using System.Collections;

using RTS;

public class PauseMenu : Menu {
    private Player player;

    protected override void Start() {
        base.Start();
        player = transform.root.GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Resume();
        }
	}

    protected override void SetButtons() {
        buttons = new string[] { "Resume", "Save Game", "Load Game", "Exit Game" };
    }

    protected override void HandleButton(string text) {
        base.HandleButton(text);
        switch (text) {
            case "Resume":
                Resume();
                break;
            case "Save Game":
                SaveGame();
                break;
            case "Load Game":
                LoadGame();
                break;
            case "Exit Game":
                ReturnToMainMenu();
                break;
        }
    }

    protected override void HideCurrentMenu() {
        base.HideCurrentMenu();
        GetComponent<PauseMenu>().enabled = false;
    }

    private void Resume() {
        Time.timeScale = 1.0f;
        GetComponent<PauseMenu>().enabled = false;
        if (player) {
            player.GetComponent<UserInput>().enabled = true;
        }
        Screen.showCursor = false;
        ResourceManager.MenuOpen = false;
    }

    private void SaveGame() {
        GetComponent<PauseMenu>().enabled = false;
        SaveMenu saveMenu = player.GetComponentInChildren<SaveMenu>();
        Debug.Log("PauseMenu: " + saveMenu);
        if (saveMenu) {
            saveMenu.enabled = true;
            saveMenu.Activate();
        }
    }

    private void ReturnToMainMenu() {
        ResourceManager.LevelName = "";
        Application.LoadLevel("MainMenu");
        Screen.showCursor = true;
    }
}
