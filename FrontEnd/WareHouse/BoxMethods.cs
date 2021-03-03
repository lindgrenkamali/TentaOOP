using System;
using System.Text.RegularExpressions;
using WareHouseLibrary;

namespace WareHouseProgram
{
    public class BoxMethods
    {
        internal static I3DStorageObject CreateFullBox(WareHouse wareHouse, bool manualLocation, int i, int j, out int iSave, out int jSave)
        {
            iSave = 0;
            jSave = 0;
            int tempI;
            int tempJ;
            string shape = BoxMethods.ShapeOfBox();

            string id = BoxMethods.CreateID();

            I3DStorageObject existId = wareHouse.SearchPackageArray(id, out tempI, out tempJ);
            if (existId != null)
            {
                Console.WriteLine("\nEtt paket med ID: " + id + " finns redan. Återgår till menyn.");
                return null;
            }

            short side = 0;
            short[] cuboidSides = new short[3];
            short radius = 0;
            bool isFragile = false;
            I3DStorageObject added;


            if (shape == "kub")
            {
                side = BoxMethods.Side();

                if (side == 0)
                {
                    return null;
                }
            }

            else if (shape == "rätblock")
            {
                cuboidSides = BoxMethods.CuboidSides();

                if (cuboidSides[0] == 0 || cuboidSides[1] == 0 || cuboidSides[2] == 0)
                {
                    return null;
                }
            }

            else if (shape == "sfär")
            {
                radius = BoxMethods.SphereRadius();

                if (radius == 00)
                {
                    return null;
                }
            }

            else if (shape == "blob")
            {
                side = BoxMethods.Side();

                if (side == 0)
                {
                    return null;
                }
            }

            string description = BoxMethods.BoxDescription();

            short weight = BoxMethods.BoxWeight();
            if (weight == -1)
            {
                Console.WriteLine("\nPaketet väger ingenting. Återgår till menyn.");
                return null;

            }

            else if (weight == 0)
            {
                Console.WriteLine("\nPaketet väger för mycket. Återgår till menyn.");
                return null;
            }

            if (shape == "blob")
            {
                isFragile = true;
            }

            else
            {
                isFragile = BoxMethods.BoxFragile();
            }

            //Används ej
            //int insuranceValue = BoxMethods.BoxValue();

            if (shape == "kub")
            {
                added = wareHouse.AddCube(id, side, description, weight, isFragile, manualLocation, i, j, out iSave, out jSave);
                return added;
            }

            else if (shape == "rätblock")
            {
                added = wareHouse.AddCuboid(id, cuboidSides[0], cuboidSides[1], cuboidSides[2], description, weight, isFragile, manualLocation, i, j, out iSave, out jSave);
                return added;
            }

            else if (shape == "sfär")
            {
                added = wareHouse.AddSphere(id, radius, description, weight, isFragile, manualLocation, i, j, out iSave, out jSave);
                return added;
            }

            else if (shape == "blob")
            {
                iSave = 0;
                jSave = 0;
                added = wareHouse.AddBlob(id, side, description, weight, manualLocation, i, j, out iSave, out jSave);
                return added;
            }
            else return null;
        }

        internal static string ShapeOfBox()
        {
            string shape;
            bool shapeValid = false;
            do
            {
                Console.WriteLine("\nSkriv in form (kub, rätblock, sfär, blob) på paketet");
                shape = Console.ReadLine();

                shape = shape.ToLower();

                if (shape == "kub")
                {
                    shapeValid = true;
                    return shape;

                }

                else if (shape == "rätblock")
                {

                    shapeValid = true;
                    return shape;

                }

                else if (shape == "sfär")
                {
                    shapeValid = true;
                    return shape;

                }

                else if (shape == "blob")
                {
                    shapeValid = true;
                    return shape;

                }

                else
                {
                    Console.WriteLine("\nOgitlig form. Försök igen.");
                }

            } while (shapeValid == false);

            return shape;
        }



        internal static string CreateID()
        {

            string id;

            bool loopID = true;
            do
            {
                Console.WriteLine("\nSkriv in paketets ID med 7 tecken. Bara bokstäver (A-Z) och siffror.");
                id = Console.ReadLine();
                id = id.ToUpper();

                loopID = true;


                Regex regex = new Regex(@"[A-Z+0-9+\-\/\*\(\)]");
                MatchCollection matches = regex.Matches(id);


                if (id.Length == 7 && matches.Count == 7)
                {
                    loopID = false;
                }

                else
                { Console.WriteLine("\nOgitligt format på ID:et, försök igen"); }

            } while (loopID == true);

            return id;
        }


        internal static short Side()
        {
            try
            {
                Console.WriteLine("\nHur lång är sidans (cm) paket? Tillåtna dimensioner 1 - 228 cm)");
                short cubeSide = Int16.Parse(Console.ReadLine());

                if (cubeSide > 0 && cubeSide <= 228)
                {
                    return cubeSide;
                }

                else if (cubeSide > 228)
                {
                    Console.WriteLine("\nPaketet är alldeles för stort! Återgår till menyn.");
                    return 0;
                }

                else
                {
                    Console.WriteLine("\nPaketet är alldeles för litet. Återgår till menyn.");
                    return 0;
                }

            }
            catch (OverflowException)
            {
                Console.WriteLine("\nBara värden mellan 1 - 228 är tillåtna. Försök igen");
                short catchSide = Side();
                return catchSide;
            }

            catch (FormatException)
            {
                Console.WriteLine("\nBara siffror får användas vid inmatning. Försök igen");
                short catchSide = Side();
                return catchSide;
            }


        }

        internal static short[] CuboidSides()
        {
            try
            {

                Console.WriteLine("\nPaketet får max ha en volym på 12 m3 och en sida på max 300 cm. Skriv in paketets längd (cm)");
                short xSide = Int16.Parse(Console.ReadLine());

                if (xSide < 0)
                {
                    Console.WriteLine("\nPaketet har en för kort längd. Återgår till menyn");
                    return null;
                }

                else if (xSide > 300)
                {
                    Console.WriteLine("\nPaketet har en för lång längd. Återgår till menyn");
                    return null;
                }


                Console.WriteLine("\nSkriv in paketets bredd (cm)");
                short ySide = Int16.Parse(Console.ReadLine());

                if (ySide < 0)
                {
                    Console.WriteLine("\nPaketet har en för kort bredd. Återgår till menyn");
                    return null;
                }

                else if (ySide > 300)
                {
                    Console.WriteLine("\nPaketet har en för lång bredd. Återgår till menyn");
                    return null;
                }

                Console.WriteLine("\nSkriv in paketets höjd (cm)");
                short zSide = Int16.Parse(Console.ReadLine());

                if (zSide < 0)
                {
                    Console.WriteLine("\nPaketet har en för kort höjd. Återgår till menyn");
                    return null;
                }

                else if (zSide > 300)
                {
                    Console.WriteLine("\nPaketet har en för lång höjd. Återgår till menyn");
                    return null;
                }
                else
                {
                    short[] cuboidSides = { xSide, ySide, zSide };
                    return cuboidSides;
                }

            }
            catch (OverflowException)
            {
                Console.WriteLine("\nAlldeles för stor inmatning. Försök igen");
                short[] cuboidSides = CuboidSides();
                return cuboidSides;
            }
            catch (FormatException)
            {
                Console.WriteLine("\nFör låg eller för hög inmatning. Försök igen");
                short[] cuboidSides = CuboidSides();
                return cuboidSides;
            }
        }

        internal static short SphereRadius()
        {
            try
            {
                Console.WriteLine("\nHur stor radie har sfären? (MAX 114 cm)");
                short radius = Int16.Parse(Console.ReadLine());

                if (radius > 0 && radius <= 114)
                {
                    return radius;
                }

                else if (radius > 114)
                {
                    Console.WriteLine("\nPaketet är alldeles för stort!");
                    return 0;
                }

                else
                {
                    Console.WriteLine("\nPaketet är alldeles för litet");
                    return 0;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\nBara siffror får användas vid inmatning. Försök igen");
                short radius = SphereRadius();
                return radius;


            }
            catch (OverflowException)
            {
                Console.WriteLine("\nBara värden mellan 1 - 114 är tillåtna. Försök igen");
                short radius = SphereRadius();
                return radius;
            }

        }

        internal static string BoxDescription()
        {
            string boxDesc;
            bool descLoop = false;
            do
            {
                Console.WriteLine("\nSkriv in beskrivningen på paketet");
                boxDesc = Console.ReadLine();
                if (string.IsNullOrEmpty(boxDesc))
                {
                    return boxDesc;
                }
            } while (descLoop == true);
            return boxDesc;
        }

        internal static short BoxWeight()
        {
            try
            {
                bool loopWeight = true;
                do
                {

                    Console.WriteLine("\nHur mycket (kg) väger paketet? 1 - 1000 kg");
                    short boxWeight = Int16.Parse(Console.ReadLine());

                    if (boxWeight > 0 && boxWeight <= 1000)
                    {
                        return boxWeight;
                    }

                    else if (boxWeight > 1000)
                    {

                        return 0;
                    }

                    else
                    {


                        return -1;
                    }
                } while (loopWeight == true);
            }

            catch (FormatException)
            {
                Console.WriteLine("\nBara siffror får användas vid inmatning.");
                short catchWeight = BoxWeight();
                return catchWeight;
            }
            catch (OverflowException)
            {
                Console.WriteLine("Bara värden mellan 1-1000 är tillåtna!");
                short catchWeight = BoxWeight();
                return catchWeight;
            }



        }

        internal static bool BoxFragile()
        {
            bool loopFragile = false;
            string input;
            bool isFragile = false;

            do
            {
                Console.WriteLine("\nÄr lådan ömtålig? (ja eller nej)");
                input = Console.ReadLine();
                input = input.ToLower();

                if (input == "ja")
                {
                    isFragile = true;

                }
                else if (input == "nej")
                {
                    isFragile = false;

                }

            } while (loopFragile == true);

            return isFragile;

        }

        //Kod som inte används
        /*internal static int BoxValue()
        {
            try
            {
                bool loopValue = true;
                do
                {

                    Console.WriteLine("\nVad är värdet på paketet (kr)?");
                    int boxValue = int.Parse(Console.ReadLine());
                    if (boxValue <= 0)
                    {
                        Console.WriteLine("\nPaketet måste ha ett positivtvärde. Försök igen");
                    }

                    else
                    {
                        loopValue = false;
                    }

                    return boxValue;
                } while (loopValue == true);
            }
            catch (FormatException)
            {
                Console.WriteLine("\nSiffror får bara användas vid in matning av värdet. Försök igen");
                int catchValue = BoxValue();
                return catchValue;
            }

            catch (OverflowException)
            {
                Console.WriteLine("\nDu har angett ett värde som är för litet eller för stort. Försök igen");
                int catchValue = BoxValue();
                return catchValue;
            }
        }
        */

           internal static void BoxPosition(out int i, out int j)
            {
                try
                {
                    bool floorValid = false;
                    bool rowValid = false;
                    do
                    {
                        Console.WriteLine("\nVilken våning skall paketet läggas på? 1-3");
                        i = int.Parse(Console.ReadLine()) - 1;

                    if (i >= 0 && i <= 2)
                    {
                        floorValid = true;

                    }
                    else
                    { 
                        Console.WriteLine("\nFelaktig inmatning. Bara nummer mellan 1 - 3. Försök igen");
                }
                } while (floorValid == false);

                    do
                    {
                        Console.WriteLine("\nVilken hylla skall paketet läggas på? 1 - 100");
                        j = int.Parse(Console.ReadLine()) - 1;
                    if(j >= 0 && j <= 99)
                    {
                        rowValid = true;
                    }
                    else
                    {
                        Console.WriteLine("\nFelaktig inmatning. Bara nummer mellan 1 - 100. Försök igen");
                    }
                    } while (rowValid == false || floorValid == false);
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nDu har angett värden som inte är tillåtna. Försök igen");
                    BoxPosition(out i, out j);

                }
                catch (OverflowException)
                {
                    Console.WriteLine("\nDu har angett ett för stort värde eller för litet. Försök igen");
                    BoxPosition(out i, out j);
                }


            }

        }


    }
