using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;

public delegate void Callback();
public delegate void IntCallback(int number);
public delegate void ListIntCallback(List<int> number);


public class GameManager : MonoBehaviour {
    public GameObject frameManager;
    public int playersNumber;
    public int totalFrames;

    // player -> frame -> punteggio
    private List<List<List<int>>> gameScores;

    private int[] totScore;
    private int[] timesRedoubling;

    // Start is called before the first frame update
    void Start() {  
        totScore = new int[playersNumber];
        timesRedoubling = new int[playersNumber];

        gameScores = new List<List<List<int>>>();
        for (int i = 0; i < playersNumber; i++) {
            List<List<int>> point = new List<List<int>>();
            for(int j = 0; j < totalFrames; j++) {
                point.Add(new List<int>());
            }
            gameScores.Add(point);
        }
        StartGame();
    }

    private async void StartGame() {
        var bowlingScore = new BowlingScoreCalculator(playersNumber, totalFrames);
        ScoresUI.instance.SetNumberOfPlayers(playersNumber);
        
        for (int f = 0; f < totalFrames; f++)
        {
            Debug.Log("Frame: " + f);
            for (int p = 0; p < playersNumber; p++)
            {
                Debug.Log("Player: " + p);

                List<int> score = await frameManager.GetComponent<FrameManager>().RunFrame(f == totalFrames, p, f);
                //aspetto il punteggio del frame...

                Debug.Log("Frame: " + f + " Player: " + p + " -> " );
                
                // Questo è lo score dove per ogni tiro hai quanti birilli hai buttato esattamente in quel tiro
                int[] scoreT = new int[score.Count];

                // Creo l'array e rimetto apposto i punteggi in modo ordinato
                scoreT[0] = score[0];
                for (int i = 1; i < scoreT.Length; i++) {
                    scoreT[i] = score[i] - score[i-1]; 
                }

                bowlingScore.BowlFrame(new Frame( new List<int>(scoreT)));

                // Setto i punteggi nella lista dei punteggi e li inserisco 
                gameScores[p][f] = new List<int>(scoreT);

                int sum = 0;
                // Raddoppio del punteggio
                for (int i = 0; i < score.Count && timesRedoubling[p] > 0; i++, timesRedoubling[p]--) {
                    // raddoppio il tiro i
                    sum += scoreT[i];
                }

                if (score.Count == 1 && score[0] == 10) {
                    // Abbiamo fatto strike
                    timesRedoubling[p] += 2;   

                } else if (score.Count == 2 && score[1] == 10) {
                    // Abbiamo fatto spare
                    timesRedoubling[p] ++;

                }

                Debug.Log("moltiplicatore: "+timesRedoubling[p]);
                // Qua va fatta la somma.

                totScore[p] += scoreT.Sum() +sum;

                // Aggiorno graficamente
                if (timesRedoubling[p] > 0)
                {
                    ScoresUI.instance.setParzialScore(p, f, totScore[p].ToString());
                    ScoresUI.instance.SetTotScore(p, totScore[p].ToString());
                }

                
            }
        }
    }
    // Update is called once per frame
    void Update() {

    }

    public class Frame {
        private List<int> framePoint;

        public Frame (List<int> frame) {
            if (frame.Count > 2) throw new System.Exception("Error frame > 2");

            foreach (int i in frame) {
                if (i > 10) throw new System.Exception("Number of point > 10");
            }

            framePoint = frame;
        }

        public bool IsStrike() {
            return framePoint.Count == 1 && framePoint.Sum() == 10;
        }

        public bool IsSpare() {
            return framePoint.Count == 2 && framePoint.Sum() == 10;
        }

        public int FirstThrow () {
            return framePoint[0];
        }

        public int Total () {
            return framePoint.Sum();
        }
    }

    public class BowlingScoreCalculator {
        private int playersNumber;
        private int totalFrames;

        private Frame[][] _frames;
        private int[] scores;
        
        private int _currentFrame;

        private int _currentPlayer;

        private Frame LastFrame { get { return _frames[_currentPlayer][_currentFrame - 1]; } }

        private Frame TwoFramesAgo { get { return _frames[_currentPlayer][_currentFrame - 2]; } }

        public BowlingScoreCalculator (int playersNumber, int totalFrames) {
            this.playersNumber = playersNumber;
            this.totalFrames = totalFrames;
            this._frames = new Frame[playersNumber][]; 
            for (int i = 0; i < _frames.Length; i ++) {
                this._frames[i] = new Frame[totalFrames];
            }

            this.scores = new int[playersNumber];

        }

        public int Score () { 
            return scores[_currentPlayer];
        }

        public void BowlFrame(Frame frame)
        {
            AddMarkBonuses(frame);

            scores[_currentPlayer] += frame.Total();
            _frames[_currentPlayer++][_currentFrame] = frame;

            Debug.Log("Punteggio: : : : " + scores[_currentPlayer-1]);

            if (_currentPlayer >= playersNumber) {
                _currentPlayer = 0;
                _currentFrame++;
            }

        }

        private void AddMarkBonuses(Frame frame)
        {
            if (WereLastTwoFramesStrikes())
                scores[_currentPlayer] += frame.Total() + frame.FirstThrow();
            else if (WasLastFrameAStrike())
                scores[_currentPlayer] += frame.Total();
            else if (WasLastFrameASpare())
                scores[_currentPlayer] += frame.FirstThrow();

            
        }

        private bool WereLastTwoFramesStrikes()
        {
            return WasLastFrameAStrike() && _currentFrame > 1 && TwoFramesAgo.IsStrike();
        }

        private bool WasLastFrameAStrike()
        {
            return _currentFrame > 0 && LastFrame.IsStrike();
        }

        private bool WasLastFrameASpare()
        {
            return _currentFrame > 0 && LastFrame.IsSpare();
        }
    }

}


