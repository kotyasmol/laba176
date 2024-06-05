using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MessagePack;
using PersonLibrary;

public static class Serializer
{
    public static async Task SaveAsJsonAsync(string filePath, List<Person> persons)
    {
        try
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await JsonSerializer.SerializeAsync(stream, persons);
            }
        }
        catch (Exception ex)
        {
            throw new IOException("Failed to save JSON file.", ex);
        }
    }

    public static async Task<List<Person>> LoadFromJsonAsync(string filePath)
    {
        try
        {
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                return await JsonSerializer.DeserializeAsync<List<Person>>(stream);
            }
        }
        catch (Exception ex)
        {
            throw new IOException("Failed to load JSON file.", ex);
        }
    }

    public static async Task SaveAsXmlAsync(string filePath, List<Person> persons)
    {
        try
        {
            var serializer = new XmlSerializer(typeof(List<Person>));
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await Task.Run(() => serializer.Serialize(stream, persons));
            }
        }
        catch (Exception ex)
        {
            throw new IOException("Failed to save XML file.", ex);
        }
    }

    public static async Task<List<Person>> LoadFromXmlAsync(string filePath)
    {
        try
        {
            var serializer = new XmlSerializer(typeof(List<Person>));
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                return await Task.Run(() => (List<Person>)serializer.Deserialize(stream));
            }
        }
        catch (Exception ex)
        {
            throw new IOException("Failed to load XML file.", ex);
        }
    }

    public static async Task SaveAsBinaryAsync(string filePath, List<Person> persons)
    {
        try
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await MessagePackSerializer.SerializeAsync(stream, persons);
            }
        }
        catch (Exception ex)
        {
            throw new IOException("Failed to save binary file.", ex);
        }
    }

    public static async Task<List<Person>> LoadFromBinaryAsync(string filePath)
    {
        try
        {
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                return await MessagePackSerializer.DeserializeAsync<List<Person>>(stream);
            }
        }
        catch (Exception ex)
        {
            throw new IOException("Failed to load binary file.", ex);
        }
    }
}