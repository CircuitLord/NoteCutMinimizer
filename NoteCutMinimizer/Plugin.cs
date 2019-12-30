using Harmony;
using IPA;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NoteCutMinimizer
{
    public class Plugin : IBeatSaberPlugin {


        private HarmonyInstance _harmony;
        
        public string Name => "NoteCutMinimizer";
        public string Version => "1.1.0";



        public static Plugin Instance { get; private set; }

        
        public void OnApplicationStart() {

            Instance = this;
            
            NCMSettings.LoadSettingsJson();
            
            _harmony = HarmonyInstance.Create("com.circuitlord.beatsaber.notecutminimizer");
            try {
                _harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                Log($"Failed to apply harmony patches! {ex}");
            }

            Log("NoteCutMinimizer has started successfully!");
            
            
            
            

        }


        public void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
        {

            

        }

        public void OnActiveSceneChanged(Scene arg0, Scene scene) {

        }



        public void OnApplicationQuit()
        {
            NCMSettings.SaveSettingsJson();
        }


        public void OnUpdate()
        {

            
            


        }

        public void OnFixedUpdate()
        {
        }

        public static void Log(string message)
        {
            Console.WriteLine("[{0}] {1}", "NoteCutMinimizer", message);
        }

        public void OnSceneUnloaded(Scene scene)
        {
        }
    }
}
