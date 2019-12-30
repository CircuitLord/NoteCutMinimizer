using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;


namespace NoteCutMinimizer {
    
    
    
    public class NCMSettings {

        public static NCMJsonSettings config = new NCMJsonSettings();
        private static bool isLoaded = false;
        private static string configFilePath;
        private static List<Action> pendingActions = new List<Action>();

        
        
        
        
        
        public static void LoadSettingsJson(bool regenConfig = false) {
            
            
            
            configFilePath = Path.GetFullPath(Path.Combine(Application.dataPath, @"..\"));

            configFilePath = Path.Combine(configFilePath, "UserData", "NCMSettings.json");
            

            //If it doesn't exist, we need to gen a new one.
            if (regenConfig || !File.Exists(configFilePath)) {

                GenNewConfig();
                return;
            }

            try {
                config = JsonUtility.FromJson<NCMJsonSettings>(File.ReadAllText(configFilePath));
            } catch (Exception e) {
                Debug.LogError(e);
            }

            isLoaded = true;
            foreach (var pendingAction in pendingActions) {
                pendingAction();
            }
            pendingActions.Clear();
        }

        public static void OnLoad(Action action) {
            if (isLoaded) action();
            else {
                pendingActions.Add(action);
            }
        }

        public static void SaveSettingsJson() {
            File.WriteAllText(configFilePath, JsonUtility.ToJson(config, true));
        }

        

        private static void GenNewConfig() {

            //Debug.Log("Generating new configuration file...");

            NCMJsonSettings temp = new NCMJsonSettings();
            
            config = temp;
            isLoaded = true;

            if (File.Exists(configFilePath)) File.Delete(configFilePath);

            SaveSettingsJson();

        }


    }
	

	
	
	public class NCMJsonSettings {
		public bool enabled = true;

		public float forceMultiplier = 1.0f;
	}
    
    

    
    


}