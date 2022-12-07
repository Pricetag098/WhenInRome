using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class Endless : MonoBehaviour
{
    public Spawner spawner;
    public CanvasGroup deathScreen;
    public TextMeshProUGUI score;
    public TextMeshProUGUI hsDisp;
    public int maxScores = 12;
    bool dead;

    HighScores scores;
    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            deathScreen.alpha = Mathf.Lerp(deathScreen.alpha,1,Time.unscaledDeltaTime);
        }
    }

    public void OnDeath()
    {
        dead = true;
        score.text = "your score <" + spawner.wave.ToString() + ">";
        hsDisp.text = scores.GiveText();
    }

    void SaveScore()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        
        scores.scores.Add(spawner.wave);
        scores.scores.Sort();
        if(scores.scores.Count > maxScores)
        {
            scores.scores.Remove(scores.scores.Count - 1);
        }

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, scores);
        file.Close();
    }
    void Load()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found");
            scores = new HighScores();
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        scores = (HighScores)bf.Deserialize(file);
        file.Close();
    }
    private void OnDestroy()
    {
        SaveScore();
    }


    [System.Serializable]
    class HighScores
    {
        public List<int> scores = new List<int>();
        
        public string GiveText()
        {
            string text = "";
            for(int i = 0; i < scores.Count; i++)
            {
                string temp = (i+1).ToString() + ". " + scores[i].ToString() + "\n";
                text += temp;
            }
            return text;
        }
    }
}
