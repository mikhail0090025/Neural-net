using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neural_net
{
    public partial class MainWindow : Form
    {
        Generation generation;
        LearningDatabase learningDatabase;
        public MainWindow()
        {
            InitializeComponent();
            generation = new Generation(4, 2, 4, 5, 50, RoundType.DontRound, RoundType.ZeroAndOne, RoundType.Tanh, 0.001f);
            learningDatabase = new LearningDatabase(4, 2);
            learningDatabase.AddItem(new List<double> { -1, -0.5f, 0f, 1f }, new List<double> { -0.9f, 0.65f });
            generation.SetDatabase(learningDatabase);
        }
        private async void btn_pass_one_gn_Click(object sender, EventArgs e)
        {
            await generation.PassOneGeneration();
            MessageBox.Show($"Error: {generation.CurrentError}");
        }
    }
}
