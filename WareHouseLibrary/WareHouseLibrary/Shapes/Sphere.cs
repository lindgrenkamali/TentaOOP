using System;

namespace WareHouseLibrary.Shapes
{
    public class Sphere : I3DStorageObject
    {
        public short Radius { get; }

        public string ID { get; }

        public string Description { get; }

        public short Weight { get; }

        public double Volume { get; }

        public double Area { get; }

        public bool IsFragile { get; }

        public short MaxDimension { get; }

        public int InsuranceValue { get; set; }

        //Ej klar
        internal Sphere(string id, short radius, string description, short weight, bool isFragile)
        {
            double tempVolume = Math.Round((radius + radius) * (radius + radius) * (radius + radius) / (double)1000000, 2);

            double tempArea = Math.Round(4 * Math.PI * (radius * radius) / (double)10000, 2);

            double compareArea = 4 * Math.PI * (radius * radius) / (double)10000;

            double compareVolume = (radius + radius) * (radius + radius) * (radius + radius) / (double)1000000;

            if (tempVolume < 0.01)
            {
                tempVolume = 0.01;
            }
            else if (tempVolume < compareVolume)
            {
                tempVolume += 0.01;
            }

            if (tempArea < 0.01)
            {
                tempArea = 0.01;
            }
            else if (tempArea < compareArea)
            {
                tempArea += 0.01;
            }


            this.Radius = radius;
            this.ID = id;
            this.Description = description;
            this.Weight = weight;
            this.Volume = tempVolume;
            this.Area = tempArea;
            this.IsFragile = isFragile;
            this.MaxDimension = (short)(radius + radius);
            this.InsuranceValue = 0;

        }

        public override string ToString()
        {
            return $"ID: {this.ID}\n" +
                $"Beskrivning: {this.Description}\n" +
                $"Vikt: {this.Weight} kg\n" +
                $"Volym: {this.Volume}  m3\n" +
                $"Area: {this.Area} m2\n" +
                $"Ömtåligt: {this.IsFragile}\n" +
                $"Längsta sida: {this.MaxDimension} cm\n" +
                $"Försäkringsvärde: {this.InsuranceValue} kr";


        }
    }
}
