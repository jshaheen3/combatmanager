using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace CombatManager
{
    public class WeatherGenerator
    {
        // Created from http://www.d20pfsrd.com/gamemastering/environment/weather
        
        private Random rnd;
        private WeatherBlockCreator wb;
        public delegate void LinkHander(object sender, DocumentLinkEventArgs e);

        LinkHander _LinkHandler;

        private int wind;
        private int low, high;

        public WeatherGenerator(LinkHander linkHandler)
        {
            rnd = new Random();
            _LinkHandler = linkHandler;
        }

        public enum Climate { Cold = 0, Temperate, Desert, MAX };
        public enum Season { Spring = 0, Summer, Autumn, Winter, MAX };

        public void AddClimateFilters(ComboBox filterComboBox)
        {
            for (int i = 0; i < (int)Climate.MAX; i++ ) {
                ComboBoxItem bi = new ComboBoxItem();
                bi.Name = ((Climate)i).ToString();
                bi.Content = ((Climate)i).ToString();
                if (i == 1)
                    bi.IsSelected = true;
                filterComboBox.Items.Add(bi);
            }
        }

        public void AddSeasonFilters(ComboBox filterComboBox)
        {
            filterComboBox.Items.Clear();
            for (int i = 0; i < (int)Season.MAX; i++)
            {
                ComboBoxItem bi = new ComboBoxItem();
                bi.Name = ((Season)i).ToString();
                bi.Content = ((Season)i).ToString();
                if (i == 0)
                    bi.IsSelected = true;
                filterComboBox.Items.Add(bi);
            }
        }

 
        public void Update(FlowDocument document, int cl, int sea, int num = 1)
        {
            Climate climate = (Climate)cl;
            Season season = (Season)sea;
            
            wb = new WeatherBlockCreator(document, _LinkHandler);
            
            switch (climate)
            {
                case Climate.Cold:
                    CreateColdWeather();
                    break;

                case Climate.Desert:
                    CreateDesertWeather();
                    break;

                case Climate.Temperate:
                    CreateTemperateWeather(season);
                    break;

                default:
                    // climate.ToString() + " " + season.ToString() + "\n";
                    break;
            }

            document.Blocks.AddRange(wb.getBlocks());
        }

        private int getHotTemp()
        {
            return rnd.Next(85, 110 + 1);
        }

        private int getWarmTemp()
        {
            return rnd.Next(60, 85 + 1);
        }

        private int getModerateTemp()
        {
            return rnd.Next(40, 60 + 1);
        }

        private int getColdTemp()
        {
            return rnd.Next(0, 40 + 1);
        }

        /* Weather for Temperate Climates.  Weather is set by the season */
        private void CreateTemperateWeather(Season season)
        {
            wb.AddTitle("Temperate Climate");

            int die = rnd.Next(1, 100 + 1);
            if (die <= 70)
                TemperateNormal(season);
            else if (die <= 80)
                TemperateAbnormal(season);
            else if (die <= 90)
                TemperateInclement(season);
            else if (die <= 99)
                TemperateStorm(season);
            else
                TemperatePowerfulStorm(season);
        }

        private void TemperatePowerfulStorm(Season season)
        {
            /*
             * Very high winds and torrential precipitation reduce visibility to zero, making Perception checks and all ranged weapon attacks impossible. 
             * Unprotected flames are automatically extinguished, and protected flames have a 75% chance of being doused. 
             * Creatures caught in the area must make a Fortitude save or face the effects based on the size of the creature (see Table: Wind Effects). 
             * Powerful storms are divided into the following four types.
            */

            // TODO: Finish this section

            high = getTempBySeason(season);
            low = high - rnd.Next(10, 20 + 1);

            wind = rnd.Next(50, 74 + 1);
            switch (season)
            {
                case Season.Winter:
                    wb.AddBoldBlock("Blizzard");
                    wb.AddTempWind(high, low, wind);
                    WeatherBlizzard();
                    break;

                case Season.Spring:
                    wb.AddBoldBlock("Windstorm");
                    //1d6 hours
                    wb.AddTempWind(high, low, wind);
                    WindStormWinds();
                    break;

                case Season.Summer:
                    wb.AddBoldBlock("Hurricane");
                    wind = rnd.Next(75, 174 + 1);
                    wb.AddTempWind(high, low, wind);
                    //  Hurricane: In addition to very high winds and heavy rain, hurricanes are accompanied by floods. Most adventuring activity is impossible under such conditions.
                    //Hurricanes can last for up to a week, but their major impact on characters comes in a 24 - to - 48 - hour period when the center of the storm moves through their area.
                    WindHurricane();
                    break;

                case Season.Autumn:
                    wb.AddBoldBlock("Tornado");
                    // Tornadoes are very short-lived(1d6 × 10 minutes), typically forming as part of a thunderstorm system.
                    wind = rnd.Next(175, 300 + 1);
                    wb.AddTempWind(high, low, wind);
                    Tornado();
                    break;
            }
        }

        private void TemperateStorm(Season season)
        {
            high = getTempBySeason(season);
            low = high - rnd.Next(10, 20 + 1);
            wind = rnd.Next(30, 50 + 1);

            if (low <= 30)
            {
                wb.AddBoldBlock("Snowstorm");
                wb.AddTempWind(high, low, wind);
                WeatherSnowStorm();
            }
            else
            {
                wb.AddBoldBlock("Thunderstorm");
                wb.AddTempWind(high, low, wind);
                WeatherThunderStorm();

                int tornado = rnd.Next(1, 10 + 1);
                if (tornado <= 1)
                    Tornado();
            }
        }

        private void TemperateInclement(Season season)
        {
            high = getTempBySeason(season);
            low = high - rnd.Next(10, 20 + 1);
            NormalWeather(high, low);
            
            int die = rnd.Next(1, 100 + 1);
            if (die <= 30)
                WeatherFog();
            else if (die <= 90)
            {
                if (low <= 30)
                    WeatherSnow();
                else
                    WeatherRain();
            }
            else
            {
                if (low <= 30)
                    WeatherSleet();
                else
                    WeatherHail();
            }
        }

        private void WeatherSnow()
        {
            int time = rnd.Next(1, 4 + 1) + rnd.Next(1, 4 + 1);
            int inches = rnd.Next(1, 6 + 1);
            string txt = "";
            txt += "Precipitation: ";
            txt += time + " hours of snow producing " + inches + ((inches == 1) ? " inch" : " inches") + " of snow.\n\n";   
            txt += "Snow reduces visibility ranges by half, resulting in a –4 penalty on ranged weapon attacks and perception checks. " +
                   "\n\nIt costs 2 squares of movement to enter a snow-covered square.\n\n" +
                   "Snow will automatically extinguish unprotected flames (candles, torches, and the like). " +
                   "Protected flames (such as those of lanterns) will have a 50% chance of being extinguished.\n";
            wb.AddBlock(txt);
        }

        private void WeatherSnowStorm()
        {
            int time = rnd.Next(1, 4 + 1) + rnd.Next(1, 4 + 1) - 1;
            int inches = rnd.Next(1, 6 + 1);
            string txt = "";
            txt += "Precipitation: ";
            txt += time + ((time == 1) ? " hour" : " hours") + " of snow producing " + inches + ((inches == 1) ? " inch" : " inches") + " of snow.\n\n";
            txt += "Snow reduces visibility ranges by three-quarters, resulting in a –8 penalty on perception checks. " +
                   "The snow storm makes ranged weapon attacks impossible, except for those using siege weapons, which have a –4 penalty on attack rolls. " +
                   "\n\nIt costs 2 squares of movement to enter a snow-covered square.\n\n" +
                   "Snow will automatically extinguish unprotected flames (candles, torches, and the like). " +
                   "Protected flames (such as those of lanterns) will have a 50% chance of being extinguished.";
            wb.AddBlock(txt);
            WindCheckedSize("tiny", "", -2);
        }

        private void WeatherBlizzard()
        {
            string txt = "";
            txt += "Precipitation: ";
            int feet = rnd.Next(1, 3 + 1);
            int time = rnd.Next(1, 3 + 1);
            int drift = rnd.Next(1, 4 + 1) * 5;
            txt += time + ((time == 1) ? " day" : " days") + " of heavy snow producing " + feet + ((feet == 1) ? " foot " : " feet ") + "of snow\n\n";
            txt += "The blizzard obscures all sight beyond 5 feet, including darkvision. " +
                  "Creatures 5 feet away have concealment (attacks by or against them have a 20% miss chance). " +
                  "The snow storm makes ranged weapon attacks impossible, except for those using siege weapons, which have a –4 penalty on attack rolls. " +
                  "\n\nIt costs 4 squares of movement to enter a snow-covered square.\n\n" +
                  "Snow drifts will form snow up to " + drift + " feet deep, especially in and around objects big enough to deflect the wind—a cabin or a large tent, for instance.\n\n" +
                  "Snow will automatically extinguish unprotected flames (candles, torches, and the like). Protected flames (such as those of lanterns) will have a 50% chance of being extinguished.";

            wb.AddBlock(txt);
            WindCheckedSize("medium", "small", -8);
        }

        private void WeatherThunderStorm()
        { 
            int time = rnd.Next(1, 4 + 1) + rnd.Next(1, 4 + 1);
            string txt = "";
            txt += "Precipitation: ";
            txt += time + " hours of heavy rain.\n\n";
            txt += "The heavy rain reduces visibility ranges by three-quarters, resulting in a –8 penalty on perception checks. ";
            wb.AddBlock(txt);
            WeatherLightning();
            WindStormWinds();
        }

        private void WeatherRain()
        {
            int time = rnd.Next(1, 4 + 1) + rnd.Next(1, 4 + 1);
            string txt = "";
            txt += "Precipitation: ";
            txt += time + " hours of rain.\n\n";
            txt += "Rain reduces visibility ranges by half, resulting in a –4 penalty on perception checks.\n";
            wb.AddBlock(txt);
        }

        private void WeatherHail()
        {
            int time = rnd.Next(1, 4 + 1);
            int hail = rnd.Next(1, 20 + 1);
            string txt = "";
            txt += "Precipitation: ";
            txt += time + ((time == 1) ? "hour" : "hours") + " of rain with " + hail + ((hail == 1) ? "minute" : "minutes") + " of hail.\n\n";
            txt += "Hail does not reduce visibility, but the sound of falling hail makes sound-based perception checks more difficult (–4 penalty). ";
            int chance = rnd.Next(1, 100 + 1);
            if (chance <= 5)
                txt += "The hail is large enough to deal 1 point of lethal damage (per storm) to anything in the open.\n";
            txt += "Once on the ground,  it costs 2 squares of movement to enter a hail-covered square. ";
            wb.AddBlock(txt);
        }

        private void WeatherFog()
        {
            int time = rnd.Next(1, 4 + 1) + rnd.Next(1, 4 + 1);
            string txt = "";
            txt += "Precipitation: ";
            txt += time + " hours of fog.\n\n";
            txt += "Whether in the form of a low-lying cloud or a mist rising from the ground, fog obscures all sight beyond 5 feet, including darkvision.\n" +
                   "Creatures 5 feet away have concealment (attacks by or against them have a 20% miss chance).";
            wb.AddBlock(txt);
        }

        private void WeatherSleet()
        {
            int time = rnd.Next(1, 4 + 1) + rnd.Next(1, 4 + 1);
            string txt = "";
            txt += "Precipitation: ";
            txt += time + " hours of sleet.\n\n";
            txt += "Sleet is essentially frozen rain reducing visibility ranges by half, " +
                   "resulting in a –4 penalty on ranged weapon attacks and perception checks.\n" +
                   "\n\nIt costs 2 squares of movement to enter a sleet-covered square.\n\n" +
                   "Sleet will automatically extinguish unprotected flames (candles, torches, and the like). " +
                   "Protected flames (such as those of lanterns) will have a 75% chance of being extinguished.\n";
            wb.AddBlock(txt);
        }

        private void WeatherLightning()
        {
            wb.AddBlock("During the one-hour period at the storm's centre (decrease this as the PCs move away from the centre) " +
                        "there is the risk of being struck by lightning. As a rule of thumb, assume one bolt per minute. Each bolt causes between 4d8 and 10d8 electricity damage.\n");
        }

        private void TemperateAbnormal(Season season)
        {
            high = getTempBySeason(season);
            low = high - rnd.Next(10, 20 + 1);
            AbnormalWeather(high, low);
        }

        private void TemperateNormal(Season season)
        {
            high = getTempBySeason(season);
            low = high - rnd.Next(10, 20 + 1);
            NormalWeather(high, low);
        }

        private int getTempBySeason(Season season)
        {
            switch (season)
            {
                case Season.Autumn:
                case Season.Spring:
                    high = getModerateTemp();
                    break;

                case Season.Winter:
                    high = getColdTemp();
                    break;

                case Season.Summer:
                    high = getWarmTemp();
                    break;
            }

            return high;
        }

        /* Weather for Desert Climates */
        private void CreateDesertWeather()
        {
            wb.AddTitle("Desert Climate");
            high = getHotTemp();
            low = high - rnd.Next(10, 20 + 1);

            int die = rnd.Next(1, 100 + 1);
            if (die <= 70)
                NormalWeather(high, low);
            else if (die <= 90)
                DesertHotWindy(high, low);
            else if (die <= 99)
                DesertDuststorm(high, low);
            else
                DesertDownpour(high, low);
        }

  
        private void DesertDownpour(int high, int low)
        {
            string txt = "";
            wb.AddBoldBlock("Downpour");
            wind = rnd.Next(10, 30 + 1);

            wb.AddTempWind(high, low, wind);

            int time = rnd.Next(1, 4 + 1) + rnd.Next(1, 4 + 1);

            txt += "The downpour lasts for " + time + ((time == 1) ? " hour." : " hours.") + "\n";
            // Can create floods.  Don't know what chance that is....
                     
            txt += "The downpour obscures all sight beyond 5 feet, including darkvision, resulting in a –4 penalty on ranged weapon attacks and perception checks. " +
                   "Creatures 5 feet away have concealment (attacks by or against them have a 20% miss chance)\n" +
                   "The downpour will automatically extinguish unprotected flames (candles, torches, and the like). " +
                   "Protected flames (such as those of lanterns) will have a 50% chance of being extinguished.\n";
            
            wb.AddBlock(txt);
        }

        private void DesertDuststorm(int high, int low)
        {
            string txt = "";
            int sand;
            int time = rnd.Next(1, 4 + 1) + rnd.Next(1, 4 + 1) - 1;
            txt += "Duststorm (CR 3): The duststorm blows fine grains of sand that obscure vision, smother unprotected flames, and can even choke protected flames (50% chance).\n";
            txt += "The duststorm lasts for " + time + ((time == 1) ? " hour" : " hours") + " and ";
            
            if (rnd.Next(1, 100 + 1) >= 91) 
            {
                wind = rnd.Next(51, 74 + 1);
                wb.AddBoldBlock("Greater Dust Storm");
                sand = rnd.Next(1, 3 + 1) + rnd.Next(1, 3 + 1) - 1;
                txt += "leaves behind " + sand + ((sand == 1) ? " foot " : " feet ") + "of sand.\n";
            }
            else
            {
                wind = rnd.Next(30, 50 + 1);
                wb.AddBoldBlock("Dust Storm");
                sand = rnd.Next(1, 6 + 1);
                txt += "leaves behind " + sand + " inch" + ((sand == 1) ? " " : "es ") + "of sand.\n";
            }

            wb.AddTempWind(high, low, wind);
            wb.AddBlock(txt);
            if (wind >= 51)
            {
                wb.AddBlock("These greater duststorms deal 1d3 points of nonlethal damage each round to anyone caught out in the open without shelter and also pose a choking hazard (see Drowning), except that a character with a scarf or similar protection across her mouth and nose does not begin to choke until after a number of rounds equal to 10 + her Constitution score).");
            }
        }

        private void DesertHotWindy(int high, int low)
        {
            wb.AddBoldBlock("Hot and Windy");
            wind = rnd.Next(10, 30 + 1);

            wb.AddTempWind(high, low, wind);
            if (wind <= 21)
                WindModerate();
            else
                WindStrong();
        }

        private void WindLight()
        {
            // A gentle breeze, having little or no game effect.
        }

        private void WindModerate()
        {
            wb.AddBlock("A steady wind with a 50% chance of extinguishing small, unprotected flames, such as candles.");
        }

        private void WindStrong()
        {
            wb.AddBlock("Gusts that automatically extinguish unprotected flames (candles, torches, and the like). Such gusts impose a –2 penalty on ranged attack rolls and on perception checks.");
            WindCheckedSize("tiny", "", -2);
        }

        private void WindSevere()
        {
            wb.AddBlock("In addition to automatically extinguishing any unprotected flames, winds of this magnitude cause protected flames (such as those of lanterns) to dance wildly and have a 50% chance of extinguishing these lights. Ranged weapon attacks and perception checks are at a –4 penalty. This is the velocity of wind produced by a gust of wind spell.");
            WindCheckedSize("small", "tiny", -4);
        }


        private void WindStormWinds()
        {
            wb.AddBlock("Powerful enough to bring down branches if not whole trees, windstorms automatically extinguish unprotected flames and have a 75% chance of blowing out protected flames, such as those of lanterns. Ranged weapon attacks are impossible, and even siege weapons have a –4 penalty on attack rolls. Perception checks that rely on sound are at a –8 penalty due to the howling of the wind.");
            WindCheckedSize("medium", "small", -8);
        }

        private void WindHurricane()
        {
            wb.AddBlock("All flames are extinguished. Ranged attacks are impossible (except with siege weapons, which have a –8 penalty on attack rolls). Perception checks based on sound are impossible: all characters can hear is the roaring of the wind. Hurricane-force winds often fell trees.");
            WindCheckedSize("large", "medium", -12);
        }

        private void Tornado()
        {
            wb.AddBlock("Tornado (CR 10): All flames are extinguished. All ranged attacks are impossible (even with siege weapons), as are sound-based perception checks. Instead of being blown away, characters in close proximity to a tornado who fail their Fortitude saves are sucked toward the tornado. Those who come in contact with the actual funnel cloud are picked up and whirled around for 1d10 rounds, taking 6d6 points of damage per round, before being violently expelled (falling damage might apply). While a tornado's rotational speed can be as great as 300 mph, the funnel itself moves forward at an average of 30 mph (roughly 250 feet per round). A tornado uproots trees, destroys buildings, and causes similar forms of major destruction.");
        }

        private void WindCheckedSize(string chk, string blownaway, int flyPenalty)
        {
            string txt = "";
            txt += "\nCreatures of " + chk + " size or smaller are unable to move forward against the force of the wind unless they succeed on a DC 10 Strength check (if on the ground) or a DC " + (20 - flyPenalty) + " fly skill check if airborne.";
            if (blownaway != "")
            {
                txt += "\n\nCreatures of " + blownaway + " size or smaller on the ground are knocked prone and rolled 1d4 × 10 feet, taking 1d4 points of nonlethal damage per 10 feet, unless they make a DC 15 Strength check. " +
                       "Flying creatures are blown back 2d6 × 10 feet and take 2d6 points of nonlethal damage due to battering and buffeting, unless they succeed on a DC " + (25 - flyPenalty) + " fly skill check.";
            }
            wb.AddBlock(txt);
        }

        /* Weather for Cold Climate */
        private void CreateColdWeather()
        {
            wb.AddTitle("Cold Climate");
            int die = rnd.Next(1, 100 + 1);

            high = getColdTemp();
            low = high - rnd.Next(10, 20 + 1);

            if (die <= 70)
                NormalWeather(high, low);
            else if (die <= 80)
                AbnormalWeather(high, low);
            else if (die <= 90)
                ColdInclement(high, low);
            else if (die <= 99)
                ColdStorm(high, low);
            else
                ColdPowerfulStorm(high, low);
        }

        private void ColdPowerfulStorm(int high, int low)
        {
            wind = rnd.Next(50, 74 + 1);
            
            wb.AddBoldBlock("Powerful Storm");
            wb.AddTempWind(high, low, wind);

            WeatherBlizzard();

            int lightning = rnd.Next(1, 100 + 1);
            if (lightning <= 10)
                WeatherLightning();
       }

       private void ColdStorm(int high, int low)
       {
            wind = rnd.Next(30, 50 + 1);
  
            wb.AddBoldBlock("Stormy Weather");
            wb.AddTempWind(high, low, wind);

            WeatherSnowStorm();
        }

        private void ColdInclement(int high, int low)
        {
            wind = rnd.Next(0, 10 + 1);
            int time = rnd.Next(1, 4 + 1) + rnd.Next(1, 4 + 1);
            
            wb.AddBoldBlock("Inclement Weather");
            wb.AddTempWind(high, low, wind);

            int die = rnd.Next(1, 100 + 1);
         
            if (die <= 30)
                WeatherFog();
            else if (die <= 90)
                WeatherSnow();
            else
                WeatherSleet();
            
        }

        private void AbnormalWeather(int high, int low)
        {
            string txt = "";
            wind = rnd.Next(0, 10 + 1);
            wb.AddBoldBlock("Abnormal Weather");

            int wave = rnd.Next(1, 100 + 1);
            if (wave <= 30) 
            {
                wb.AddBoldBlock("Heat Wave");
                high += 20;
                low += 20;
            }
            else  
            {
                wb.AddBoldBlock("Cold Snap");
                high -= 20;
                low -= 20;
            }
            wb.AddTempWind(high, low, wind);
            wb.AddBlock(txt);
        }

        private void NormalWeather(int high, int low)
        {
            wind = rnd.Next(0, 10 + 1);
            wb.AddBoldBlock("Normal Weather");
            wb.AddTempWind(high, low, wind);
        }

     

        private class WeatherBlockCreator : BlockCreator
        {
            /* Inner class to write text to the document area */

            private List<Block> blocks;
            private LinkHander _LinkHandler;        
               
            public WeatherBlockCreator(FlowDocument document, LinkHander linkHandler)
                : base(document)
            {
                blocks = new List<Block>();
                document.Blocks.Clear();
                _LinkHandler = linkHandler;
            }

            public List<Block> getBlocks()
            {
                return blocks;
            }

            public void AddTitle(string name)
            {
                blocks.Add(CreateHeaderParagraph(name));
            }

            void link_ToolTipOpening(object sender, ToolTipEventArgs e)
            {
                Hyperlink l = (Hyperlink)sender;
                ((ToolTip)l.ToolTip).DataContext = l.Tag;
            }

            void link_Click(object sender, RoutedEventArgs e)
            {
                if (_LinkHandler != null)
                {
                    string rule = (string)((Hyperlink)sender).DataContext;
                    _LinkHandler(this, new DocumentLinkEventArgs(rule, "Rule"));
                }
            }

            public void AddBoldBlock(string txt)
            {
                Span s = new Span();

                Paragraph details = new Paragraph();
                details.Margin = new Thickness(0, 4, 0, 0);

                s.Inlines.Add(new Bold(new Run(txt)));
                details.Inlines.Add(s);

                blocks.Add(details);
            }

            public void AddBlock(string txt)
            {
                Span s = new Span();

                Paragraph details = new Paragraph();
                details.Margin = new Thickness(0, 4, 0, 0);

                // Array of words to link to in the rules tab
                string[] words = { "perception", "movement", "concealment", "fly" };
                string[] split = txt.Split(' ');
                foreach (string str in split)
                {
                    if (words.Any(str.ToLower().Contains))
                    {
                        foreach (string word in words)
                        {
                            if (str == word)
                            {
                                Hyperlink link = AddRuleLink(str);
                                s.Inlines.Add(link);
                                s.Inlines.Add(new Run(" "));
                            }
                        }
                    }
                    else
                        s.Inlines.Add(new Run(str + " "));
                }

                details.Inlines.Add(s);
                blocks.Add(details);
            }

            private Hyperlink AddRuleLink(string name)
            {
                Rule rule = Rule.Rules.FirstOrDefault(a => String.Compare(a.Name, name, true) == 0);

                Hyperlink link = new Hyperlink(new Run(rule.Name));
                link.Click += new RoutedEventHandler(link_Click);
                link.DataContext = rule.Name;

                if (rule != null)
                {
                    link.Tag = rule;
                    ToolTip t = (ToolTip)App.Current.MainWindow.FindResource("ObjectToolTip");

                    if (t != null)
                    {

                        ToolTipService.SetShowDuration(link, 360000);
                        link.ToolTip = t;
                        link.ToolTipOpening += new ToolTipEventHandler(link_ToolTipOpening);

                    }
                }
                return link;
            }

            public void AddTempWind(int high, int low, int wind)
            {
                string txt = "";
                txt += "Daytime High: " + high + "°F";
                txt += " \tNighttime Low: " + low + "°F";
                txt += "\nWind Speed: " + wind + " mph\n";

                Span s = new Span();

                Paragraph p = new Paragraph();
                p.Margin = new Thickness(0, 4, 0, 0);

                s.Inlines.Add(new Run(txt));
                p.Inlines.Add(s);

                blocks.Add(p);
            }
        }
    
    }
}
