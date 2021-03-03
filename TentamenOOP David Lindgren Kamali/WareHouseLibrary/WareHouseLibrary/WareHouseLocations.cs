using System;
using System.Collections.Generic;

namespace WareHouseLibrary
{
    internal class WareHouseLocations
    {
        internal double WeightLeft { get; set; }

        internal double VolumeLeft { get; set; }

        internal bool FragileBox { get; set; }

        private List<I3DStorageObject> WHLocation { get; }


        internal short Height
        {
            get;
        }



        internal short Width
        {
            get;
        }

        internal short Depth { get; }


        internal double MaxWeight { get; }

        internal double MaxVolume { get; }



        internal WareHouseLocations()
        {
            this.Height = 200;
            this.Width = 300;
            this.Depth = 200;
            this.MaxWeight = 1000;
            this.MaxVolume = Convert.ToUInt32(this.Height * this.Width * this.Depth) / 1000000;
            this.FragileBox = false;
            this.WeightLeft = MaxWeight;
            this.VolumeLeft = MaxVolume;
            this.WHLocation = new List<I3DStorageObject>();
        }

        internal I3DStorageObject AddPackage(I3DStorageObject package)
        {

            this.WHLocation.Add(package);
            this.WeightLeft = this.WeightLeft - package.Weight;
            this.VolumeLeft = this.VolumeLeft - package.Volume;
            this.FragileBox = package.IsFragile;
            return package;
        }

        internal I3DStorageObject SearchPackageList(string id)
        {

            for (int i = 0; i < this.WHLocation.Count; i++)
            {
                if (WHLocation[i].ID == id)
                {
                    return this.WHLocation[i];
                }
            }
            return null;
        }

        internal I3DStorageObject DeletePackage(string id)
        {

            if (this.WHLocation.Count >= 0)
            {

                for (int i = 0; i < this.WHLocation.Count; i++)
                {
                    if (WHLocation[i].ID == id)
                    {

                        this.WeightLeft = this.WeightLeft + this.WHLocation[i].Weight;
                        this.VolumeLeft = this.VolumeLeft + this.WHLocation[i].Volume;
                        if (this.WHLocation[i].IsFragile == true)
                        {
                            this.FragileBox = false;
                        }
                        I3DStorageObject box = this.WHLocation[i];
                        this.WHLocation.RemoveAt(i);

                        return box;
                    }
                }
                return null;
            }
            return null;
        }




    }

}

