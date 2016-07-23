using CombatManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Controls;

namespace CombatManager
{
    public class NameGenerator
    {
        private Random rnd;
        private static int MIN_LETTERS = 3;

        public NameGenerator()
        {
            rnd = new Random();
        }

        public enum SexFilter : int
        {
            Male = 0,
            Female = 1,
            Max
        }

        public enum RaceFilter : int
        {
            Dwarf = 0,
            Elf,
            Gnome,
            Half_Elf,
            Half_Orc,
            Halfling,
            Random,
            Max
        }

        /* From Players Guide for now. Would like to build names based on set of rules. */
        public static string[] Dwarf_MaleNames = new string[] { "Dolgrin", "Grunyar", "Harsk", "Kazmuk", "Morgrym", "Rogar" };
        public static string[] Dwarf_FemaleNames = new string[] { "Agna", "Bodill", "Ingra", "Kotri", "Rusilka", "Yangrit" };
        public static string[] Elf_MaleNames = new string[] { "Caladrel", "Heldalel", "Lanliss", "Meirdrarel", "Seldlon", "Talathel", "Variel", "Zordlon" };
        public static string[] Elf_FemaleNames = new string[] { "Amrunelara", "Dardlara", "Faunra", "Jathal", "Merisiel", "Oparal", "Soumral", "Tessara", "Yalandlara" };
        public static string[] Gnome_MaleNames = new string[] { "Abroshtor", "Bastargre", "Halungalom", "Krolmnite", "Poshment", "Zarzuket", "Zatqualmie" };
        public static string[] Gnome_FemaleNames = new string[] { "Besh", "Fijit", "Lini", "Neji", "Majet", "Pai", "Queck", "Trig" };
        public static string[] HalfElf_MaleNames = new string[] { "Calathes", "Encinal", "Kyras", "Narciso", "Quiray", "Satinder", "Seltyiel", "Zirul" };
        public static string[] HalfElf_FemaleNames = new string[] { "Cathran", "Elsbeth", "Iandoli", "Kieyanna", "Lialda", "Maddela", "Reda", "Tamarie" };
        public static string[] HalfOrc_MaleNames = new string[] { "Ausk", "Davor", "Hakak", "Kizziar", "Makoa", "Nesteruk", "Tsadok" };
        public static string[] HalfOrc_FemaleNames = new string[] { "Canan", "Drogheda", "Goruza", "Mazon", "Shirish", "Tevaga", "Zeljka" };
        public static string[] Halfling_MaleNames = new string[] { "Antal", "Boram", "Evan", "Jamir", "Kaleb", "Lem", "Miro", "Sumak" };
        public static string[] Halfling_FemaleNames = new string[] { "Anafa", "Bellis", "Etune", "Filiu", "Lissa", "Marra", "Rillka", "Sistra", "Yamyra" };

         

        public void AddTypeFilters(ComboBox nameTypeFilterComboBox)
        {
            for (int i = 0; i < (int)RaceFilter.Max; i++)
            {
                ComboBoxItem bi = new ComboBoxItem();
                bi.Name = ((RaceFilter)i).ToString();
                bi.Content = ((RaceFilter)i).ToString();
                if (i == 0)
                    bi.IsSelected = true;
                nameTypeFilterComboBox.Items.Add(bi);
            }
        }

        public void AddGenderFilters(ComboBox nameSecondFilterComboBox)
        {
            nameSecondFilterComboBox.Items.Clear();
            for (int i = 0; i < (int)SexFilter.Max; i++)
            {
                ComboBoxItem bi = new ComboBoxItem();
                bi.Name = ((SexFilter)i).ToString();
                bi.Content = ((SexFilter)i).ToString();
                if (i == 0)
                    bi.IsSelected = true;
                nameSecondFilterComboBox.Items.Add(bi);
            }
        }

        public void AddNumberFilters(ComboBox nameSecondFilterComboBox)
        {
            nameSecondFilterComboBox.Items.Clear();
            for (int i = MIN_LETTERS; i <= 10; i++)
            {
                ComboBoxItem bi = new ComboBoxItem();
                bi.Content = i.ToString() + " Letters";
                if (i == MIN_LETTERS)
                    bi.IsSelected = true;

                nameSecondFilterComboBox.Items.Add(bi);
            }
        }


        public string Update(int race, int num)
        {
            SexFilter sf = (SexFilter)num;
            RaceFilter rf = (RaceFilter)race;

            switch (rf)
            {
                case RaceFilter.Dwarf:
                    if (sf == SexFilter.Male)
                        return (GenerateName(Dwarf_MaleNames));
                    else
                        return (GenerateName(Dwarf_FemaleNames));

                case RaceFilter.Elf:
                    if (sf == SexFilter.Male)
                        return (GenerateName(Elf_MaleNames));
                    else
                        return (GenerateName(Elf_FemaleNames));

                case RaceFilter.Gnome:
                    if (sf == SexFilter.Male)
                        return (GenerateName(Gnome_MaleNames));
                    else
                        return (GenerateName(Gnome_FemaleNames));

                case RaceFilter.Half_Elf:
                    if (sf == SexFilter.Male)
                        return (GenerateName(HalfElf_MaleNames));
                    else
                        return (GenerateName(HalfElf_FemaleNames));

                case RaceFilter.Half_Orc:
                    if (sf == SexFilter.Male)
                        return (GenerateName(HalfOrc_MaleNames));
                    else
                        return (GenerateName(HalfOrc_FemaleNames));

                case RaceFilter.Halfling:
                    if (sf == SexFilter.Male)
                        return (GenerateName(Halfling_MaleNames));
                    else
                        return (GenerateName(Halfling_FemaleNames));

                case RaceFilter.Random:
                    return (GenerateRandomName(num));
                    
                default:
                    break;

            }
            return (rf.ToString() + "\n" + sf.ToString());
        }

        private string GenerateName(string[] namelist)
        {
            int i = rnd.Next(namelist.Count());
            return (namelist[i]);
        }


        public static char[] letter = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        public static char[] consonant = new char[] { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z' };
        public static char[] vowel = new char[] { 'a', 'e', 'i', 'o', 'u' };

        private string GenerateRandomName(int num)
        {
            num += MIN_LETTERS;
            string name = "";
            int r;

            for (int i = 0; i < num; i++)
            {
                r = rnd.Next(letter.Count()); // Lets pick a letter
                if (name.Length == 0)
                {
                    name += letter[r];
                }
                else if (name.Length == 1)
                {
                    // If the first letter is a vowel, the make the second a consonant
                    if (vowel.Contains(name[0]))
                    {
                        r = rnd.Next(consonant.Count());
                        name += consonant[r];
                    }
                    else
                    {
                        r = rnd.Next(vowel.Count());
                        name += vowel[r];
                    }
                }
                else
                {
                    int prev = i - 1;
                    int prevprev = prev - 1;

                    // check if letters are the same in a row
                    // if we have 2 consonats in a row, add a vowel
                    if (consonant.Contains(name[prev]) && consonant.Contains(name[prevprev]))
                    {
                        r = rnd.Next(vowel.Count());
                        name += vowel[r];
                        continue;
                    }

                    name += letter[r];
                    
                }
            }
            name = name.Substring(0,1).ToUpper() + name.Substring(1);
            return (name);
        }

    }
}
