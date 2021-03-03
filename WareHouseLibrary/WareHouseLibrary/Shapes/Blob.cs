using System;

namespace WareHouseLibrary.Shapes
{
    class Blob : I3DStorageObject
    {
        public short Side { get; }
        public string ID { get; }

        public string Description { get; }

        public short Weight { get; }

        public double Volume { get; }

        public double Area { get; }

        public bool IsFragile { get; }

        public short MaxDimension { get; }

        public int InsuranceValue { get; set; }


        public Blob(string id, short side, string description, short weight)
        {

            double tempArea = Math.Round(side * side / (double)10000, 2);

            double tempVolume = Math.Round(side * side * side / (double)1000000, 2);

            double compareArea = side * side / (double)10000;

            double compareVolume = side * side * side / (double)1000000;

            if (tempArea < 0.01)
            {
                tempArea = 0.01;
            }
            else if (tempArea < compareArea)
            {
                tempArea += 0.01;
            }

            if (tempVolume < 0.01)
            {
                tempVolume = 0.01;
            }

            else if (tempVolume < compareVolume)
            {
                tempVolume += 0.01;
            }

            this.Side = side;
            this.ID = id;
            this.Description = description;
            this.Weight = weight;
            this.Volume = tempVolume;
            this.Area = tempArea;
            this.IsFragile = true;
            this.MaxDimension = side;
            this.InsuranceValue = 0;


        }

        public override string ToString()
        {
            return $"ID: {this.ID}\n" +
                $"Beskrivning: {this.Description}\n" +
                $"Vikt: {this.Weight} kg\n" +
                $"Volym: {this.Volume} m3\n" +
                $"Area: {this.Area} m2\n" +
                $"Ömtåligt: {this.IsFragile}\n" +
                $"Längsta sida: {this.MaxDimension} cm\n" +
                $"Försäkringsvärde: {this.InsuranceValue} kr";


        }
    }
}


