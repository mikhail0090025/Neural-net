using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neural_net
{
    internal class Generation
    {
        protected List<NeuralNet> generation;
        protected int inputsCount;
        protected int outputsCount;
        protected int hiddenLayersCount;
        protected int neuralsInHiddenLayersAmount;
        protected int size;
        protected RoundType inpRoundType;
        protected RoundType neuralRoundType;
        protected RoundType outpRoundType;
        protected LearningDatabase learningDatabase;
        public int InputsCount => inputsCount;
        public int OutputsCount => outputsCount;
        public int HiddenLayersCount => hiddenLayersCount;
        public int NeuralsInHiddenLayersAmount => neuralsInHiddenLayersAmount;
        public int Size => size;
        public LearningDatabase LearningDatabase => learningDatabase;
        public RoundType InputsRound
        {
            get => inpRoundType;
            set
            {
                inpRoundType = value;
                foreach (var neuralNet in generation) neuralNet.inputRoundType = value;
            }
        }
        public RoundType NeuralsRound
        {
            get => neuralRoundType;
            set
            {
                neuralRoundType = value;
                foreach (var neuralNet in generation) neuralNet.neuralRoundType = value;
            }
        }
        public RoundType OutputsRound
        {
            get => outpRoundType;
            set
            {
                outpRoundType = value;
                foreach (var neuralNet in generation) neuralNet.outputRoundType = value;
            }
        }
        public Generation(int inpCount, int outpCount, int hiddenLayersCount_, int neuralsInHiddenLayerCount, int genSize, RoundType inputs_round,
            RoundType neurals_round, RoundType output_round)
        {
            inpRoundType = inputs_round;
            neuralRoundType = neurals_round;
            outpRoundType = output_round;
            inputsCount = inpCount;
            outputsCount = outpCount;
            hiddenLayersCount = hiddenLayersCount_;
            neuralsInHiddenLayersAmount = neuralsInHiddenLayerCount;
            generation = new List<NeuralNet>();
            size = genSize;
            for (int i = 0; i < genSize; i++)
            {
                generation.Add(new NeuralNet(inpCount, outpCount, hiddenLayersCount_, neuralsInHiddenLayerCount, inputs_round, neurals_round, output_round));
            }
        }
        public void SetDatabase(LearningDatabase database)
        {
            if (database.InputsCount != this.InputsCount) throw new Exception("Given database is invalid, because counts of inputs are different");
            if (database.OutputsCount != this.OutputsCount) throw new Exception("Given database is invalid, because counts of outputs are different");
            learningDatabase = database;
        }
        public async Task<NeuralNet> BestNN()
        {
            // Counting errors of neural nets
            List<double> Errors = new List<double>();
            for (int i = 0; i < generation.Count; i++)
            {
                double error_this_nn = 0;
                for (int j = 0; j < learningDatabase.Size; j++)
                {
                    var gotten_data = generation[i].CalcOutputs(learningDatabase.TestInputs[j]);
                    for (int k = 0; k < gotten_data.Count; k++) error_this_nn += Math.Abs(gotten_data[k] - learningDatabase.ExpectedOutputs[j][k]);
                }
                Errors.Add(error_this_nn);
            }
        }
    }
    internal class LearningDatabase
    {
        protected List<List<double>> testInputs;
        protected List<List<double>> expectedOutputs;
        public List<List<double>> TestInputs => testInputs;
        public List<List<double>> ExpectedOutputs => expectedOutputs;
        public int Size => testInputs.Count;
        protected int inputsCount;
        protected int outputsCount;
        public int InputsCount => inputsCount;
        public int OutputsCount => outputsCount;
        public LearningDatabase(int inputs_count, int outputs_count)
        {
            inputsCount = inputs_count;
            outputsCount = outputs_count;
            testInputs = new List<List<double>>();
            expectedOutputs = new List<List<double>>();
        }
        public void AddItem(List<double> test_inputs, List<double> exp_outputs)
        {
            if (test_inputs.Count != inputsCount) throw new Exception("Given test inputs are invalid for this database");
            if (exp_outputs.Count != outputsCount) throw new Exception("Given expected outputs are invalid for this database");
            testInputs.Add(test_inputs);
            expectedOutputs.Add(exp_outputs);
        }
    }
}
