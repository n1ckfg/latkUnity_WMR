using UnityEngine;
using System.Collections;

public class LatkInputVive : MonoBehaviour {

	public SteamVR_NewController steamControllerMain;
	public SteamVR_NewController steamControllerAlt;
	public LightningArtist latk;

	void Awake() {
		if (latk == null) latk = GetComponent<LightningArtist>();
	}

	void Update() {
		// 1. draw
		if ((steamControllerMain.triggerPressed && !steamControllerMain.menuPressed)) {// || Input.GetKeyDown(KeyCode.Space)) {
			latk.clicked = true;
		} else {
			latk.clicked = false;
		}

		if (steamControllerMain.triggerPressed && steamControllerMain.menuPressed) {
			latk.inputErase();
		} else if (!steamControllerMain.triggerPressed && steamControllerMain.menuPressed) {
			latk.inputPush();
			latk.inputColorPick();
		}	

		/*
		if (latk.mouseMode) {
			if (Input.GetMouseButtonDown(0)) {
				latk.clicked = true;
			} else if (Input.GetMouseButtonUp(0)) {
				latk.clicked = false;
			}
		}
		*/

		// 2. new frame
		if ((steamControllerMain.padDown && steamControllerMain.menuPressed && !steamControllerAlt.menuPressed)) {// || Input.GetKeyDown(KeyCode.F)) {
			latk.inputNewFrameAndCopy();
			Debug.Log("Ctl: New Frame Copy");
		} else if ((steamControllerMain.padDown && steamControllerMain.padDirCenter && !steamControllerMain.menuPressed && !steamControllerAlt.menuPressed)) {// || Input.GetKeyDown(KeyCode.G)) {
			latk.inputNewFrame();
			Debug.Log("Ctl: New Frame");
		}

		//if ((!steamControllerMain.padPressed && blockMainPadButton) || Input.GetKeyUp(KeyCode.F)) {
		//blockMainPadButton = false;
		//}

		// 3. play
		if (steamControllerAlt.padDown && steamControllerAlt.padDirCenter) {// || Input.GetKeyDown(KeyCode.P)) {
			latk.inputPlay();
			Debug.Log("Ctl: Play");
		}

		//if ((!steamControllerAlt.padPressed && blockAltPadButton) || Input.GetKeyUp(KeyCode.P)) {
		//blockAltPadButton = false;
		//}	

		// ~ ~ ~ ~ ~ ~ ~ ~ ~

		// 4. frame back
		if (steamControllerAlt.gripDown) {// || Input.GetKeyDown(KeyCode.LeftArrow)) {
			latk.inputFrameForward();
		}

		//if ((!steamControllerAlt.gripped && blockAltGripButton) || Input.GetKeyUp(KeyCode.LeftArrow)) {
		//blockAltGripButton = false;
		//}

		// 5. frame forward
		if (steamControllerMain.gripDown) {// || Input.GetKeyDown(KeyCode.RightArrow)) {
			latk.inputFrameBack();
		}

		//if ((!steamControllerMain.gripped && blockMainGripButton) || Input.GetKeyUp(KeyCode.RightArrow)) {
		//blockMainGripButton = false;
		//}

		// 6. show / hide all frames
		if ((!steamControllerMain.menuPressed && steamControllerAlt.menuDown)) {// || Input.GetKeyDown(KeyCode.UpArrow)) {
			//latk.inputShowFrames();
		//} else if (steamControllerAlt.menuUp || Input.GetKeyDown(KeyCode.DownArrow)) {
			//latk.inputHideFrames();

			latk.showOnionSkin = !latk.showOnionSkin;
			if (latk.showOnionSkin) {
				latk.inputShowFrames();
			} else {
				latk.inputHideFrames();
			}
		}

		// ~ ~ ~ ~ ~ ~ ~ ~ ~

		/*
		if (Input.GetKeyDown(KeyCode.Z)) { // reset all
			latk.resetAll(); 
		}

		if (Input.GetKeyDown(KeyCode.X)) { // reset
			latk.layerList[latk.currentLayer].frameList[latk.layerList[latk.currentLayer].currentFrame].reset(); 
		}

		if (Input.GetKeyDown(KeyCode.T)) { // random
			//resetAll();
			latk.testRandomStrokes();
		}

		if (Input.GetKeyDown(KeyCode.O)) { // scale
			latk.applyScaleAndOffset();
		}

		if (Input.GetKeyDown(KeyCode.R) && !latk.isReadingFile) {
			latk.armReadFile = true;
		}

		if (Input.GetKeyDown(KeyCode.S) && !latk.isWritingFile) {
			latk.armWriteFile = true;
		}
		*/

		if (steamControllerMain.menuPressed && steamControllerAlt.menuDown) {// || Input.GetKeyDown(KeyCode.K)) {
            //latk.inputNextLayer();
            latk.inputNewLayer();
        }

        if (steamControllerMain.menuPressed && steamControllerAlt.menuPressed && steamControllerMain.padDown) {// || Input.GetKeyDown(KeyCode.L)) {
			//latk.inputNewLayer();
		}

        // dir pad main
        if (steamControllerMain.padDown && !steamControllerMain.menuPressed && !steamControllerAlt.menuPressed) {
            if (steamControllerMain.padDirUp) {
                if (latk.brushMode == LightningArtist.BrushMode.ADD) {
                    latk.brushMode = LightningArtist.BrushMode.SURFACE;
                } else {
                    latk.brushMode = LightningArtist.BrushMode.ADD;
                }
            } else if (steamControllerMain.padDirDown) {
                //latk.useCollisions = !latk.useCollisions;
            }
        } else if (steamControllerMain.padPressed) {
            if (steamControllerMain.padDirLeft) latk.brushSizeInc();
            if (steamControllerMain.padDirRight) latk.brushSizeDec();
        }

        // dir pad alt
        if (steamControllerAlt.padDown && !steamControllerMain.menuPressed && !steamControllerAlt.menuPressed) {
            if (steamControllerAlt.padDirUp) {
                StartCoroutine(delayedUseCollisions(0.2f)); //latk.useCollisions = !latk.useCollisions;
            } else if (steamControllerAlt.padDirLeft) {
                latk.inputNextLayer();
            } else if (steamControllerAlt.padDirRight) {
                latk.inputPreviousLayer();
            }
        } else if (steamControllerAlt.padPressed) {
            //
        }

    }

    IEnumerator delayedUseCollisions(float delay) {
        yield return new WaitForSeconds(delay);
        if (steamControllerAlt.padDirUp) {
            latk.useCollisions = !latk.useCollisions;
        }
    }

}
