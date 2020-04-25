using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class GameController: MonoBehaviour
{
    public delegate void Handler();
    public static event Handler HideGame;

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private GameObject mascot;
    [SerializeField]
    private SceneAssembler sceneAssembler;
    [SerializeField]
    private ColumnCollectionMotion collectionMotion;
    [SerializeField]
    private GameObject bestScoreText;
    [SerializeField]
    private GameObject restartBtn;

    private int score = 0;
    public GameObject columnClollecion;
    public List<GameObject> columnsPairs;
    private int bestScore;
    public float Score
    {
        get { return Score; }
    }

    public void UpdateScore()
    {
        score++;     
    }

    public void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    public void CharacterCollided()
    {
        bestScoreText.SetActive(true);
        restartBtn.SetActive(true);
        bestScore = LoadGame();
        if(score >= bestScore) 
        {
            Save(score);
            bestScoreText.GetComponent<Text>().text = "Рекорд: " + score.ToString();
  
        }

        else
        {
            bestScoreText.GetComponent<Text>().text = "Рекорд: " + bestScore.ToString();
        
        }
        score = 0;
        Time.timeScale = 0;
    }

    public void Restart()
    {
        mascot.GetComponent<Rigidbody>().Sleep();
        collectionMotion.isCoroutineStop = false;
        sceneAssembler.ReAssembleScene();
        collectionMotion.StopAllCoroutines();
        mascot.transform.localPosition = new Vector3(mascot.transform.localPosition.x, -0.025f, mascot.transform.localPosition.z);
        Time.timeScale = 1;
        mascot.GetComponent<Rigidbody>().Sleep();
        score = 0;
        collectionMotion.rotAcum = 0;
        UpdateScoreText();
        restartBtn.SetActive(false);
        bestScoreText.SetActive(false);
    }

    public void CloseScene()
    {
        collectionMotion.isCoroutineStop = true;
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
        HideGame();
    }


    private void Save(int scoreToSave)
    {

        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/flappyScore.fscr";
        FileStream stream = new FileStream(path, FileMode.Create);
        bf.Serialize(stream, scoreToSave);
        stream.Close();

    }

    private int LoadGame()
    {
        string path = Application.persistentDataPath + "/flappyScore.fscr";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            int loadedScore = (int) formatter.Deserialize(stream);
            stream.Close();
            return loadedScore;
           
        }
        else
        {
            Debug.LogError("Saved file not found in " + path);
            return 0;
        }
    }

    private void OnEnable()
    {
        string path = Application.persistentDataPath + "/flappyScore.fscr";
        if (!File.Exists(path))
        {
            bestScore = 0;
            Save(0);
            return;

        }

        LoadGame();
    }

}
