using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// Система сохранений игры
/// </summary>

public class SaveSystem
{
    private string filePath;
    private BinaryFormatter formatter;

    public SaveSystem()
    {
        filePath = Application.persistentDataPath + "/saves/GameSave.txt";
        InitBinaryFormatter();
    }

    /// <summary>
    /// Определение форматера и добавление в него новых сериализуемых типов данных
    /// </summary>

    private void InitBinaryFormatter()
    {
        formatter = new BinaryFormatter();
        SurrogateSelector selector = new SurrogateSelector();

        Vector3SeriaizationSurrogate v3Surrogate = new Vector3SeriaizationSurrogate();
        QuaternionSerializationSurrogate qSurrogate = new QuaternionSerializationSurrogate();

        selector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), v3Surrogate);
        selector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), qSurrogate);

        formatter.SurrogateSelector = selector;
    }

    /// <summary>
    /// Метод загрузки данных из файла в систему
    /// </summary>
    /// <param name="saveDataByDefault">Данные, использующиеся в случае отсутствия сохранения</param>
    /// <returns></returns>

    public object Load(object saveDataByDefault)
    {
        if (!File.Exists(filePath))
        {
            if (saveDataByDefault != null)
            {
                Save(saveDataByDefault);
                return saveDataByDefault;
            }
        }

        var file = File.Open(filePath, FileMode.Open);
        var savedData = formatter.Deserialize(file);
        file.Close();

        return savedData;
    }

    /// <summary>
    /// Метод, выгружающий данные из системы в файл сохранения
    /// </summary>
    /// <param name="saveData">Информация, которую надо сохранить</param>

    public void Save(object saveData)
    {
        var file = File.Create(filePath);
        formatter.Serialize(file, saveData);
        file.Close();
    }
}
