using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Code;
using UnityEngine.SceneManagement;
using GameObject = UnityEngine.GameObject;

namespace Assets.Code
{
    public class LevelManager : MonoBehaviour
    {

        public static LevelManager Ctx;
        private MenuManager menus;

        public int level;
        public int enemyNumber;
        public bool paused = false;
        public GameObject enemyPrefab;

        private int enemiesSpawned = 0;
        private List<Vector3> spawnPoints;
        private float spawnTimer = 2;
        private float currTimer = 2;
        private System.Random random = new System.Random();

        // Start is called before the first frame update
        void Start()
        {
            menus = new MenuManager();
            Ctx = this;
            if (Ctx.level == 0)
            {
                menus.ShowMainMenu();
            }
            else
            {
                spawnPoints = new List<Vector3>();
                foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                    if (enemy)
                        spawnPoints.Add(enemy.transform.position);
            }
            
        }

        // Update is called once per frame
        void Update()
        {
            currTimer -= paused ? 0 : Time.deltaTime;
            if (currTimer <= 0 && enemiesSpawned < enemyNumber)
            {
                SpawnEnemy();
                currTimer = spawnTimer;
            }
        }

        void SpawnEnemy()
        {
            Instantiate(enemyPrefab, spawnPoints[random.Next(spawnPoints.Count)], transform.rotation);
        }

        public void Pause()
        {
            paused = true;
            menus.ShowPause();
        }

        public void UnPause()
        {
            paused = false;
            menus.HidePause();
        }

        public void GameOver()
        {
            SceneManager.LoadScene("Main");
        }

        public void NextLevel()
        {
            SceneManager.LoadScene("Level"+(level+1));
        }

        public void LoadLevel()
        {

        }

    }
}