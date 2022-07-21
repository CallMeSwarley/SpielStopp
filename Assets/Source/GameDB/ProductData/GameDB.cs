using System;
using System.Linq;
using GameDBLibrary;

/**************************************************************************************
*
*
*                     THIS IS A GENERATED FILE! DO NOT EDIT!
*
*
**************************************************************************************/

namespace GameDBProductData
{
    public class GameDB : GameDBBase
    {
        public GameDataTable GameDataTable
        {
            get { return (GameDataTable)Tables[GameDataSchema.TableName]; }
        }


        public GameDB(string name) : base(name, "ProductData") {

			Tables.Add(GameDataSchema.TableName, new GameDataTable((string key) => { return new GameData(key, this); }));
        
#if UNITY_EDITOR
            System.Reflection.Assembly editorAssembly = System.AppDomain.CurrentDomain.GetAssemblies().First(a => a.FullName.StartsWith("GameDBEditorLibrary"));
            var gameDBEditorType = editorAssembly.GetTypes().FirstOrDefault(t => t.Namespace == "GameDBEditorLibrary" && t.FullName.EndsWith(".GameDBEditor"));
            var method = gameDBEditorType.GetMethod("AddRuntimeDB", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            method.Invoke(obj: null, parameters: new [] { this });
#endif
			this.Logger = new UnityLogger();
		}

		
        public Exception Load(string path, bool notify = true) {
            var gameDBResource = UnityEngine.Resources.Load(path) as UnityEngine.TextAsset;

		    if (gameDBResource == null)
		    {
		        return new ArgumentException(string.Format("Failed to load gameDB {0} at path: {1}", Name, path));
		    }

            return Import(gameDBResource.text);
        }

		public class UnityLogger : GameDBLibrary.Logger
		{
			public override void Log(string message)
			{
				UnityEngine.Debug.Log(message);
			}

			public override void LogError(string message)
			{
				UnityEngine.Debug.LogError(message);
			}

			public override void LogException(Exception e)
			{
				UnityEngine.Debug.LogException(e);
			}
        }
    }
}
