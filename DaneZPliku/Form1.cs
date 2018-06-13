using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
|||||||||||||||||||||| Maciej Stępień       ||||||||||||||||||||||
|||||||||||||||||||||| UWM, ISI 3           ||||||||||||||||||||||
|||||||||||||||||||||| 2018r                ||||||||||||||||||||||
|||||||||||||||||||||| Sztuczna Inteligencja||||||||||||||||||||||
|||||||||||||||||||||| "Algorytm Apriori i  ||||||||||||||||||||||
||||||||||||||||||||||  Reguły Asocjacyjne" ||||||||||||||||||||||
||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

    Kod jest nieco chaotyczny ze względu na pisanie go z ręki bez projektu.
*/

namespace DaneZPlikuOkienko
{
    public struct AsoF2
    {
        public double ufn, wsp;
        public List<string> lista;
        public AsoF2(double u, double w, List<string> l)
        {
            this.ufn = u;
            this.wsp = w;
            this.lista = l;
        }
    }
    public struct Aso
    {
        public double ufn, wsp;
        public List<string> lista;
        public Aso(double u, double w,List<string> l)
        {
            this.ufn = u;
            this.wsp = w;
            this.lista = l;
        }
    }

    public struct C2Struct
    {
        public string String1;
        public string String2;

        public C2Struct(string s1, string s2)
        {
            this.String1 = s1;
            this.String2 = s2;

        }
    }

    public struct F1Struct
    {
        public string String;
        public int count;

        public F1Struct(string s)
        {
            this.String = s;
            this.count = 1;
           
        }
    }

    public partial class DaneZPliku : Form
    {
        List<F1Struct> ListF1Struct;
        List<C2Struct> ListC2Struct;
        List<C2Struct> ListF2Struct; //F2 uzywa tych samych typw danych co c2

        List<List<string>> ListFK;
        List<List<C2Struct>> ListC3;
        List<int> ListGoodC3Index;
        List<List<Aso>> ListAsoF3Calculated;
        List<List<List<string>>> ListAsoF3Pre;
        List<Aso> ListAsoF2Calculated;
        //List<double> ListWSPF3;
        //List<double> ListWSPF2;

        //List<double> ListUSNF3;
        //<double> ListUFNF2;


        int liczbaWierszyWParagonie; 


        private string[][] paragon;

        public DaneZPliku()
        {
            InitializeComponent();
            cbWarunek.SelectedIndex = 0;
        }

        private void btnWybierzParagon_Click(object sender, EventArgs e)
        {
            DialogResult wynikWyboruPliku = ofd.ShowDialog(); // wybieramy plik
            if (wynikWyboruPliku != DialogResult.OK)
                return;

            tbSciezkaDoParagonu.Text = ofd.FileName;
            string trescPliku = System.IO.File.ReadAllText(ofd.FileName); // wczytujemy treść pliku do zmiennej
            string[] wiersze = trescPliku.Trim().Split(new char[] { '\n' }); // treść pliku dzielimy wg znaku końca linii, dzięki czemu otrzymamy każdy wiersz w oddzielnej komórce tablicy
            paragon = new string[wiersze.Length][];   // Tworzymy zmienną, która będzie przechowywała wczytane dane. Tablica będzie miała tyle wierszy ile wierszy było z wczytanego poliku

            for (int i = 0; i < wiersze.Length; i++)
            {
                string wiersz = wiersze[i].Trim();     // przypisuję i-ty element tablicy do zmiennej wiersz
                string[] cyfry = wiersz.Split(new char[] { ' ' });   // dzielimy wiersz po znaku spacji, dzięki czemu otrzymamy tablicę cyfry, w której każda oddzielna komórka to czyfra z wiersza
                paragon[i] = new string[cyfry.Length];    // Do tablicy w której będą dane finalne dokładamy wiersz w postaci tablicy integerów tak długą jak długa jest tablica cyfry, czyli tyle ile było cyfr w jednym wierszu

                for (int j = 0; j < cyfry.Length; j++)
                {
                    string cyfra = cyfry[j].Trim(); // przypisuję j-tą cyfrę do zmiennej cyfra
                    paragon[i][j] = cyfra;  
                }
            }

            tbParagon.Text = TablicaDoString(paragon);
        }

        public string TablicaDoString<T>(T[][] tab)
        {
            string wynik = "";
            for (int i = 0; i < tab.Length; i++)
            {
                for (int j = 0; j < tab[i].Length; j++)
                {
                    wynik += tab[i][j].ToString() + " ";
                }
                wynik = wynik.Trim() + Environment.NewLine;
            }

            return wynik;
        }

        //Po klikniecie w pracuj
        private void btnStart_Click(object sender, EventArgs e)
        {

            //czyszczenie 
            tbWyniki.Text = "";
            ListF1Struct = new List<F1Struct>();
            ListC2Struct = new List<C2Struct>();
            ListF2Struct = new List<C2Struct>();
            ListFK = new List<List<string>>();
            ListC3 = new List<List<C2Struct>>();
            ListGoodC3Index = new List<int>();
            ListAsoF3Pre = new List<List<List<string>>>();
            ListAsoF3Calculated = new List<List<Aso>>();
            ListAsoF2Calculated = new List<Aso>();

            if (!LetsBetterCheckFirstWeHaveAnyDataToAnalize())
            {
                MessageBox.Show("Brak danych do analizy!");
                return;
            }
            //Budujemy zbiory zdarzen czestych
            F1();
            // Teraz kombinacje bez powtórzen elementów zbioru F1, tworza zbiór kandydatów,
            C2();

            F2();
            C3();

            //Oblicza liczbe wierszy w paragonie dla wszystkich funkcji WSP i UFN
            liczbaWierszyWParagonie = paragon.Count();

            //Sprawdza czy coś z tego C3 wyszło, jak nie to elo
            if(ListGoodC3Index.Count < 1)
            {
                tbWyniki.Text += Environment.NewLine + "Żaden ze zbiorów nie przeszedł selekcji :(";
                return;
            }

            //Wyswietla wszystko przed regułami
            //ShowBeforeAso();

            RegulyAsocjacyjne();

            //Wkoncu niech wypisze to wszystko i elo
            ProgJakosci();

        }

        private void ProgJakosci()
        {
            double prog = Math.Abs(Math.Round(ProgCase(), 2));
            Newline();
            tbWyniki.Text += "Po uwzglednieniu progu jakosci wsp * ufn = ­" + prog + " reguły o czestosci przynajmniej = " + progCzestosci.Value + " postac: " + Environment.NewLine;
            
            //dla f3
            foreach(var i in ListAsoF3Calculated)
            {
                foreach(var j in i)
                {
                    double tmp = j.ufn * j.wsp;
                    if(tmp >= prog)
                    {
                        ProgCaseShow(j);

                    }
                }
            }

            //dla f2
            foreach(var i in ListAsoF2Calculated)
            {
                double tmp = i.ufn * i.wsp;
                if (tmp >= prog)
                {
                    ProgCaseShow(i);

                }
            }

        }

        private void ProgCaseShow(Aso j)
        {
            for(int  i = 0;i < j.lista.Count; i++)
            {
                //dla dwojek
                if(j.lista.Count == 2)
                {
                    tbWyniki.Text += j.lista[0] + " => " + j.lista[1];
                    break;
                }
                if (i == j.lista.Count - 1)
                    tbWyniki.Text += j.lista[i];

                else
                    if(i == j.lista.Count - 2)
                    tbWyniki.Text += j.lista[i] + " => ";
                else
                    tbWyniki.Text += j.lista[i] + " ^ ";
            }
            tbWyniki.Text += " wsp * ufn = " + (j.ufn * j.wsp);
            Newline();
        }

        private double ProgCase()
        {
            switch (cbWarunek.SelectedIndex)
            {
                case 0:
                    return ((double)1 / (double)3);                    
                case 1:
                    return ((double)1 / (double)10);
                case 2:
                    return ((double)2 / (double)10);
                case 3:
                    return ((double)3 / (double)10);
                case 4:
                    return ((double)4 / (double)10);
                default:
                    return ((double)1 / (double)3);

            }

        }

        void RegulyAsocjacyjne()
        {
            //F3
            DoWSPF3List();
            CalculateF3Aso();
            CalculateF2Aso();


        }

        private void CalculateF2Aso()
        {
            Newline(); Newline(); //Wyrownanie ladne XD
            tbWyniki.Text += "Reguły Asocjacyjne F2: ";
            Newline();


            foreach (var i in ListF2Struct)
            {
                double Xwsp = 0;
                double Yufn = 0;
                foreach (var k in paragon)
                {
                    bool ok = true;

                    if (!k.Contains(i.String1))
                        ok = false;
                    if (!k.Contains(i.String2))
                        ok = false;


                    
                    if (ok) Xwsp += 1;
                }


                //Pora na ufność XD


                foreach (var k in paragon)
                {
                    
                    if(k.Contains(i.String1))
                        Yufn += 1;
                }



                //Wyswietlanie ladne


                tbWyniki.Text += i.String1 + " => " + i.String2;


                double wsp = Xwsp / liczbaWierszyWParagonie;
                double ufn = Math.Round((Xwsp / Yufn), 2);
                
                tbWyniki.Text += " wsp: = " + Xwsp + "/" + liczbaWierszyWParagonie + ", ufn = " + Xwsp + "/" + Yufn + ", wsp * ufn = (" + wsp + "*" + ufn + ") = " + (wsp * ufn);
                Newline();



                //zamieniam c2struct na liste stringowow bo takie dane przyjmuje struct aso
                List<string> tmp = new List<string>(new string [] {i.String1, i.String2 });

                //tu dodaje do listy aby wykorzystac do progow
                ListAsoF2Calculated.Add(new Aso(ufn, wsp, tmp));
            }
          

            //Wyswietlanie
            
        }

        private void CalculateF3Aso()
        {
            tbWyniki.Text += "Reguły Asocjacyjne F3: ";
            Newline();

            foreach (var i in ListAsoF3Pre)
            {
               List<Aso> lla = new List<Aso>();

                foreach (var j in i) //Wiersz z tego co w dowspff3 zrobilismy
                {

                    double Xwsp = 0; //zapisuje liczebnik wsp, mianownik to poprostu liczba wierszy w paragonie
                    double Yufn = 0; //zapisuje mianownik wfn, liczbenik to xwsp
                    //Najpierw liczymy wsp

                    //Dla kazdego wiersza z wylicznego przeszukaj wszystkie wiersze zbioru D
                    foreach (var k in paragon)
                    {
                        bool ok = true;
                        foreach (var m in j)
                        {
                            if (!k.Contains(m))
                            {
                                ok = false;
                            }
                        }
                        if (ok) Xwsp += 1;
                    }


                    //Pora na ufność XD
                    foreach (var k in paragon)
                    {
                        bool ok = true;
                        for(int m =0;m < j.Count -1;m++)
                        {
                           // tbWyniki.Text += j[m] + " ";
                            if (!k.Contains(j[m]))
                            {
                                ok = false;
                            }
                            
                        }
                        //Newline();
                        if (ok) Yufn += 1;

                    }

                    

                    Newline();
                    for (int l = 0; l < j.Count; l++)
                    {
                        if (!(l == j.Count - 1))
                            if (l == j.Count - 2) tbWyniki.Text += j[l];
                            else
                                tbWyniki.Text += j[l] + " ^ ";
                        else
                            tbWyniki.Text += " => " + j[l];

                    }
                    double wsp = Xwsp / liczbaWierszyWParagonie;
                    double ufn = Math.Round((Xwsp / Yufn), 2);
                    tbWyniki.Text += " wsp: = " + Xwsp + "/" + liczbaWierszyWParagonie + ", ufn = " + Xwsp + "/" + Yufn + ", wsp * ufn = (" + wsp + "*" + ufn + ") = " + Math.Round((wsp * ufn),2);
                    


                    //Po znalezieniu ufn i wsp, warto dodac te dane do listy dizeki temu mozemy to pozniej wykorzystac do roznych progow
                    lla.Add(new Aso(ufn, wsp, j));
                   


                }
                ListAsoF3Calculated.Add(lla);

            }








        }

        private void DoWSPF3List()
        {
            List<List<string>> kopia = ListFK;

            //Dla kazdej listy ktora przeszla selekcje
            foreach (var i in ListGoodC3Index)
            {
                List<List<string>> lls = DoWSPF3List_A1(kopia[i]);



                //break;
                ListAsoF3Pre.Add(lls);
            }
        }
        private List<List<string>> DoWSPF3List_A1(List<string> list)
        {
            List<List<string>> tmp = new List<List<string>>();

            for(int i = 0; i < list.Count; i++)
            {

                string tmp2 = list[0];
                list.RemoveAt(0);
                list.Add(tmp2);

                List<string> tmp3 = new List<string>();
                foreach (var j in list) tmp3.Add(j);

                
                tmp.Add(tmp3);
            }

            return tmp;
        }

        void C3_D()
        {
            int elements;
            


            foreach (var i in ListGoodC3Index)
            {
                elements = ListFK[i].Count;

                foreach (var j in ListFK[i])
                {

                }
            }
        }
        void C3()
        {
            
            List<string> candidates = new List<string>();
            
            List<C2Struct> tmpStruct = new List<C2Struct>();

            //Czy na pierwszym miejscu wyestepuje wiecej niz 2 razy w ogole
            foreach (var i in ListF2Struct)
            {
                //jezeli tak dodaj do listy kandydatow do dalszego suzkania
                if (C3_1(i.String1))
                {
                    if(!candidates.Contains(i.String1))
                    candidates.Add(i.String1);
                }
                
            }

            //utworz z stringow w pomocniczej listy kandydatow
            foreach (var i in candidates)

            {
                List<string> tmpList = new List<string>();
                tmpList.Add(i); //Dodaj 1 stringa
                foreach (var j in ListF2Struct)
                {
                    if (i == j.String1 && !tmpList.Contains(j.String2))
                    {
                        tmpList.Add(j.String2);
                    }
                }

                ListFK.Add(tmpList);
            }

            for (int i =0; i <  ListFK.Count;i++)
            {
                tmpStruct = new List<C2Struct>();
                foreach (var j in ListFK[i])
                {
                    foreach (var k in ListFK[i])
                    {
                        
                        //sprawdz czy taka para sie juz nei pojawila
                       if(C3_2(j,k, tmpStruct)) //dopisz tu

                        if (j != k)
                               
                                    tmpStruct.Add(new C2Struct(j,k));
                    }
                }
                ListC3.Add(tmpStruct);
            }

            //Selekcja z c3
            C3_3();
            //Ostateczne apriro czy cos
            C3_D();

            /*tbWyniki.Text += Environment.NewLine;
            foreach (var i in candidates)
            {
                tbWyniki.Text += i + " ";
            }*/

        }
        bool C3_1(string s)
        {
            int howMany = 0;

            foreach (var i in ListF2Struct)
            {
                if (i.String1 == s) howMany += 1;
            }


            if(howMany >= 2) return true;
            else return false;
            
        }
        //Czysci liste z powtarzajacych sie dwojek
        bool C3_2(string s1, string s2, List<C2Struct> i)
        {
            //s1 is string1 , s2 is string2
            // i to przesylana lista w ktorej znajduja sie wpisane juz dwojki
            
            

             foreach(var j in i)
            {
                if (j.String1 == s2 && j.String2 == s1)
                    return false;
            }
            

            return true;
        }
        void C3_3()
        {
            
            for(int i =0;i < ListC3.Count; i++)
            {
                bool isWrongInList = false;

                foreach (C2Struct j in ListC3[i]) //lista struktow, sprawdz czy ktorys z nich nie jest zawarty w f2, jezeli tak 
                {
                    if(!ListF2Struct.Exists(x => x.String1 == j.String1 && x.String2 == j.String2))
                    {
                        isWrongInList = true;
                        continue;
                        
                    }
                    
                       
                    

                }

                if (!isWrongInList) ListGoodC3Index.Add(i);
                


            }

        }
        bool LetsBetterCheckFirstWeHaveAnyDataToAnalize()
        {
            bool tmp = true;
            if (tbParagon.Text == "" || tbParagon.Text == null)
                if (paragon == null) tmp = false;
                else tbParagon.Text = TablicaDoString(paragon);

            return tmp;
        }
        void C2()
        {
            foreach(var i in ListF1Struct)
            {
                foreach(var j in ListF1Struct)
                {
                    if(i.String != j.String)                       
                    ListC2Struct.Add(new C2Struct(i.String, j.String));
                }
            }

        }
        void F1()
        {
            foreach (var i in paragon)
            {
                foreach (var j in i)
                    ExistInF1(j);

                    
            }
          
            List<F1Struct> tmp = ListF1Struct;
            ListF1Struct = tmp.OrderBy(o => o.String).ToList();
            //Usuwa wszystkie elelemnty z listy ktore maja prog mniejszy niz x
            DeleteOn((int)progCzestosci.Value);

        }
        void F2()
        {
            int howMany;
            bool first; 
            bool second;

            foreach(var i in ListC2Struct)
            {
                howMany = 0;
                

                foreach (var j in paragon) //Każdy wiersz w danych poczatkowych
                {
                    first = false;
                    second = false;

                    foreach (var k in j )//Kazdy element wierasza
                    {
                        if(i.String1 == k)
                        {
                            first = true;
                        }
                        if(i.String2 == k)
                        {
                            second = true;
                        }
                    }

                    if (first && second) howMany += 1;
                        


                }

                if (howMany >= 2) ListF2Struct.Add(new C2Struct(i.String1, i.String2));

            }
        }        
        void ExistInF1(string y)
        {
            if (ListF1Struct.Exists(x => x.String == y))
            {
                F1Struct result = ListF1Struct.Find(x => x.String == y);
                result.count += 1;

                var rs = ListF1Struct.Single(r => r.String == y);
                ListF1Struct.Remove(rs);
                ListF1Struct.Add(result);
                

            }
            else
                ListF1Struct.Add(new F1Struct(y));
        }
        void DeleteOn(int x)
        {
            List<F1Struct> lista = new List<F1Struct>();
            for(int i =0; i < ListF1Struct.Count; i++)
            {
                if (ListF1Struct[i].count >= x)
                {
                    lista.Add(ListF1Struct[i]);
                    
                }
                
            }
            ListF1Struct = lista;
        }
        void ShowBeforeAso()
        {


            tbWyniki.Text += "F1: ";
            foreach (var i in ListF1Struct)
            {
                tbWyniki.Text += i.String + ": " + i.count + " ";
            }

            Newline(); Newline();
            tbWyniki.Text += "C2: ";

            foreach(var i in ListC2Struct)
            {
                tbWyniki.Text += "{ " + i.String1 + ", " + i.String2 + " } ";
            }

            Newline(); Newline();
            tbWyniki.Text += "F2: ";

            foreach (var i in ListF2Struct)
            {
                tbWyniki.Text += "{ " + i.String1 + ", " + i.String2 + " } ";
            }

            Newline(); Newline(); 
            tbWyniki.Text += "Fk: ";

            foreach (var i in ListFK)
            {
                tbWyniki.Text += "{ ";
                foreach (var j in i)
                {
                    //Dzieki ifowi nie daje przecinak gdy koniec listy
                    if (j == i.Last()) { tbWyniki.Text += j; }
                    else tbWyniki.Text += j + ", ";

                }
                tbWyniki.Text += " } ";
            }

            Newline(); Newline();
            tbWyniki.Text += "Ck: ";

            foreach (var i in ListC3)
            {
                
                foreach (var j in i)
                {
                    tbWyniki.Text += "{ " + j.String1 + ", " + j.String2 + " } ";
                }
                Newline(); Newline();
            }




            /*
                        tbWyniki.Text += "Indeksy Fk które przeszły selekcję zbioru C3: ";

                        foreach (var i in ListGoodC3Index)  
                        {
                            tbWyniki.Text += i + " ";

                        }*/

            tbWyniki.Text += "Fk które przeszły selekcję zbioru C3: ";
            foreach (var i in ListGoodC3Index)
            {
                Newline(); Newline();
                foreach (var j in ListFK[i])
                {



                    tbWyniki.Text += j + " ";
                    
                    
                }
                

            }



        }
        //Funkcja ktora dodaje do tbwyniki nowa linie, krocej sie pisze new line niz tbwyniki.text += nenvasdasd .newline za kazdym razem
        void Newline()
        {
            tbWyniki.Text += Environment.NewLine;
        }
    }
}
