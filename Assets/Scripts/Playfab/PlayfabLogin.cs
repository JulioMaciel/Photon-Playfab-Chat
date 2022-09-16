using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Playfab
{
    public class PlayfabLogin : MonoBehaviour
    {
        [SerializeField] string username;
        
        #region Unity
        
        void Start()
        {
            if (string.IsNullOrEmpty(PlayFabSettings.TitleId)) 
                PlayFabSettings.TitleId = "9EBFD";
        }
        
        #endregion
        
        #region Private

        bool IsValidUsername()
        {
            return username.Length is >= 3 and <= 24;
        }
        
        void LoginWithCustomId()
        {
            Debug.Log($"Login to Playfab as {username}");
            var request = new LoginWithCustomIDRequest { CustomId = username, CreateAccount = true };
            PlayFabClientAPI.LoginWithCustomID(request, OnLoginCustomIdSuccess, OnFailure);
        }
        
        void UpdateDisplayName(string displayName)
        {
            Debug.Log($"Updating Playfab account's Display name to: {displayName}");
            var request = new UpdateUserTitleDisplayNameRequest { DisplayName = displayName };
            PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameSuccess, OnFailure);
        }
        
        #endregion
        #region  Public
        
        public void SetUsername(string userName)
        {
            username = userName;
            PlayerPrefs.SetString("USERNAME", username);
        }
        
        public void Login()
        {
            if (!IsValidUsername()) return;
        
            LoginWithCustomId();
        }
        
        #endregion
        #region Playfab Callbacks

        void OnLoginCustomIdSuccess(LoginResult result)
        {
            Debug.Log($"You have logged into Playfab using custom id {username}");
            UpdateDisplayName(username);
        }
        
        void OnDisplayNameSuccess(UpdateUserTitleDisplayNameResult result)
        {
            Debug.Log($"You have updated the displayname of the playfab account!");
            SceneManager.LoadScene("MainMenu");
        }
        
        void OnFailure(PlayFabError error)
        {
            Debug.Log($"There was an issue with your request: {error.GenerateErrorReport()}");
        }
        
        #endregion
    }
}
