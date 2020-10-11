using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Behavioral.Memento
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Memento Pattern!");

            Settings settings = new Settings()
            {
                ConnectionString = "SQL",
                Config = "V1 Config",
                Enviroment = "Development",
                LastModified = DateTime.Now
            };

            Console.WriteLine("Init: \n" + settings);

            // Caretaker
            var settingMemory = new SettingMemory();
            settingMemory.SettingMemento = settings.CreateMemento();
            
            settings.LastModified = DateTime.Now.AddMinutes(1);
            settings.Config = "V2 Config";
            settings.Enviroment = "Production";

            Console.WriteLine("Updated setting values: \n" + settings);

            settings.Backup(settingMemory.SettingMemento);

            Console.WriteLine(settings);
        }
    }

    #region .Net Optimized

    /// <summary>
    /// The 'Originator' class
    /// </summary>
    [Serializable]
    internal class Settings
    {
        public string ConnectionString { get; set; }
        public string Config { get; set; }
        public string Enviroment { get; set; }
        public DateTime LastModified { get; set; }

        public SettingMemento CreateMemento()
        {
            Console.WriteLine("\nSaving state --\n");

            var settingMemento = new SettingMemento();
            return settingMemento.Serialize(this);
        }

        public void Backup(SettingMemento memento)
        {
            Console.WriteLine("\nRestoring state --\n");

            var previousState = (Settings)memento.Deserialize();

            this.ConnectionString = previousState.ConnectionString;
            this.Config = previousState.Config;
            this.Enviroment = previousState.Enviroment;
            this.LastModified = previousState.LastModified;
        }

        public override string ToString()
        {
            return $"{ConnectionString} \n{Config} \n{Enviroment} \n{LastModified}";
        }
    }

    /// <summary>
    /// The 'Memento' class
    /// </summary>
    internal class SettingMemento
    {
        MemoryStream stream = new MemoryStream();
        BinaryFormatter formatter = new BinaryFormatter();

        public SettingMemento Serialize(object o)
        {
            formatter.Serialize(stream, o);
            return this;
        }

        public object Deserialize()
        {
            stream.Seek(0, SeekOrigin.Begin);
            object o = formatter.Deserialize(stream);
            stream.Close();

            return o;
        }
    }

    internal class SettingMemory
    {
        public SettingMemento SettingMemento { get; set; }
    }
    #endregion
}
