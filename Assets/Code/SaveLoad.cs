using System.IO;
using System.Xml.Serialization;
using Assets.Code;
using UnityEngine;

namespace Assets.Code
{
    public class SaveLoad
    {

        private const string PathExt = "/save";
        private readonly string _path;

        public SaveLoad()
        {
            _path = Application.persistentDataPath + PathExt;

        }

        public void Save()
        {
            using (var file = File.Create(_path))
            {
                var data = new SaveData();
                var writer = new XmlSerializer(typeof(SaveData));
                writer.Serialize(file, data);
                file.Close();
            }
        }

        public void Load()
        {
            if (!File.Exists(_path))
            {
                return;
            }

            using (var file = File.Open(_path, FileMode.Open))
            {
                var reader = new XmlSerializer(typeof(SaveData));
                var data = reader.Deserialize(file) as SaveData;
                file.Close();
                LevelManager.Ctx.LoadLevelByNumber(data.level);
            }
        }

        public class SaveData
        {
            public int level;

            public SaveData()
            {
                level = LevelManager.Ctx.level;
            }
        }
    }
}