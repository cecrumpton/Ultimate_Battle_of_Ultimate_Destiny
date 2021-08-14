using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Ultimate_Battle_of_Ultimate_Destiny.Model
{
    public class XmlDataManager
    {
        XDocument _doc;
        string _filepath;
        private List<Character> _charactersFromXML = new List<Character>();

        public XmlDataManager()
        {
            string directoryName = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            string fileName = "\\CharacterData.xml";
            _filepath = directoryName + fileName;
            if (File.Exists(_filepath))
            {
                _doc = XDocument.Load(_filepath);
            }
            else
            {
                _doc = new XDocument();
                _doc.Add(new XElement("Character"));
                _doc.Save(_filepath);
            }
        }

        public void AddXmlItem(string name, string type)
        {
            XElement character = _doc.Element("Character");
            character.Add(new XElement(type, name));
            _doc.Save(_filepath);
        }

        public void UpdateXmlItem(string oldName, string newName, string type)
        {
            XElement characterElement = _doc.Element("Character");
            var listOfCharactersByType = characterElement.Elements(type);
            foreach (var character in listOfCharactersByType.Where(w => w.Value == oldName))
            {
                character.Value = newName;
                break;
            }
            _doc.Save(_filepath);
        }

        public void DeleteXmlItem(string name, string type)
        {
            XElement characterElement = _doc.Element("Character");
            var listOfCharactersByType = characterElement.Elements(type);
            foreach (var character in listOfCharactersByType.Where(w => w.Value == name))
            {
                character.Remove();
                break;
            }
            _doc.Save(_filepath);
        }


        public List<Character> GetXmlDataAndAddToList(Dictionary<string,string> dataTypes)
        {
            foreach (KeyValuePair<string, string> type in dataTypes)
            {
                if(_doc.Root.Elements(type.Key).Count() != 0)
                {
                    IEnumerable<XElement> listofXmlItemsByType = _doc.Root.Elements(type.Key);
                    foreach (XElement element in listofXmlItemsByType)
                    {
                        _charactersFromXML.Add(new Character(element.Value, type));
                    }
                }
            }
            return _charactersFromXML;
        }

    }
}
