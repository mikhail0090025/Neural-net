using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

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
            // Some data to database
            learningDatabase.AddItem(new List<double> { -1, -0.5, 0, 1 }, new List<double> { -0.9, 0.65 });
            learningDatabase.AddItem(new List<double> { 0.5, 0.2, -0.7, 1 }, new List<double> { 0.3, -0.5 });
            learningDatabase.AddItem(new List<double> { 0.8, -0.3, 0.9, -1 }, new List<double> { 0.8, -0.2 });
            learningDatabase.AddItem(new List<double> { -0.2, 0.6, -0.4, 0.2 }, new List<double> { -0.1, 0.9 });
            generation.SetDatabase(learningDatabase);
            ////////////////////////////////////
            SetUI();
        }
        private async void btn_pass_one_gn_Click(object sender, EventArgs e)
        {
            await generation.PassOneGeneration();
            SetUI();
        }
        void SetUI()
        {
            lbl_data.Text = $"Error: {generation.CurrentError}\nError change: {generation.ErrorChange}\nGenerations passed: {generation.GenerationsPassed}";
            lbl_sensivity_learning.Text = $"Sensivity: {generation.LearningFactor}";
        }
        private void btn_increase_sensivity_Click(object sender, EventArgs e)
        {
            generation.IncreaseLearningFactor();
            SetUI();
        }

        private void btn_reduce_sensivity_Click(object sender, EventArgs e)
        {
            generation.ReduceLearningFactor();
            SetUI();
        }
        bool IsLearning;
        private async void btn_learn_untill_stop_Click(object sender, EventArgs e)
        {
            if(IsLearning)
            {
                MessageBox.Show("Neural net is already learning!");
                return;
            }
            await Task.Run(async () =>
            {
                IsLearning = true;
                while (IsLearning)
                {
                    await generation.PassOneGeneration();
                }
            });
            SetUI();
        }
        private void btn_stop_Click(object sender, EventArgs e)
        {
            IsLearning = false;
        }

        private async void btn_time_for_1_gen_Click(object sender, EventArgs e)
        {
            if(IsLearning)
            {
                MessageBox.Show("Neural net is learning. Stop learning to try it");
                return;
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            await generation.PassOneGeneration();
            sw.Stop();
            MessageBox.Show($"Time for 1 generation is {sw.ElapsedMilliseconds} milliseconds.");
            SetUI();
        }
    }
}
