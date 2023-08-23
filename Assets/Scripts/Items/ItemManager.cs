using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [field: SerializeReference]
    private List<IToolController> toolControllers;

    private IToolController activeToolController;

    private int toolControllerIndex = 0;

    private void Start() {

        foreach(IToolController toolController in toolControllers) {
            toolController.gameObject.SetActive(false);
        }

        activeToolController = toolControllers[toolControllerIndex];
        activeToolController.SetActive();
    }

    private void Update() {

        if(GameManager.GetInstance().IsPaused()) {
            return;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            activeToolController.Use(Direction.NORTH);   
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow)) {
            activeToolController.Use(Direction.SOUTH);   
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow)) {
            activeToolController.Use(Direction.WEST);   
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            activeToolController.Use(Direction.EAST);   
        }

        if(Input.GetKeyDown(KeyCode.E)) {
            SelectNextTool();
        }
        else if(Input.GetKeyDown(KeyCode.Q)) {
            SelectPreviousTool();
        }
    }

    private void SelectNextTool() {

        activeToolController.SetInactive();

        toolControllerIndex = (toolControllerIndex + 1) % toolControllers.Count;
        activeToolController = toolControllers[toolControllerIndex];

        activeToolController.SetActive();

    }

    private void SelectPreviousTool() {

        activeToolController.SetInactive();

        toolControllerIndex = (toolControllerIndex - 1 + toolControllers.Count) % toolControllers.Count;
        activeToolController = toolControllers[toolControllerIndex];

        activeToolController.SetActive();
    }
}
