using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neural_net
{
    internal class NeuralNet
    {
        protected Layer inputs;
        protected Layer outputs;
        public Layer Outputs => outputs;
        protected List<Layer> HiddenLayers;
        public NeuralNet(int InputsAmount, int OutputsAmount, int HiddenLayersAmount, int NeuralsInHiddenLayerAmount)
        {
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
    }
    internal class Neural
    {
        protected double value;
        public double Value => value;
        protected void SetValue(double v)
        {
            value = v;
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
    }
}
