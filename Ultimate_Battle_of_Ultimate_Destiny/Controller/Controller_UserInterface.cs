using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Ultimate_Battle_of_Ultimate_Destiny.Model;

namespace Ultimate_Battle_of_Ultimate_Destiny.Controller
{
    public class Controller_UserInterface
    {
        public XmlDataManager xmlDataManager = new XmlDataManager();
        public List<Character> CharacterList { get; private set; }
        public Dictionary<string, string> Types { get; }
        List<string> SearchResults = new List<string>();

        public Controller_UserInterface()
        {
            Types = new Dictionary<string, string>()
            {
                {"Movie","Movies" },
                {"Athlete", "Athletes" },
                {"Video_Game", "Video Games" },
                {"Anime", "Anime" },
                {"Other", "Other" }
            };
            CharacterList = xmlDataManager.GetXmlDataAndAddToList(Types);
        }

        public void AddCharacter(string name, KeyValuePair<string, string> type)
        {
            bool check = CheckIfCharacterIsUnique(name, type);
            if (check == true)
            {
                CharacterList.Add(new Character(name, type));
                xmlDataManager.AddXmlItem(name, type.Key);
            }
        }

        public List<string> SearchCharacters(string searchText, KeyValuePair<string, string> type)
        {
            SearchResults.Clear();
            CharacterList.Clear();
            CharacterList = xmlDataManager.GetXmlDataAndAddToList(Types);
            //TODO: make search case insensitive
            IEnumerable<string> searchResults = from character in CharacterList
                              where character.Name.Contains(searchText)
                              where character.Type.Equals(type)
                              select character.Name;
            SearchResults = searchResults.ToList();
            return SearchResults;
        }

        public void UpdateCharacter(string oldName, string newName, KeyValuePair<string,string> type)
        {
            bool check = CheckIfCharacterIsUnique(newName, type);
            if (check == true)
            {
                foreach (Character character in CharacterList
                    .Where(w => w.Name == oldName).Where(x => x.Type.Key == type.Key))
                {
                    character.Name = newName;
                    xmlDataManager.UpdateXmlItem(oldName, newName, type.Key);
                    break;
                }
            }
        }

        public void DeleteCharacter(string name, KeyValuePair<string, string> type)
        {
            foreach (Character character in CharacterList
                .Where(w => w.Name == name).Where(x => x.Type.Key == type.Key))
            {
                CharacterList.Remove(character);
                xmlDataManager.DeleteXmlItem(name, type.Key);
                break;
            }
        }

        private bool CheckIfCharacterIsUnique(string name, KeyValuePair<string,string> type)
        {
            bool check = true;
            IEnumerable<string> duplicateCharacters = from character in CharacterList
                                                      where character.Name.Equals(name)
                                                      where character.Type.Equals(type)
                                                      select character.Name;
            if (duplicateCharacters.Count() > 0)
            {
                check = false;
                MessageBox.Show($"{name} of media type {type.Value} already exists!");
            }
            return check;
        }

    }
}
