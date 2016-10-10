using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MazeSolver.MazeComponents;
using MazeSolver.MazeComponents.HelperTypes;

namespace MazeSolver.Forms
{
    public partial class FrmMain : Form
    {

        private Solver Solver;
        private MazeLoader Loader;
        private int[,] Maze;

        private Point? EntrancePoint;
        private Point? ExitPoint;
        private Point SelectedBlock = Point.Empty;

        private bool IsMatrixSolved = false;

        public FrmMain()
        {
            InitializeComponent();

            pbMaze.Size = new Size(Settings.MAZE_COLUMNS * Settings.MAZE_BLOCK_SIZE + 3, Settings.MAZE_ROWS * Settings.MAZE_BLOCK_SIZE + 3);

            Solver = new Solver(Settings.MAZE_ROWS, Settings.MAZE_COLUMNS);
            Maze = Solver.GetMaze();
        }

        private void FrmMain_Load(object sender, System.EventArgs e)
        {
            Solver = new Solver(Settings.MAZE_ROWS, Settings.MAZE_COLUMNS);
            pbMaze.Size = new Size(Settings.MAZE_COLUMNS * Settings.MAZE_BLOCK_SIZE + 1, Settings.MAZE_ROWS * Settings.MAZE_BLOCK_SIZE + 1);
        }

        private void pbMaze_MouseDown(object sender, MouseEventArgs e)
        {
            // If the matrix has been solved, do not allow maze edit.
            if (IsMatrixSolved)
                return;

            int BlockX = e.X / Settings.MAZE_BLOCK_SIZE;
            int BlockY = e.Y / Settings.MAZE_BLOCK_SIZE;

            if (BlockX < Settings.MAZE_COLUMNS && BlockX >= 0 && BlockY < Settings.MAZE_ROWS && BlockY >= 0)
            {
                if (cmdManualEdit.Checked)
                {
                    Maze = Solver.GetMaze();

                    Maze[BlockY, BlockX] = (Maze[BlockY, BlockX] == (int) MazeNodeStatus.WalkableTile)
                        ? (int) MazeNodeStatus.Wall
                        : (int) MazeNodeStatus.WalkableTile;

                    //if (Maze[BlockY, BlockX] == 0)
                    //    Maze[BlockY, BlockX] = (int) MazeNodeStatus.Wall;
                    //else
                    //    Maze[BlockY, BlockX] = 0;

                    pbMaze.Refresh();
                }
                else
                {
                    txtSolutionLogs.Clear();

                    if (Maze[BlockY, BlockX] == (int) MazeNodeStatus.Path)
                    {

                        while (SelectedBlock.X != BlockX || SelectedBlock.Y != BlockY)
                        {
                            Maze[SelectedBlock.Y, SelectedBlock.X] = 0;

                            // This is basically some logic to determine which block at which direction was clicked and move to it.

                            if (SelectedBlock.X - 1 >= 0 && SelectedBlock.X - 1 < Settings.MAZE_COLUMNS && Maze[SelectedBlock.Y, SelectedBlock.X - 1] == (int) MazeNodeStatus.Path)
                                SelectedBlock.X--;
                            else if (SelectedBlock.X + 1 >= 0 && SelectedBlock.X + 1 < Settings.MAZE_COLUMNS && Maze[SelectedBlock.Y, SelectedBlock.X + 1] == (int)MazeNodeStatus.Path)
                                SelectedBlock.X++;
                            else if (SelectedBlock.Y - 1 >= 0 && SelectedBlock.Y - 1 < Settings.MAZE_ROWS && Maze[SelectedBlock.Y - 1, SelectedBlock.X] == (int)MazeNodeStatus.Path)
                                SelectedBlock.Y--;
                            else if (SelectedBlock.Y + 1 >= 0 && SelectedBlock.Y + 1 < Settings.MAZE_ROWS && Maze[SelectedBlock.Y + 1, SelectedBlock.X] == (int)MazeNodeStatus.Path)
                                SelectedBlock.Y++;

                            pbMaze.Refresh();
                        }
                    }
                }
            }
        }

        private void pbMaze_MouseMove(object sender, MouseEventArgs e)
        {
            // If the matrix has been solved, do not allow maze edit.
            if (IsMatrixSolved)
                return;
               
            int BlockX = e.X / Settings.MAZE_BLOCK_SIZE;
            int BlockY = e.Y / Settings.MAZE_BLOCK_SIZE;

            if (BlockX < Settings.MAZE_COLUMNS && BlockX >= 0 && BlockY < Settings.MAZE_ROWS && BlockY >= 0)
            {
                Maze = Solver.GetMaze();

                if (!cmdManualEdit.Checked)
                {
                    int[,] SolvedMaze = Solver.SolveMazeMatrix(SelectedBlock.X, SelectedBlock.Y, BlockX, BlockY);

                    if (SolvedMaze != null)                    
                        Maze = SolvedMaze;  
                                      
                    pbMaze.Refresh();
                }
            }
        }

        private void pbMaze_Paint(object sender, PaintEventArgs e)
        {
            // We manually draw the picturebox in order to draw the maze grid.
            Graphics G = e.Graphics;

            for (int x = 0; x < Settings.MAZE_ROWS; x++)
            {
                for (int y = 0; y < Settings.MAZE_COLUMNS; y++)
                {
                    // Draw the grid rectangles
                    G.DrawRectangle(ColorSettings.MazeGridColorPen, y * Settings.MAZE_BLOCK_SIZE, x * Settings.MAZE_BLOCK_SIZE, Settings.MAZE_BLOCK_SIZE, Settings.MAZE_BLOCK_SIZE);

                    if (Solver != null)
                    {
                        // If the maze has been set to null, it means there was no solution to it and it cannot be drawn.
                        if (IsMatrixSolved && Maze == null)
                        {
                            MessageBox.Show("Your maze has no possible solution.", "Maze not solvable!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            // This is an ugly hack to clear the maze. I am merely clicking the clear button programmatically.
                            cmdClear.PerformClick();
                            return;
                        }


                        // If a maze is loaded, draw each rectangle depending on the node status.                        
                        SolidBrush ColorBrush;

                        switch (Maze[x, y])
                        {
                            case (int)MazeNodeStatus.Wall:
                                ColorBrush = ColorSettings.WallTileColorBrush;
                                break;
                            case (int)MazeNodeStatus.Path:                                                                                                                          
                                ColorBrush = ColorSettings.WalkedTileColorBrush;
                                break;
                            default:
                                ColorBrush = ColorSettings.WalkableTileColorBrush;
                                break;
                        }

                        /* 
                         * If the maze was loaded from a file, both EntrancePoint
                         *  and ExitPoint must be set. If they are set, highlight
                         *   the entrance with Green and the exit with Red colors.
                         *   
                        */

                        if (EntrancePoint != null)
                        {
                            if (EntrancePoint.Value == new Point(x, y))
                                ColorBrush = ColorSettings.EntranceTileColorBrush;
                        }

                        if (ExitPoint != null)
                        {
                            if (ExitPoint.Value == new Point(x, y))
                                ColorBrush = ColorSettings.ExitTileColorBrush;
                        }
                        

                        G.FillRectangle(ColorBrush, y * Settings.MAZE_BLOCK_SIZE + 1,
                            x * Settings.MAZE_BLOCK_SIZE + 1, Settings.MAZE_BLOCK_SIZE - 1,
                            Settings.MAZE_BLOCK_SIZE - 1);

                    }
                    else
                    {
                        G.FillRectangle(ColorSettings.WalkableTileColorBrush, y * Settings.MAZE_BLOCK_SIZE + 1,
                               x * Settings.MAZE_BLOCK_SIZE + 1, Settings.MAZE_BLOCK_SIZE - 1,
                               Settings.MAZE_BLOCK_SIZE - 1);
                    }
                }
            }
        }

        private void cmdLoadMaze_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog
            {
                Filter = "Text Files|*.txt",
                Title = "Select a maze map text file..."
            };

            if (OpenFile.ShowDialog() != DialogResult.OK) return;

            Loader = new MazeLoader();

            try
            {                                                
                int[,] LoadedMaze = Loader.LoadCoordinatesFromFile(OpenFile.FileName);

                Settings.MAZE_ROWS = LoadedMaze.GetLength(0);
                Settings.MAZE_COLUMNS = LoadedMaze.GetLength(1);

                pbMaze.Size = new Size(Settings.MAZE_COLUMNS * Settings.MAZE_BLOCK_SIZE + 3, Settings.MAZE_ROWS * Settings.MAZE_BLOCK_SIZE + 3);

                Solver = new Solver(LoadedMaze);
                Maze = Solver.GetMaze();

                EntrancePoint = new Point(Loader.EntranceCoordinates.X, Loader.EntranceCoordinates.Y);
                ExitPoint = new Point(Loader.ExitCoordinates.X, Loader.ExitCoordinates.Y);

                // We do not want to modify the loaded maze
                cmdManualEdit.Enabled = false;

                pbMaze.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error has occurred while trying to load your maze.\n\nDetails:\n{ex.Message}",
                    "Error while loading maze!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            // If a maze was loaded, then manual edit button will be turned off. Re-enable it.
            cmdManualEdit.Enabled = true;

            txtSolutionLogs.Clear();

            IsMatrixSolved = false;

            Settings.MAZE_ROWS = 16;
            Settings.MAZE_COLUMNS = 20;

            Solver = new Solver(Settings.MAZE_ROWS, Settings.MAZE_COLUMNS);
            pbMaze.Size = new Size(Settings.MAZE_COLUMNS * Settings.MAZE_BLOCK_SIZE + 3, Settings.MAZE_ROWS * Settings.MAZE_BLOCK_SIZE + 3);

            Maze = Solver.GetMaze();

            EntrancePoint = null;
            ExitPoint = null;

            pbMaze.Refresh();
        }

        private void cmdSolveMaze_Click(object sender, EventArgs e)
        {
            if (Solver == null || EntrancePoint == null || ExitPoint == null)
            {
                MessageBox.Show("Please load a maze from a pre-built text file first.", "No maze loaded!",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtSolutionLogs.Clear();

            Solver.SolutionPoints.Clear();

            int[,] SolvedMaze = Solver.SolveMazeMatrix(EntrancePoint.Value.Y, EntrancePoint.Value.X, ExitPoint.Value.Y,
                ExitPoint.Value.X);

            Maze = SolvedMaze;
            IsMatrixSolved = true;

            if (Solver.SolutionPoints != null && Solver.SolutionPoints.Count > 0)
            {
                List<Point> SolutionPoints = Solver.SolutionPoints;
                SolutionPoints.Reverse();

                if (EntrancePoint != null)
                    txtSolutionLogs.AppendText($"Starting on entrance point at ({EntrancePoint.Value.X}, {EntrancePoint.Value.Y})\n");

                // We skip one element because the first element is the entrance point.
                foreach (Point P in SolutionPoints.Skip(1).ToList())                
                    txtSolutionLogs.AppendText($"Moving to block ({P.X}, {P.Y})\n");

                if (ExitPoint != null)
                    txtSolutionLogs.AppendText($"Finishing on exit point at ({ExitPoint.Value.X}, {ExitPoint.Value.Y})\n");
            }

            pbMaze.Refresh();
        }

        private void cmdManualEdit_CheckedChanged(object sender, EventArgs e)
        {
            pbMaze.Refresh();
        }

        private void pbMaze_Resize(object sender, EventArgs e)
        {
            // Change the width of the form when the PictureBox containing the maze resizes.
            Width = pbMaze.Width + 320;
        }

        private void FrmMain_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FrmHelp HelpForm = new FrmHelp();
            HelpForm.Show(this);
        }
    }
}
