//THIS IS A GENERATED FROM TOOL. DO NOT EDIT IT!
//Open the tool and modify it instead.
//Tool: [MenuItem("TOPEBOX/Tools/EditorSwitchScene")]

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Topebox.Tool.SceneSwitcher
{
    [InitializeOnLoad]
    public static class GenerateCustomMenu
    {
        #region GENERAL CONTEXT MENU
        
        private static string start_scene_path = "Assets/Scenes/AppLaucher.unity";
        
		//============== Mimiland/Launcher - Open Scene Launcher =================
		private static string scene_0_path = "Assets/Scenes/AppLaucher.unity";
		[MenuItem("Mimiland/Launcher Scene &#1")]
		public static void quickMimiland_Launcher()
		{
			openSceneWithSaveConfirm(scene_0_path);
		}

		//============== Mimiland/World Map 1 - Mimiland/World Map 21 =================
		private static string scene_1_path = "Assets/Scenes/MainMenu.unity";
		[MenuItem("Mimiland/World Map 1 Scene &#2")]
		public static void quickMimiland_WorldMap1()
		{
			openSceneWithSaveConfirm(scene_1_path);
		}

		//============== Mimiland/World Map 2 - Open World Map 2 =================
		private static string scene_2_path = "Assets/Scenes/Gameplay.unity";
		[MenuItem("Mimiland/World Map 2 Scene &#3")]
		public static void quickMimiland_WorldMap2()
		{
			openSceneWithSaveConfirm(scene_2_path);
		}


    
        #endregion
    
        private static void openSceneWithSaveConfirm(string scenePath)
        {
            // Refresh first to cause compilation and include new assets
            AssetDatabase.Refresh();
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene(scenePath);
        }
    
        // pref IDs
        private const string LAST_OPENED_SCENE = "Game.LastOpenedScene";
        private const string PLAYED_USING_RUN_UTILS = "Game.PlayedUsingRunUtils";
    
        // bool states
        private static bool aboutToRun = false;
    
        [MenuItem("Template/Run &#R")]
        public static void Run()
        {
            SceneSetup[] setups = EditorSceneManager.GetSceneManagerSetup();
            if (setups.Length > 0)
            {
                EditorPrefs.SetString(LAST_OPENED_SCENE, setups[0].path);
            }
    
            EditorPrefs.SetBool(PLAYED_USING_RUN_UTILS, true);
            aboutToRun = true;
    
            openSceneWithSaveConfirm(start_scene_path);
    
            EditorApplication.isPlaying = true;
        }
    
        private static void LoadLastOpenedScene()
        {
            if (EditorApplication.isPlaying || EditorApplication.isCompiling)
            {
                // changed to playing or compiling
                // no need to do anything
                return;
            }
    
            if (!EditorPrefs.GetBool(PLAYED_USING_RUN_UTILS))
            {
                // this means that normal play mode might have been used
                return;
            }
    
            // We added this check because this method is still invoked while EditorApplication.isPlaying is false
            // We only load the last opened scene when the aboutToRun flag is "consumed"
            if (aboutToRun)
            {
                aboutToRun = false;
                return;
            }
    
            // at this point, the scene has stopped playing
            // so we load the last opened scene
            string lastScene = EditorPrefs.GetString(LAST_OPENED_SCENE);
            if (!string.IsNullOrEmpty(lastScene))
            {
                EditorSceneManager.OpenScene(lastScene);
            }
    
            EditorPrefs.SetBool(PLAYED_USING_RUN_UTILS, false); // reset flag
        }
    
        static GenerateCustomMenu()
        {
            EditorApplication.playmodeStateChanged += LoadLastOpenedScene;
        }
    }
}
#endif
//END GENERATED CODE