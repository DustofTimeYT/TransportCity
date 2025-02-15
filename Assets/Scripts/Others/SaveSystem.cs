using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// ������� ���������� ����
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
    /// ����������� ��������� � ���������� � ���� ����� ������������� ����� ������
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
    /// ����� �������� ������ �� ����� � �������
    /// </summary>
    /// <param name="saveDataByDefault">������, �������������� � ������ ���������� ����������</param>
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
    /// �����, ����������� ������ �� ������� � ���� ����������
    /// </summary>
    /// <param name="saveData">����������, ������� ���� ���������</param>

    public void Save(object saveData)
    {
        var file = File.Create(filePath);
        formatter.Serialize(file, saveData);
        file.Close();
    }
}
