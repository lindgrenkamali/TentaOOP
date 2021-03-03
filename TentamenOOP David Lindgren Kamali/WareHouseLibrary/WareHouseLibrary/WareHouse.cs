using WareHouseLibrary.Shapes;

namespace WareHouseLibrary
{

    public class WareHouse
    {

        private WareHouseLocations[,] wareHouseStorage = new WareHouseLocations[3, 100];

        private void FillBoxes()
        {
            wareHouseStorage[0, 2].AddPackage(new Cuboid("XG532P1", 10, 10, 50, "Kaktus", 3, false));

            wareHouseStorage[1, 25].AddPackage(new Cube("FDS332G", 110, "Tjock-TV", 20, true));

            wareHouseStorage[2, 69].AddPackage(new Blob("GFDG34L", 70, "Uppstoppad sköldpadda", 15));

            wareHouseStorage[0, 99].AddPackage(new Sphere("53F4S44", 20, "Fotboll", 3, false));



            wareHouseStorage[0, 8].AddPackage(new Cuboid("R55G1LO", 40, 20, 50, "Xbox Series X", 8, true));

            wareHouseStorage[1, 75].AddPackage(new Cube("L4ED0RT", 10, "Rubiks kub", 1, false));

            wareHouseStorage[2, 22].AddPackage(new Blob("4GFD423", 100, "Zaccosäck", 10));
            wareHouseStorage[0, 33].AddPackage(new Sphere("S904FAC", 114, "Stenklot", 1000, false));

            wareHouseStorage[0, 2].AddPackage(new Cube("G5J4J3F", 30, "Skumgummi", 1, false));

            wareHouseStorage[1, 59].AddPackage(new Cuboid("340GDDS", 190, 100, 50, "Säng", 50, false));
            
        }

        public WareHouse()
        {
            for (int i = 0; i < wareHouseStorage.GetLength(0); i++)
            {
                for (int j = 0; j < wareHouseStorage.GetLength(1); j++)
                {
                    wareHouseStorage[i, j] = new WareHouseLocations();

                }
            }

            FillBoxes();

        }

        //Försöker lägga till paket med cube
        public I3DStorageObject AddCube(string id, short side, string description, short weight, bool isFragile, bool manualLocation, int i, int j, out int iSave, out int jSave)
        {

            I3DStorageObject cube = new Cube(id, side, description, weight, isFragile);
            I3DStorageObject addedBox = BoxAddLoop(cube, manualLocation, i, j, out iSave, out jSave);
            iSave = i;
            jSave = j;
            return addedBox;

        }

        //Försöker lägga till paket med cuboid
        public I3DStorageObject AddCuboid(string id, short xSide, short ySide, short zSide, string description, short weight, bool isFragile, bool manualLocation, int i, int j, out int iSave, out int jSave)
        {
            I3DStorageObject cuboid = new Cuboid(id, xSide, ySide, zSide, description, weight, isFragile);

            I3DStorageObject addedBox = BoxAddLoop(cuboid, manualLocation, i, j, out iSave, out jSave);
            return addedBox;
        }

        //Försöker lägga till paket med sphere

        public I3DStorageObject AddSphere(string id, short radius, string description, short weight, bool isFragile, bool manualLocation, int i, int j, out int iSave, out int jSave)
        {
            I3DStorageObject sphere = new Sphere(id, radius, description, weight, isFragile);

            I3DStorageObject addedBox = BoxAddLoop(sphere, manualLocation, i, j, out iSave, out jSave);

            return addedBox;
        }

        //Försöker lägga till paket med blob

        public I3DStorageObject AddBlob(string id, short side, string description, short weight, bool manualLocation, int i, int j, out int iSave, out int jSave)
        {
            I3DStorageObject blob = new Blob(id, side, description, weight);

            I3DStorageObject addedBox = BoxAddLoop(blob, manualLocation, i, j, out iSave, out jSave);
            return addedBox;
        }

        public I3DStorageObject SearchPackageArray(string id, out int i, out int j)
        {
            i = 0;
            j = 0;


            for (i = 0; i < wareHouseStorage.GetLength(0); i++)
            {
                for (j = 0; j < wareHouseStorage.GetLength(1); j++)
                {

                    I3DStorageObject foundPackage = wareHouseStorage[i, j].SearchPackageList(id);

                    if (foundPackage != null)
                    {
                        return foundPackage;
                    }
                }
            }
            return null;
        }

        public I3DStorageObject MovePackage(string id, int i, int j, out int oldFloor, out int oldRow)
        {

            I3DStorageObject package = SearchPackageArray(id, out oldFloor, out oldRow);

            if (package != null)
            {
                package = DeletePackage(id, out oldFloor, out oldRow);
                

                I3DStorageObject newPackage = BoxAddLoop(package, true, i, j, out i, out j);

                if (newPackage != null)
                {
                    return newPackage;
                }
                else
                {
                    package = wareHouseStorage[oldFloor, oldRow].AddPackage(package);
                    return null;
                }
            }
            else return null;

        }


        public I3DStorageObject DeletePackage(string id, out int iSave, out int jSave)
        {

            for (int i = 0; i < wareHouseStorage.GetLength(0); i++)
            {
                for (int j = 0; j < wareHouseStorage.GetLength(1); j++)
                {

                    var deletedPackage = wareHouseStorage[i, j].DeletePackage(id);
                    if (deletedPackage != null)
                    {
                        iSave = i;
                        jSave = j;
                        return deletedPackage;
                    }
                }
            }
            iSave = 0;
            jSave = 0;
            return null;
        }

        public I3DStorageObject BoxAddLoop(I3DStorageObject box, bool manualLocation, int i, int j, out int iSave, out int jSave)
        {
            if (manualLocation == false)
            {
                for (i = 0; i < wareHouseStorage.GetLength(0); i++)
                {
                    for (j = 0; j < wareHouseStorage.GetLength(1); j++)
                    {
                        if (wareHouseStorage[i, j].WeightLeft >= box.Weight && wareHouseStorage[i, j].Width >= box.MaxDimension && wareHouseStorage[i, j].FragileBox == false && wareHouseStorage[i, j].VolumeLeft >= box.Volume)
                        {
                            if (box.IsFragile == true && wareHouseStorage[i, j].WeightLeft == 1000)
                            {
                                wareHouseStorage[i, j].AddPackage(box);
                                iSave = i;
                                jSave = j;
                                return box;
                            }
                            else if (box.IsFragile == false)
                            {
                                wareHouseStorage[i, j].AddPackage(box);
                                iSave = i;
                                jSave = j;
                                return box;
                            }
                        }

                    }
                }
                iSave = i;
                jSave = j;
                return null;
            }
            else
            {
                if (wareHouseStorage[i, j].WeightLeft >= box.Weight && wareHouseStorage[i, j].Width >= box.MaxDimension && wareHouseStorage[i, j].FragileBox == false && wareHouseStorage[i, j].VolumeLeft >= box.Volume)
                {
                    wareHouseStorage[i, j].AddPackage(box);
                    iSave = i;
                    jSave = j;
                    return box;
                }
                iSave = i;
                jSave = j;
                return null;
            }

        }
    }
}



