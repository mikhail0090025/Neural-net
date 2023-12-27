using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neural_net
{
    public enum RoundType { DontRound, Tanh, ZeroAndOne }
    internal class NeuralNet
    {
        protected Layer inputs;
        protected Layer outputs;
        public Layer Outputs => outputs;
        public Layer Inputs => inputs;
        protected List<Layer> HiddenLayers;
        public RoundType inputRoundType;
        public RoundType neuralRoundType;
        public RoundType outputRoundType;
        public NeuralNet(int InputsAmount, int OutputsAmount, int HiddenLayersAmount, int NeuralsInHiddenLayerAmount, RoundType inputsRound, RoundType neuralRound, RoundType outputRound)
        {
            inputRoundType = inputsRound;
            neuralRoundType = neuralRound;
            outputRoundType = outputRound;
            HiddenLayers = new List<Layer>();
            for (int i = 0; i < HiddenLayersAmount; i++) HiddenLayers.Add(null);
            // Outputs initialization
            var outputs_ = new List<Neural>();
            for (int i = 0; i < OutputsAmount; i++)
            {
                outputs_.Add(new Neural());
            }
            outputs = new Layer(null, outputs_);
            // Hidden layers initialization
            for (int i = HiddenLayersAmount - 1; i >= 0; i--)
            {
                var neurals_ = new List<Neural>();
                for (int j = 0; j < NeuralsInHiddenLayerAmount; j++)
                {
                    neurals_.Add(new Neural());
                }
                if (i == HiddenLayersAmount - 1)
                {
                    HiddenLayers[i] = new Layer(outputs, neurals_);
                }
                else
                {
                    HiddenLayers[i] = new Layer(HiddenLayers[i + 1], neurals_);
                }
            }
            // Inputs initialization
            var inputs_ = new List<Neural>();
            for (int i = 0; i < InputsAmount; i++)
            {
                inputs_.Add(new Neural());
            }
            inputs = new Layer(HiddenLayers[0], inputs_);
        }
        public List<double> CalcOutputs(List<double> inputs_)
        {
            if (inputs_.Count != inputs.Size) throw new Exception("Count of transmitted inputs is not equal to count inputs in neural net");
            // Inputs getting
            for (int i = 0; i < inputs_.Count; i++)
            {
                inputs.Neurals[i].SetValue(inputs_[i]);
                inputs.Neurals[i].Round(inputRoundType);
            }
            inputs.SetNextLayer();
            // Hidden neural layers counting
            for (int i = 0; i < HiddenLayers.Count; i++)
            {
                HiddenLayers[i].Round(neuralRoundType);
                HiddenLayers[i].SetNextLayer();
            }
            // Outputs round
            foreach (var item in outputs.Neurals)
            {
                item.Round(outputRoundType);
            }
            // Getting numbers
            List<double> result = new List<double>();
            for (int i = 0; i < outputs.Neurals.Count; i++)
            {
                result.Add(outputs.Neurals[i].Value);
            }
            return result;
        }
        private static Random random = new Random();
        public async Task SetBy(NeuralNet NN, double ChangeFactor)
        {
            double newRandomValue() => ((random.NextDouble() * 2) - 1) * ChangeFactor;
            await Task.Run(() =>
            {
                // Inputs connections setting
                for (int i = 0; i < inputs.Connection.CurrentLayerSize; i++)
                {
                    for (int j = 0; j < inputs.Connection.NextLayerSize; j++)
                    {
                        inputs.Connection.SetConnection(i, j, NN.Inputs.Connection.GetConnection(i, j) + newRandomValue());
                    }
                }
                for (int i = 0; i < HiddenLayers.Count; i++)
                {
                    for (int j = 0; j < HiddenLayers[i].Connection.CurrentLayerSize; j++)
                    {
                        for (int k = 0; k < HiddenLayers[i].Connection.NextLayerSize; k++)
                        {
                            var num = NN.HiddenLayers[i].Connection.GetConnection(j, k) + newRandomValue();
                            HiddenLayers[i].Connection.SetConnection(j, k, num);
                        }
                    }
                }
            });
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Inputs: ");
            foreach (var item in inputs.Neurals)
            {
                stringBuilder.Append($"{item.Value}, ");
            }
            stringBuilder.Append("Neurals: ");
            foreach (var layer in HiddenLayers)
            {
                foreach (var item in layer.Neurals)
                {
                    stringBuilder.Append($"{item.Value}, ");
                }
            }
            stringBuilder.Append("Outputs: ");
            foreach (var item in outputs.Neurals)
            {
                stringBuilder.Append($"{item.Value}, ");
            }
            return stringBuilder.ToString();
        }
    }
    internal class Neural
    {
        protected double value;
        public double Value => value;
        public void SetValue(double v)
        {
            value = v;
        }
        public void Round(RoundType RoundType_)
        {
            switch (RoundType_)
            {
                case RoundType.DontRound: break;
                case RoundType.Tanh:
                    value = Math.Tanh(value);
                    break;
                case RoundType.ZeroAndOne:
                    value = value > 0 ? 1 : 0;
                    break;
                    default: throw new Exception("Type is undefined");
            }
        }
        public void Reset() => value = 0;
        public Neural()
        {
            value = 0;
        }
        public Neural(double v)
        {
            value = v;
        }
    }
    internal class Layer
    {
        protected List<Neural> neurals;
        public List<Neural> Neurals => neurals;
        public Layer NextLayer;
        public Connection Connection;
        public int Size => neurals.Count;
        public Layer(Layer next_layer, List<Neural> neurals_)
        {
            neurals = new List<Neural>();
            NextLayer = next_layer;
            neurals = neurals_;
            if (NextLayer != null)
            Connection = new Connection(this, NextLayer);
        }
        public void Reset()
        {
            foreach (var neural in neurals)
            {
                neural.Reset();
            }
        }
        public void Round(RoundType roundType)
        {
            foreach (var item in neurals)
            {
                item.Round(roundType);
            }
        }
        public void SetNextLayer()
        {
            NextLayer.Reset();
            for (int i = 0; i < neurals.Count; i++)
            {
                for (int j = 0; j < NextLayer.neurals.Count; j++)
                {
                    var n = NextLayer.neurals[j];
                    n.SetValue(n.Value + (neurals[i].Value * Connection.GetConnection(i, j)));
                }
            }
        }
    }
    internal class Connection
    {
        protected List<List<double>> connections;
        protected Layer CurrentLayer;
        protected Layer NextLayer;
        public int CurrentLayerSize => CurrentLayer.Size;
        public int NextLayerSize => NextLayer.Size;
        // New connection
        private static Random random = new Random();
        public Connection(Layer currentLayer, Layer nextLayer)
        {
            CurrentLayer = currentLayer;
            NextLayer = nextLayer;
            connections = new List<List<double>>();
            double RandomConnection() => (random.NextDouble() * 2) - 1f;
            for (int i = 0; i < currentLayer.Size; i++)
            {
                var line = new List<double>();
                for (int j = 0; j < nextLayer.Size; j++)
                {
                    line.Add(RandomConnection());
                }
                connections.Add(line);
            }
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in connections)
            {
                stringBuilder.Append(string.Join(", ", item) + "\n");
            }
            return stringBuilder.ToString();
        }
        public double GetConnection(int indexInCurrentLayer, int indexInNextLayer) => connections[indexInCurrentLayer][indexInNextLayer];
        public double SetConnection(int indexInCurrentLayer, int indexInNextLayer, double value) => connections[indexInCurrentLayer][indexInNextLayer] = value;
    }
}
