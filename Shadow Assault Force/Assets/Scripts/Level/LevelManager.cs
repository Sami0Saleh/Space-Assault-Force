using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] _levels;
    [SerializeField] PlayerController _player;
    


    public void StartLevel()
    {
        if (_player.Level == _levels.Length)
        {
            Time.timeScale = 0f;
            SceneManager.LoadScene(0);
        }
        else
        {
            _player.transform.position = new Vector3(1.26800001f, -0.451000005f, -2.80900002f);
            _levels[_player.Level].SetActive(true);
            _levels[_player.Level - 1].SetActive(false);
        }
    }
}
