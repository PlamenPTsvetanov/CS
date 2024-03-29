using System;
using System.IO;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
class Program
{
    static void Main()
    {
        Console.WriteLine("Insert file path:");
        string filePath = Console.ReadLine();
        string fileContents = File.ReadAllText(filePath);
        readCoordinatesAndSaveToFile(fileContents);

        try
        {
            List<PhoneInfo> listOfPhoneInfo = new List<PhoneInfo>();
            PhoneInfo phoneInfo = new PhoneInfo();

            fileContents = Regex.Replace(fileContents, @"\r|\t|\n", "  ");
            fileContents.Normalize();
            foreach (var info in Regex.Replace(fileContents, @"\s{2,}", " ").Split(" "))
            {
                string trimmed = info.Trim();
                if (Regex.Match(trimmed, "\\+395").Success || Regex.Match(trimmed, "^[0-9]{3}$").Success || Regex.Match(trimmed, "^[0-9]{2}$").Success)
                {
                    phoneInfo.number += trimmed + " ";
                }
                else if (Regex.Match(trimmed, "[\\d]").Success)
                {
                    phoneInfo.id = BigInteger.Parse(trimmed);
                }
                else if (Regex.Match(trimmed, "[^\\d]").Success)
                {
                    phoneInfo.name = trimmed;
                }

                if (phoneInfo.id.HasValue &&
                    phoneInfo.name != null &&
                    phoneInfo.number != null &&
                    phoneInfo.number.Length > 12)
                {
                    listOfPhoneInfo.Add(phoneInfo);
                    phoneInfo = new PhoneInfo();
                }
            }

            XmlDocument output = new XmlDocument();
            XmlElement root = output.CreateElement("Phonebook");
            output.AppendChild(root);

            foreach (var info in listOfPhoneInfo)
            {
                XmlElement contactElement = output.CreateElement("Phonebook");

                XmlElement name = output.CreateElement("name");
                name.InnerText = info.name;
                contactElement.AppendChild(name);

                XmlElement id = output.CreateElement("id");
                id.InnerText = info.id.ToString();
                contactElement.AppendChild(id);

                XmlElement phoneNumber = output.CreateElement("number");
                phoneNumber.InnerText = info.number.Trim();
                contactElement.AppendChild(phoneNumber);

                root.AppendChild(contactElement);
            }

            output.Save("/phonebook.xml");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }

    }

    private static void readCoordinatesAndSaveToFile(string fileContents)
    {
        try
        { 
            string[] coordinatePairs = fileContents.Split(";");

            List<Coordinates> listOfCoordinates = new List<Coordinates>();
            foreach (var coordinates in coordinatePairs)
            {
                Coordinates coordinate = new Coordinates();
                string[] strings = coordinates.Split(",");
                coordinate.lat = Double.Parse(strings[0]);
                coordinate.lng = Double.Parse(strings[1]);
                listOfCoordinates.Add(coordinate);
            }

            string json = JsonSerializer.Serialize(listOfCoordinates, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            using (FileStream fs = File.Create("/location.json"))
            {
                fs.Write(new UTF8Encoding(true).GetBytes(json), 0, json.Length);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }

    struct Coordinates
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    struct PhoneInfo
    {
        public String? number { get; set; }
        public String? name { get; set; }
        public BigInteger? id { get; set; }
    }
}
