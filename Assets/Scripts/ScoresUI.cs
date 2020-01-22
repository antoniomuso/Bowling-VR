using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScoresUI : MonoBehaviour
{
    //name of the players
    public string PlayerName1;
    public string PlayerName2;
    //reference of the Players scores
    public GameObject[] Players;

    //colors of the frame
    private Color c_OffBackFrame;
    private Color c_OnBackFrame;
    private Color c_OffNumberFrame;
    private Color c_OnNumberFrame;

    //references to the head backframe
    public Image[] IndicesBackFrame;

    //the display names of the players
    public Text[] DisplayName;

    //the text of the total scores of the players
    public Text[] TotScore; 
    private int currentFrame;   //indicates the current frame selected in the head
    private int totFrames;  //total number of the frames
    public Dictionary<int, Dictionary<int, Text[]>> score;

    // Start is called before the first frame update
#region Singleton
private static ScoresUI _instance;
public static ScoresUI instance => _instance;
    private void Awake()
    {
        SetFrameColors();
        if (_instance != null) {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
    }

#endregion


    void Start()
    {
        InitializeScore();
        SwitchOnFrame(currentFrame);
        ResetScore();
        //SetNumberOfPlayers(1);
        //SetName(0, PlayerName1);
        //SetName(1, PlayerName2);
    }
    
    public void SwitchOnFrame(int indexFrame)
    {
            IndicesBackFrame[indexFrame].color = c_OnBackFrame;
            IndicesBackFrame[indexFrame].GetComponentInChildren<Text>().color = c_OnNumberFrame;
    }

    public void SwitchOffFrame(int indexFrame)
    {
        IndicesBackFrame[indexFrame].color = c_OffBackFrame;
        IndicesBackFrame[indexFrame].GetComponentInChildren<Text>().color = c_OffNumberFrame;
    }

    /*
     * Method used to retrieve the Image references of the frames of a player
     * */
    private Image[] GetFrames(GameObject Player)
    {
        List<Image> childrenOfPlayer = new List<Image>(Player.GetComponentsInChildren<Image>());
        List<Image> framesResult = new List<Image>();
        foreach (Image c in childrenOfPlayer)
        {
            if(c.name.StartsWith("BackFrame"))
            {
                framesResult.Add(c);
            }
        }
        return framesResult.ToArray();
    }


    /**
     * Method used to set the Color of background and text of the frames
     * */
    private void SetFrameColors()
    {
        c_OffBackFrame = Color.black;
        c_OffNumberFrame = Color.white;
        c_OnBackFrame = Color.red;
        c_OnNumberFrame = Color.yellow;
    }


    /**
     * Method used to retrieve the Text references of throws and partial score
     * */
    private Text[] GetThrowsAndPartialScore(Image Frame)
    {
        List<Image> childrenOfFrame = new List<Image>(Frame.GetComponentsInChildren<Image>());
        List<Text> result = new List<Text>();
        foreach( Image c in childrenOfFrame)
        {
            if(c.name.Equals("BackThrows"))
            {
                Image[] singleBackThrows = c.GetComponentsInChildren<Image>();
                
                foreach(Image sb in singleBackThrows)
                {
                    if(sb.name.StartsWith("BackThrow") && !sb.Equals(c))
                    {
                        //Debug.Log(sb.name);
                        result.Add(sb.GetComponentInChildren<Text>());
                    }
                }
            }
            else if(c.name.Equals("BackPartialScore"))
            {
                Text partialScore = c.GetComponentInChildren<Text>();
                result.Add(partialScore);
            }
        }
        return result.ToArray();
    }


    /**
     * Method used to retrieve all the references of the Text scores
     * */
    private void InitializeScore()
    {
        score = new Dictionary<int, Dictionary<int, Text[]>>();
        currentFrame = 0;

        //initialize the score dictionary for each player
        for(int p = 0; p < Players.Length; p++)
        {
            score[p] = new Dictionary<int, Text[]>();
            Image[] frames = GetFrames(Players[p]);
            totFrames = frames.Length;
            //for each frame, retrieve the references of Text Throws and PartialScore
            for (int f=0; f < totFrames; f++)
            {
                score[p][f] = GetThrowsAndPartialScore(frames[f]);
            }
        }
    }

    /**
     * Method used to clean the scoreboard display, placing all emptystrings
     * and 0 as totScore.
     * */
    public void ResetScore ()
    {
        currentFrame = 0;

        //loop to reset all the frame scores
        for (int p = 0; p < Players.Length; p++)
        {
            for (int f = 0; f < totFrames; f++)
            {
                //for the last frame we have 3 single throws +1 partialScore
                //otherwise 2 + 1 partialScore
                if (f == totFrames - 1)
                {
                    for (int s = 0; s < 3; s++)
                        SetScore(p, f, s, "");
                }
                else
                {
                    for (int s = 0; s < 2; s++)
                        SetScore(p, f, s, "");
                }
                setParzialScore(p,f,"");
            }

            //reset also the tot score
            SetTotScore(p, "0");
        }
        
    }
    
    /**
     * Method used to set the score of a player indicated by index, by the index of the frame,
     * the part of a frame:
     *  - when IndexFrame has a value < 9  --> partOfFrame can be 0,1 for throws and 2 for partial score;
     *  - when IndexFrame has value == 10 --> partOfFrame can be 0,1,2 for throws and 3 for partial score;
     * The string score indicates the character to put inside the scoreboard display.
     * */
    public void SetScore(int indexPlayer, int IndexFrame, int partOfFrame, string score)
    {
        if ( this.score[indexPlayer][IndexFrame].Length-1 == partOfFrame) {
            throw new Exception();
        }

        this.score[indexPlayer][IndexFrame][partOfFrame].text = score;
    }

    public void setParzialScore (int indexPlayer, int IndexFrame, string score) {
        int len = this.score[indexPlayer][IndexFrame].Length;
        this.score[indexPlayer][IndexFrame][len-1].text = score;
    }

    /*
     * Method used to deactivate the display ( name + score)  of player2 when
     * the modality chosen is single player.
     * */
    public void SetNumberOfPlayers(int numberOfPlayers)
    {
        if (numberOfPlayers > Players.Length || numberOfPlayers <= 0) throw new Exception("Max number of players is " + Players.Length);

        for (int i = 0; i < Players.Length; i++) {
            if (i < numberOfPlayers) Players[i].SetActive(true);
            else Players[i].SetActive(false);
        }

    }

    /**
     * Method used to set the name of a player 
     * */
    public void SetName(int indexDisplayName, string playerName)
    {
        DisplayName[indexDisplayName].text = playerName;
    }

    /*
     * Method used to set the tot score of a player
     * */
    public void SetTotScore(int indexPlayer, string totScore)
    {
        TotScore[indexPlayer].text = totScore;
    }




}
