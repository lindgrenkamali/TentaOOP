using System;
using WareHouseLibrary;

namespace WareHouseProgram
{
    class WareHouseProgram
    {
        static void Main(string[] args)
        {

            bool loop = true;
            Console.WriteLine("Välkommen till ett lager");
            string choice;
            bool manualLocation = false;
            int i = 0;
            int j = 0;
            I3DStorageObject box;
            WareHouse wareHouse = new WareHouse();
            int iPosition = 0;
            int jPosition = 0;

            do
            {

                Console.WriteLine("\nTryck 1 för att lägga till låda på första lediga plats");
                Console.WriteLine("Tryck 2 för att lägga till låda på angiven plats");
                Console.WriteLine("Tryck 3 för att hitta låda med ID");
                Console.WriteLine("Tryck 4 för att ta bort angiven låda");
                Console.WriteLine("Tryck 5 för att flytta låda till angiven plats");
                Console.WriteLine("Tryck 6 för att avsluta programmet");
                choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        manualLocation = false;
                        box = BoxMethods.CreateFullBox(wareHouse, manualLocation, i, j, out iPosition, out jPosition);

                        if (box != null)
                        {
                            Console.WriteLine("\nEtt paket har lagts till med information: \n" + box.ToString() + "\n på våning: " + (iPosition + 1) + " plats: " + (jPosition + 1));

                        }
                        else
                        {
                            Console.WriteLine("\nPaketet har ej lagts till. Paketet kan ej tas emot av någon hylla");
                        }
                        break;

                    case "2":
                        Console.Clear();
                        manualLocation = true;
                        BoxMethods.BoxPosition(out i, out j);
                        box = BoxMethods.CreateFullBox(wareHouse, manualLocation, i, j, out iPosition, out jPosition);

                        if (box != null)
                        {
                            Console.WriteLine("\nEtt paket har lagts till med information: \n" + box.ToString() + "\n\npå våning: " + (iPosition + 1) + " plats: " + (jPosition + 1));

                        }
                        else
                        {
                            Console.WriteLine("\nPaketet har ej lagts till. Platsen kan innehålla något ömtåligt eller fylld");
                        }
                        break;

                    case "3":
                        Console.Clear();
                        string id = BoxMethods.CreateID();

                        box = wareHouse.SearchPackageArray(id, out i, out j);
                        if (box != null)
                        {
                            Console.WriteLine("\nPaketet hittades på våning: " + (i + 1) + "\nplats: " + (j + 1));
                            Console.WriteLine(box.ToString());
                        }
                        else
                        {
                            Console.WriteLine("\nPaketet hittades inte");
                        }

                        break;

                    case "4":
                        Console.Clear();
                        id = BoxMethods.CreateID();
                        box = wareHouse.DeletePackage(id, out iPosition, out jPosition);

                        if (box != null)
                        {
                            Console.WriteLine("Paketet:\n");
                            Console.WriteLine(box.ToString());
                            Console.WriteLine("\nhar tagits bort från");
                            Console.WriteLine("våning: " + (iPosition + 1));
                            Console.WriteLine("hylla: " + (jPosition+ 1));
                        }

                        else
                        {
                            Console.WriteLine("\nKunde inte hitta något paket med ID: " + id + " att ta bort");
                        }

                        break;

                    case "5":
                        Console.Clear();
                        id = BoxMethods.CreateID();
                        I3DStorageObject checkBox = wareHouse.SearchPackageArray(id, out iPosition, out jPosition);

                        if (checkBox == null)
                        {
                            Console.WriteLine("\nPaket med ID: " + id + " hittades inte. Återgår till menyn.\n");
                            break;
                        }
                        BoxMethods.BoxPosition(out i, out j);
                        box = wareHouse.MovePackage(id, i, j, out iPosition, out jPosition);

                        if (box != null)
                        {
                            Console.WriteLine(box.ToString() + " har flyttats från våning: " + (iPosition + 1) + " hylla: " + (jPosition + 1) + " till våning: " + (i + 1) + " hylla: " + (j + 1));

                        }

                        else
                        {
                            Console.WriteLine("\nPaket:\n\n" + checkBox.ToString() + " \n\nkunde inte flyttas och står kvar på våning: " + (iPosition + 1) + " hylla: " + (jPosition + 1));
                        }
                        break;


                    case "6":
                        Console.Clear();
                        Console.WriteLine("\nProgrammet avslutas efter knapptryck");
                        Console.ReadLine();
                        loop = false;
                        break;

                    default:
                        {
                            Console.Clear();
                            Console.WriteLine("\nOgitlig inmatning bara nummer mellan 1-6 är godkända");
                            break;
                        }
                }

            } while (loop == true);

        }
    }
}
