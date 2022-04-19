using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            double fuelCapacity;
            double fuelUsage;
            int type;
        s1:
            Console.WriteLine("Masinin yanacag tutumunu daxil edin:(50 den kicik 150 litrden boyuk ola bilmez)");
            bool fuelCapacityFormat = double.TryParse(Console.ReadLine(), out fuelCapacity);
            Console.WriteLine("Masinin 100km e serf etdiyi yanacag miqdarini daxil edin:(15 litrden boyuk ola bilmez)");
            bool fuelUsageFormat = double.TryParse(Console.ReadLine(), out fuelUsage);
            bool inValuecheck = FormatCheck(fuelCapacityFormat, fuelUsageFormat, fuelCapacity, fuelUsage);
            if (!inValuecheck)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("!!!!!!!!ERROR!!!!!!!-DEYERLERI DUZGUN GIRIN");
                Console.ForegroundColor = ConsoleColor.White;
                goto s1;
            }
            Car masin = new Car(fuelCapacity, fuelUsage);//Car sinifinden instance alindi.
            do
            {
        e1:
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine("Zehmet olmasa bir emeliyyat secin :");
                Console.WriteLine("1-Start The Car \n2-Drive \n3-TAddFuel \n4-Stop The Car \n5-Exit");
                Console.WriteLine("-------------------------------------------------------------------");
                bool typeFormat = int.TryParse(Console.ReadLine(), out type);
                inValuecheck = FormatCheck(typeFormat, type);
                if (!inValuecheck)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("!!!!!!!!ERROR!!!!!!!-TIPI DUZGUN GIRIN ");
                    Console.ForegroundColor = ConsoleColor.White;
                    goto e1;
                }

                switch (type)
                {
                    case 1:
                        masin.StartTheCar();
                        break;
                    case 3:
                        Console.WriteLine("Zehmet olmasa yanacag miqdarini daxil edin:");
                        double fuelAmount = int.Parse(Console.ReadLine());
                        masin.AddFuel(fuelAmount);
                        break;
                    case 2:
                        Console.WriteLine("Zehmet olmasa qet edilecek mesafeni daxil edin:");
                        double localkm = int.Parse(Console.ReadLine());
                        masin.Drive(localkm);
                        break;
                    case 4:
                        masin.StopTheCar();
                        break;
                    default:
                        break;
                }



            } while (type != 5);
        }

        //GIRILEN DEYERLERIN TESTI capasity and usage gore 
        private static bool FormatCheck(bool _fuelCapacityFormat, bool _fuelUsageFormat, double _fuelCapacity, double _fuelUsage)
        {
            if (_fuelCapacityFormat && _fuelUsageFormat && _fuelCapacity <= 150 && _fuelCapacity > 50 && _fuelUsage <= 15 && _fuelUsage > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //eyni metodun overload edilib ve girilen tipin test edir .geriye bool tipinde deyer gonderir .
        private static bool FormatCheck(bool _typeFormat, int _type)
        {
            if (_typeFormat && _type > 0 && _type <= 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }

    class Car
    {
        private double _fuelCapacity;
        private double _fuelUsage;
        public bool currentStatus { get; set; }
        public double currentFuel { get; set; }
        public double localKm { get; set; }
        public double globalKm { get; set; }
        public Car(double fuelCapacity, double fuelUsage)
        {
            this._fuelCapacity = fuelCapacity;
            this._fuelUsage = fuelUsage;
            this.currentFuel = 0;
            this.currentStatus = false;
        }


        public void StartTheCar()
        {
            if (currentStatus)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Masin artiq ise salinib");
                Console.WriteLine($"Yanacag Miqdari:{currentFuel}");
                Console.WriteLine($"Mumkun surus mesafesi:{(currentFuel * 100) / _fuelUsage} km");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            else if (currentFuel > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Masin Ise salindi!!");
                currentStatus = true;
                Console.WriteLine($"Yanacag Miqdari:{currentFuel}");
                Console.WriteLine($"Mumkun surus mesafesi:{(currentFuel * 100) / _fuelUsage} km");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Masin Ise salmaq ucun yeterince yanacaq yoxdur .Zehmet olmasa yanacag doldurun !!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void Drive(double _localKm)
        {
            if (!currentStatus)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Masin ise salinmayib!Masini ise salin!");
                Console.ForegroundColor = ConsoleColor.White;
            }
             else if (_localKm <= ((currentFuel * 100) / _fuelUsage))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                currentFuel = currentFuel - (_fuelUsage * _localKm) / 100;
                globalKm +=_localKm;
                Console.WriteLine($"Masin {_localKm} km mesafeni qet eledi .");
                Console.WriteLine($"Istifade edilen Yanacaq Midqari:{(_fuelUsage * _localKm) / 100} Litr");
                Console.WriteLine($"Qalan yanacaq miqdari:{currentFuel} litr");
                Console.WriteLine($"Umumi Surus mesafesi:{globalKm} km");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"!!{_localKm} km mesafeni qet etmek ucun kifayet qeder yanacaq yoxdur!!");
                Console.WriteLine($"Lazim olan yanacaq Miqdari :{(_fuelUsage * _localKm) / 100} litr");
                Console.WriteLine($"Faktiki yanacaq Miqdari :{currentFuel} litr");
                Console.ForegroundColor = ConsoleColor.White;
            }
            //150 km 
        }

        public void AddFuel(double _fuelAmount)
        {

            if (currentFuel + _fuelAmount > _fuelCapacity)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Daxil edilen miqdar yalnisdir.Yanacaq miqdari masinin yanacaq tutumunu asdi!");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            else
            {
                currentFuel = currentFuel + _fuelAmount;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Hazirki yanacaq ehtiyatiniz : {currentFuel} oldu.");
                Console.ForegroundColor = ConsoleColor.White;
            }

        }

        public void StopTheCar()
        {
            if (currentStatus)
            {
                Console.WriteLine("Masin ugurla sonduruldu!!");
                currentStatus = false;
            }
            else
            {
                Console.WriteLine("Masin artiq sondurulub!!");
            }

        }

    }

}


//Console ekranı açıldıqda adamdan bir əməliyyat daxil etməyi istəsin.Adam 5 tipdə
//əməliyyat seçə bilər.Bunlar aşağıdakılardır:
//1-Start The Car--Bu əməliyyatı seçdikdə mövcud yanacaq ehtiyatini desin.Əgər ki cari yanacaq 0 olsa bu haqda məlumat verilsin.
//2-Drive--Bu əməliyyatı seçdikdə gediləcək məsafəni soruşsun.Özünüz daxil edin Console.ReadLine() ilə.Məsələn 20 daxil edəndı
//yazılsın ki, siz 20km məsafə getdiniz.Ümumi yürüş məsafısini göstərsin başlanğıcdakı dəyəri ilə bunun cəmi yəni.
//Həmçinində son yanacaq ehtiyatınızı göstərsin.Yanacaq sərfiyyatına  görə gedilən yola uyğun cari yanacaq azaldılsın.
//3-TAddFuel --Bu əməliyyatı seçdikdə maşına yanacaq əlavə edilsin.Adamdan daxil ediləcək yanacaq miqdarını isdəsin,
//məsələn 10 daxil etdikdə son yanac miqdarı ilə 10 dəyərini cəmləsin və alınan dəyəri, Yanacaq miqdari: 20l şəklində ekrana yazdırın.
//4-Stop The Car--Bu əməliyyatı seçdikdə sizin ümumi yürüş məsafəniz ekranda yazılsın, həmçinində yeni sətrdən ən son maşında mövcud olan
//yanacaq miqdarı yazılsın.