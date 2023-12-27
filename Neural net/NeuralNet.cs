using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neural_net
{
    public enum RoundType { DontRound, Tanh, NeroAndOne }
    internal class NeuralNet
    {
        protected Layer inputs;
        protected Layer outputs;
        public Layer Outputs => outputs;
        protected List<Layer> HiddenLayers;
        protected RoundType inputRoundType;
        protected RoundType neuralRoundType;
        protected RoundType outputRoundType;
        public NeuralNet(int InputsAmount, int OutputsAmount, int HiddenLayersAmount, int NeuralsInHiddenLayerAmount, RoundType inputsRound, RoundType neuralRound, RoundType outputRound)
        {
            inputRoundType = inputsRound;
            neuralRoundType = neuralRound;
            outputRoundType = outputRound;
            HiddenLayers = new List<Layer>(HiddenLayersAmount);
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
            // Hidden neural layers counting
            for (int i = 0; i < HiddenLayers.Count; i++)
            {
                HiddenLayers[i].Round(neuralRoundType);
                HiddenLayers[i].NextLayer.Reset();
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
                case RoundType.NeroAndOne:
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
        protected Connection Connection;
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
        // New connection
        public Connection(Layer currentLayer, Layer nextLayer)
        {
            CurrentLayer = currentLayer;
            NextLayer = nextLayer;
            connections = new List<List<double>>();
            Random random = new Random();
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
        public double GetConnection(int indexInCurrentLayer, int indexInNextLayer) => connections[indexInCurrentLayer][indexInNextLayer];
    }
}
