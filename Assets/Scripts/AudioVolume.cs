using UnityEngine;
using UnityEngine.UI;


public class AudioVolume : MonoBehaviour
{
        [SerializeField] private string busPathSFX = "bus:/SFX";
        [SerializeField] private string busPathMU = "bus:/MU";
        private FMOD.Studio.Bus SFXBus, MUBus;
        [SerializeField] private Slider sliderSFX, sliderMU;

        void Start()
        {
            //sliderSFX = GetComponent<Slider>();
            //sliderMU = GetComponent<Slider>();
        
            // Locate the bus in the FMOD system
            SFXBus = FMODUnity.RuntimeManager.GetBus(busPathSFX);
            MUBus = FMODUnity.RuntimeManager.GetBus(busPathMU);

            // Set the slider's current value to match the bus volume on start
            SFXBus.getVolume(out float currentVolumeSFX);
            MUBus.getVolume(out float currentVolumeMU);
            sliderSFX.value = currentVolumeSFX;
            sliderMU.value = currentVolumeMU;

            // Add a listener to handle value changes
            sliderSFX.onValueChanged.AddListener(SetBusVolumeSFX);
            sliderMU.onValueChanged.AddListener(SetBusVolumeMU);
        }

        public void SetBusVolumeSFX(float volume)
        {
            SFXBus.setVolume(volume);
        }

        public void SetBusVolumeMU(float volume)
        {
            MUBus.setVolume(volume);
        }
}
