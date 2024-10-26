using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraManager : SingletonMono<CameraManager>
{
   [SerializeField] private Camera mainCamera;
   private PostProcessVolume postProcessVolume;

   private void Start()
   {
      GetComponent();
      DisableBlurEffect();
   }

   private void GetComponent()
   {
      postProcessVolume = mainCamera.GetComponent<PostProcessVolume>();
   }

   public void EnableBlurEffect()
   {
      OnOffBlurEffect(true);
   }

   public void DisableBlurEffect()
   {
      OnOffBlurEffect(false);
   }

   private void OnOffBlurEffect(bool isOn)
   {
      postProcessVolume.enabled = isOn;
   }
}
