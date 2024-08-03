using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMenuController : MonoBehaviour
{
    public GameObject DebugMenuObject;
    private bool isDebugActive = false;

    private List<IDebugMenu> menus = new();

    private void Awake()
    {
        foreach (Transform t in DebugMenuObject.transform)
        {
            if (t.gameObject.TryGetComponent(out IDebugMenu menu))
            {
                menus.Add(menu);
            }
        }
    }

    private void Start()
    {
        DebugMenuObject.SetActive(false);
    }

    public void KillGarden()
    {
        Kill(DeathCondition.GardenDeath);
    }

    public void KillPlayer()
    {
        Kill(DeathCondition.PlayerDeath);
    }

    private void Kill(DeathCondition deathCondition)
    {
        isDebugActive = false;
        Time.timeScale = 1f;
        DebugMenuObject.SetActive(isDebugActive);

        Managers.Instance.Resolve<IGameOverMng>().DoGameOver(deathCondition);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            isDebugActive = !isDebugActive;
            DebugMenuObject.SetActive(isDebugActive);

            if (isDebugActive)
            {
                Time.timeScale = 0f;
                foreach (IDebugMenu menu in menus)
                {
                    menu.LoadMenu();
                }
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
}

public interface IDebugMenu
{
    public void LoadMenu();
}

