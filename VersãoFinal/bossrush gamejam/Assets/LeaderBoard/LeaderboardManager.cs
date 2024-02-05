using UnityEngine;
using TMPro;

// NOTE: Make sure to include the following namespace wherever you want to access Leaderboard Creator methods
using Dan.Main;

namespace LeaderboardCreatorDemo
{
    public class LeaderboardManager : MonoBehaviour
    {
        public GameObject texto;
        [SerializeField] public TMP_Text[] _entryTextObjects;
        [SerializeField] public TMP_InputField _usernameInputField;

// Make changes to this section according to how you're storing the player's score:
// ------------------------------------------------------------
        [SerializeField] public GameController _exampleGame;
        
        public int Score => (int)_exampleGame.statisticSettings.timeToCompleteGame;
// ------------------------------------------------------------

        private void Start()
        {

            LoadEntries();
        }

        private void LoadEntries()
        {
            // Q: How do I reference my own leaderboard?
            // A: Leaderboards.<NameOfTheLeaderboard>
        
            Leaderboards.NoMoreAngels.GetEntries(entries =>
            {
                _entryTextObjects = new TMP_Text[entries.Length];

                for(int i = 0; i < _entryTextObjects.Length; i++)
                {
                    GameObject gObj = Instantiate(texto, new Vector2(this.transform.position.x, this.transform.position.y - i * 45), transform.rotation);
                    gObj.transform.SetParent(gameObject.transform);
                    _entryTextObjects[i] = gObj.GetComponent<TMP_Text>();
                }

                foreach (var t in _entryTextObjects)
                    t.text = "";
                var length = Mathf.Min(_entryTextObjects.Length, entries.Length);
                for (int i = 0; i < length; i++)
                {
                    var totalMinutes = (double)entries[i].Score / 60;
                    var minutes = (int)totalMinutes;
                    var seconds = (int)((totalMinutes - minutes) * 60);
                    //_entryTextObjects[i] = Instantiate(texto, new Vector2(this.transform.position.x, this.transform.position.y - i*5), transform.rotation).GetComponent<TMP_Text>();
                    _entryTextObjects[i].text = $"{entries[i].Rank}. {entries[i].Username} - {minutes:D2}:{seconds:D2}";
                    //_entryTextObjects.Length ++;

            }});
        }
        
        public void UploadEntry()
        {
            Leaderboards.NoMoreAngels.UploadNewEntry(_usernameInputField.text, Score, isSuccessful =>
            {
                if (isSuccessful)
                    LoadEntries();
            });
        }
    }
}