using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code
{
    public partial class MenuManager : MonoScript
    {
        public static Transform Canvas { get; private set; }


        private WelcomeMenu _welcome;
        private PauseMenu _pause;
        private InstructionMenu _instruction;

        public bool InWelcome
        {
            get { return _welcome != null && _welcome.Showing; }
        }

        public MenuManager()
        {
            Canvas = GameObject.Find("Canvas").transform;
        }


        public void ShowMainMenu()
        {
            _welcome = new WelcomeMenu();
            _welcome.Show();
        }

        public void ShowInstructions()
        {
            _instruction = new InstructionMenu();
            _instruction.Show();
            GameObject start = GameObject.Find("New");
            start.SetActive(false);
            GameObject load = GameObject.Find("Load");
            load.SetActive(false);
            GameObject ins = GameObject.Find("Instructions");
            ins.SetActive(false);
            _welcome.Hide();
            Button back = GameObject.FindGameObjectWithTag("Back").GetComponent<Button>();
            /*if (button.name == "Back")
            {
                button.onClick.AddListener(LevelManager.Ctx.Menu);
            }*/
            back.onClick.AddListener(LevelManager.Ctx.Menu);

        }

        public void HideInstructions()
        {
            _instruction.Hide();
        }

        public void ShowPause()
        {
            _pause = new PauseMenu();
            _pause.Show();
        }

        public void HidePause()
        {
            _pause.Hide();
        }

        private abstract class Menu
        {
            protected GameObject thisGO;

            public bool Showing { get; private set; }

            public virtual void Show()
            {
                Showing = true;
                thisGO.SetActive(true);
            }

            public virtual void Hide()
            {
                Showing = false;
                GameObject.Destroy(thisGO);
            }
        }

        private class WelcomeMenu : Menu
        {
            public WelcomeMenu()
            {
                Object menuPrefab = Resources.Load("Menus/Welcome");
                thisGO = (GameObject) Instantiate(menuPrefab);
                thisGO.transform.SetParent(Canvas, false);
                InitializeButtons();
            }

            private void InitializeButtons()
            {
                Button[] buttons = thisGO.GetComponentsInChildren<Button>();
                foreach (Button button in buttons)
                {
                    if (button.name == "New")
                    {
                        button.onClick.AddListener(LevelManager.Ctx.NextLevel);
                    }
                    else if (button.name == "Load")
                    {
                        button.onClick.AddListener(LevelManager.Ctx.LoadLevel);
                    }
                    else if (button.name == "Instructions")
                    {
                        button.onClick.AddListener(LevelManager.Ctx.Instructions);
                    }
                }
            }
        }

        private class PauseMenu : Menu
        {
            public PauseMenu()
            {
                Object menuPrefab = Resources.Load("Menus/Pause");
                thisGO = (GameObject) Instantiate(menuPrefab);
                thisGO.transform.SetParent(Canvas, false);
            }
        }

        private class InstructionMenu : Menu
        {
            public InstructionMenu()
            {
                Object menuPrefab = Resources.Load("Menus/Instructions");
                thisGO = (GameObject)Instantiate(menuPrefab);
                thisGO.transform.SetParent(Canvas, false);
            }
        }
    }
}