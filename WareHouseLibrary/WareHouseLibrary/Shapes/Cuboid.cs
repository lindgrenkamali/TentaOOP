using System;

namespace WareHouseLibrary.Shapes
{
    class Cuboid : I3DStorageObject
    {
        public short XSide { get; }

        public short YSide { get; }

        public short ZSide { get; }


        public string ID { get; }

        public string Description { get; }

        public short Weight { get; }

        public double Volume { get; }

        public double Area { get; }

        public bool IsFragile { get; }

        public short MaxDimension { get; }

        public int InsuranceValue { get; set; }

        public Cuboid(string id, short xSide, short ySide, short zSide, string description, short weight, bool isFragile)
        {
            double compareArea = xSide * ySide / (double)10000;
            double compareVolume = xSide * ySide * zSide / (double)1000000;
            double tempArea = Math.Round(xSide * ySide / (double)10000, 2);
            double tempVolume = Math.Round(xSide * ySide * zSide / (double)1000000, 2);

            if(tempArea < 0.01)
            {
                tempArea = 0.01;
            }
            else if(tempArea < compareArea)
            {
                tempArea += 0.01;
            }

            if (tempVolume < 0.01)
            {
                tempVolume = 0.01;
            }
            else if(tempVolume < compareVolume)
            {
                tempVolume += 0.01;
            }

            this.XSide = xSide;
            this.YSide = ySide;
            this.ZSide = zSide;
            this.ID = id;
            this.Description = description;
            this.Weight = weight;
            this.Volume = tempVolume;
            this.Area = tempArea;
            this.IsFragile = isFragile;
            this.MaxDimension = Math.Max(xSide, Math.Max(ySide, zSide));
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
