using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMenuController : MonoBehaviour
{
    public GameObject DebugMenuObject;
    private bool isDebugActive = false;
    private Canvas canvas;
    private List<IDebugMenu> menus = new();

    private void Awake()
    {
        foreach (Transform t in DebugMenuObject.transform)
        {
            if (t.gameObject.TryGetComponent(out IDebugMenu menu))
            {
                menu.SetDebugMenuController(this);
                menus.Add(menu);
            }
        }

        canvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        ToggleDebugMenu(false);
    }

    public void ToggleDebugMenu(bool active)
    {
        if (active)
        {
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            foreach (IDebugMenu menu in menus)
            {
                menu.LoadMenu();
            }

            Time.timeScale = 0f;
            isDebugActive = true;
        }
        else
        {
            Time.timeScale = 1f;
            canvas.renderMode = RenderMode.WorldSpace;
            DebugMenuObject.SetActive(false);
            DebugMenuObject.SetActive(true);
            isDebugActive = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            if (isDebugActive)
            {
                ToggleDebugMenu(false);
            }
            else
            {
                ToggleDebugMenu(true);
            }
        }
    }
}

public interface IDebugMenu
{
    public void SetDebugMenuController(DebugMenuController controller);
    public void LoadMenu();
}

