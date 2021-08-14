using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Ultimate_Battle_of_Ultimate_Destiny.Controller;

namespace Ultimate_Battle_of_Ultimate_Destiny.View
{
    public partial class UserInterface : Form
    {
        Controller_UserInterface _controller;
        KeyValuePair<string, string> _displayedCharacterType;
        public UserInterface()
        {
            InitializeComponent();
            _controller = new Controller_UserInterface();
            PopulateComboboxes();
        }


        private void button_Add_Click(object sender, EventArgs e)
        {
            _controller.AddCharacter(textBox_AddCharacter.Text, (KeyValuePair<string, string>)comboBox_AddCharacter.SelectedItem);
            if (textBox_SearchCharacter.Text != "" || listBox_SearchResults.Items.Count != 0)
            {
                Search();
            }
        }

        private void PopulateComboboxes()
        {
            comboBox_AddCharacter.DataSource = new BindingSource(_controller.Types, null);
            comboBox_AddCharacter.DisplayMember = "Value";
            comboBox_AddCharacter.ValueMember = "Key";
            comboBox_SearchCharacter.DataSource = new BindingSource(_controller.Types, null);
            comboBox_SearchCharacter.DisplayMember = "Value";
            comboBox_SearchCharacter.ValueMember = "Key";
        }

        private void button_Search_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            if(listBox_SearchResults.SelectedIndex != -1)
            {
                _controller.UpdateCharacter(listBox_SearchResults.Items[listBox_SearchResults.SelectedIndex].ToString(), textBox_UpdateCharacter.Text, _displayedCharacterType);
                Search();
            }
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            if (listBox_SearchResults.SelectedIndex != -1)
            {
                _controller.DeleteCharacter(listBox_SearchResults.Items[listBox_SearchResults.SelectedIndex].ToString(), _displayedCharacterType);
                Search();
            }
        }

        private void Search()
        {
            listBox_SearchResults.Items.Clear();
            _displayedCharacterType = (KeyValuePair<string, string>)comboBox_SearchCharacter.SelectedItem;
            List<string> characterNames = _controller.SearchCharacters(textBox_SearchCharacter.Text, _displayedCharacterType);
            foreach (string characterName in characterNames)
            {
                listBox_SearchResults.Items.Add(characterName);
            }
        }

        private void button_EnterTheRing_Click(object sender, EventArgs e)
        {
            int numberOfEntrants = _controller.CharacterList.Count();
            Random random = new Random();
            int winnerIndex = random.Next(numberOfEntrants);
            string winner = _controller.CharacterList[winnerIndex].Name;
            MessageBox.Show($"{winner} won the battle by triumphing over {numberOfEntrants} fighters!");
        }
    }
}
