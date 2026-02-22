using System;
using FMOD.Studio;
using UnityEngine;
using FMODUnity;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class MusicHandler : MonoBehaviour
{
    [SerializeField] private int maxCustomers = 3;
    [SerializeField] private EventReference music;
    private EventInstance _musicInstance;
    private PARAMETER_ID _intensityID, _gameplayID;
    

    private void OnEnable()
    {
        SceneNavigation.OnGameStart += OnGameplayChange;
    }

    private void OnDisable()
    {
        SceneNavigation.OnGameStart -= OnGameplayChange;
    }

    private void Start()
    {
        EventDescription eventDesc = RuntimeManager.GetEventDescription("event:/MU/music");
        eventDesc.getInstanceCount(out int count);
        if (count == 0)
        {
            _musicInstance = RuntimeManager.CreateInstance(music);
            _musicInstance.getDescription(out var musicDescription);
            musicDescription.getParameterDescriptionByName("intensity", out var intensityDescription);
            musicDescription.getParameterDescriptionByName("gameplay", out var gameplayDescription);
            _intensityID = intensityDescription.id;
            _gameplayID = gameplayDescription.id;
            _musicInstance.start();
        }
    }

    private void CalculateIntensity(int amountOfCustomers)
    {
     int scalar = maxCustomers/3;
     int intensity = Mathf.CeilToInt(amountOfCustomers / scalar);
     _musicInstance.setParameterByID(_intensityID, intensity);
    }
    
    private void OnGameplayChange(int gameplay)
    {
        Debug.Log("OnGameplayChange");
        _musicInstance.setParameterByID(_gameplayID, gameplay);
    }

    private void AllMusicStop()
    {
        _musicInstance.getPlaybackState(out var musicState);
        if (musicState == PLAYBACK_STATE.PLAYING || musicState == PLAYBACK_STATE.STARTING)
            _musicInstance.stop(STOP_MODE.ALLOWFADEOUT);
    }
}
