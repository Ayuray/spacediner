using System;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class SFX_CharMove : MonoBehaviour
{
   [SerializeField] private EventReference charMove;
   private EventInstance _charMoveInstance;

   private void Start()
   {
      _charMoveInstance = RuntimeManager.CreateInstance(charMove);
   }

   private void Update()
   {
      bool charMove = false;
      if (charMove)
         _charMoveInstance.stop(STOP_MODE.ALLOWFADEOUT);
      else
         _charMoveInstance.start();
   }
}
