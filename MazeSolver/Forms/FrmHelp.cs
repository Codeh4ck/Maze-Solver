using System;
using System.IO;
using System.Windows.Forms;

namespace MazeSolver.Forms
{
    public partial class FrmHelp : Form
    {
        public FrmHelp()
        {
            InitializeComponent();
        }

        private void saveExampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveFile = new SaveFileDialog
            {
                Filter = "Text Files|*.txt",
                Title = "Select a name and a directory to save the example maze blueprint..."
            };

            if (SaveFile.ShowDialog() != DialogResult.OK) return;

            try
            {
                File.WriteAllText(SaveFile.FileName, labelExample.Text);
                MessageBox.Show("The maze blueprint has been successfully saved on your chosen file!\nYou can now load it on the maze solver to test it!", "Maze blueprint saved!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"There was an error saving the example maze blueprint!\n\nDetails:\n{ex.Message}", "Maze blueprint save error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
            
        }

        private void copyExampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(labelExample.Text);
                MessageBox.Show("The maze blueprint has been copied to your clipboard!", "Maze blueprint copied!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"There was an error copying the example maze blueprint!\n\nDetails:\n{ex.Message}", "Maze blueprint copy error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}
