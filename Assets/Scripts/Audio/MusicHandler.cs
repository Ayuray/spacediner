using System;
using FMOD.Studio;
using UnityEngine;
using FMODUnity;

public class MusicHandler : MonoBehaviour
{
    [SerializeField] private EventReference music;
    private EventInstance _musicInstance;
    private PARAMETER_ID _intensityID;
    private void Start()
    {
        _musicInstance = RuntimeManager.CreateInstance(music);
        _musicInstance.getDescription(out var musicDescription);
        musicDescription.getParameterDescriptionByName("intensity", out var intensityDescription);
        _intensityID = intensityDescription.id;

    }

    private void OnIntensityChange(float intensity)
    {
        _musicInstance.setParameterByID(_intensityID, intensity);
    }
}
