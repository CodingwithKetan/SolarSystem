using SolarSystemApp.Console.Constants;
using SolarSystemApp.Console.Domain.Objects;
using SolarSystemApp.Console.Domain.Services.Interfaces;
using SolarSystemApp.Console.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarSystemApp.Console.Domain.Services
{
    public class ScreenOutputService : IOutputService
    {
        private readonly IPlanetService _planetService;

        private readonly IMoonService _moonService;

        public ScreenOutputService(IPlanetService planetService, IMoonService moonService)
        {
            _planetService = planetService;
            _moonService = moonService;
        }

        public void OutputAllPlanetsAndTheirMoonsToConsole()
        {
            //The service gets all the planets from the API.
            var planets = _planetService.GetAllPlanets().ToArray();

            //If the planets aren't found, then the function stops and tells that to the user via the console.
            if (!planets.Any())
            {
                System.Console.WriteLine(OutputString.NoPlanetsFound);
                return;
            }

            //The column sizes and labels for the planets are configured here. 
            var columnSizesForPlanets = new[] { 20, 20, 30, 20 };
            var columnLabelsForPlanets = new[]
            {
                OutputString.PlanetNumber, OutputString.PlanetId, OutputString.PlanetSemiMajorAxis,
                OutputString.TotalMoons
            };

            //The column sizes and labels for the moons are configured here.
            //The second moon's column needs the 2 extra '-' characters so that it's aligned with the planet's column.
            var columnSizesForMoons = new[] { 20, 70 + 2 };
            var columnLabelsForMoons = new[]
            {
                OutputString.MoonNumber, OutputString.MoonId
            };

            //The for loop creates the correct output.
            for (int i = 0, j = 1; i < planets.Length; i++, j++)
            {
                //First the line is created.
                ConsoleWriter.CreateLine(columnSizesForPlanets);

                //Under the line the header is created.
                ConsoleWriter.CreateText(columnLabelsForPlanets, columnSizesForPlanets);

                //Under the header the planet data is shown.
                ConsoleWriter.CreateText(
                    new[]
                    {
                        j.ToString(), CultureInfoUtility.TextInfo.ToTitleCase(planets[i].Id),
                        planets[i].SemiMajorAxis.ToString(),
                        planets[i].Moons.Count.ToString()
                    },
                    columnSizesForPlanets);

                //Under the planet data the header for the moons is created.
                ConsoleWriter.CreateLine(columnSizesForPlanets);
                ConsoleWriter.CreateText(columnLabelsForMoons, columnSizesForMoons);

                //The for loop creates the correct output.
                for (int k = 0, l = 1; k < planets[i].Moons.Count; k++, l++)
                {
                    ConsoleWriter.CreateText(
                        new[]
                        {
                            l.ToString(), CultureInfoUtility.TextInfo.ToTitleCase(planets[i].Moons.ElementAt(k).Id)
                        },
                        columnSizesForMoons);
                }

                //Under the data the footer is created.
                ConsoleWriter.CreateLine(columnSizesForMoons);
                ConsoleWriter.CreateEmptyLines(2);

                /*
                    This is an example of the output for the planet Earth:
                    --------------------+--------------------+------------------------------+--------------------
                    Planet's Number     |Planet's Id         |Planet's Semi-Major Axis      |Total Moons
                    10                  |Terre               |0                             |1
                    --------------------+--------------------+------------------------------+--------------------
                    Moon's Number       |Moon's Id
                    1                   |La Lune
                    --------------------+------------------------------------------------------------------------
                */
            }
        }

        public void OutputAllMoonsAndTheirMassToConsole()
        {
            //The function works the same way as the PrintAllPlanetsAndTheirMoonsToConsole function. You can find more comments there.
            var moons = _moonService.GetAllMoons().ToArray();

            if (!moons.Any())
            {
                System.Console.WriteLine(OutputString.NoMoonsFound);
                return;
            }

            var columnSizesForMoons = new[] { 20, 20, 30, 20 };
            var columnLabelsForMoons = new[]
            {
                OutputString.MoonNumber, OutputString.MoonId, OutputString.MoonMassExponent, OutputString.MoonMassValue
            };

            ConsoleWriter.CreateHeader(columnLabelsForMoons, columnSizesForMoons);

            for (int i = 0, j = 1; i < moons.Length; i++, j++)
            {
                ConsoleWriter.CreateText(
                    new[]
                    {
                        j.ToString(), CultureInfoUtility.TextInfo.ToTitleCase(moons[i].Id),
                        moons[i].MassExponent.ToString(), moons[i].MassValue.ToString()
                    },
                    columnSizesForMoons);
            }

            ConsoleWriter.CreateLine(columnSizesForMoons);
            ConsoleWriter.CreateEmptyLines(2);

            /*
                This is an example of the output for the moon around the earth:
                --------------------+--------------------+------------------------------+--------------------
                Moon's Number       |Moon's Id           |Moon's Mass Exponent          |Moon's Mass Value
                --------------------+--------------------+------------------------------+--------------------
                1                   |Lune                |22                            |7,346             
                ...more data...
                --------------------+--------------------+------------------------------+--------------------
             */
        }

        public void OutputAllPlanetsAndTheirAverageMoonGravityToConsole()
        {
            //The function works the same way as the PrintAllPlanetsAndTheirMoonsToConsole function. You can find more comments there.
            var planets = _planetService.GetAllPlanets().ToArray();
            if (!planets.Any())
            {
                System.Console.WriteLine(OutputString.NoMoonsFound);
                return;
            }

            var columnSizes = new[] { 20, 30 };
            var columnLabels = new[]
            {
                OutputString.PlanetId, OutputString.PlanetMoonAverageGravity
            };


            ConsoleWriter.CreateHeader(columnLabels, columnSizes);

            foreach (Planet planet in planets)
            {
                if (planet.HasMoons())
                {
                    ConsoleWriter.CreateText(new string[] { $"{planet.Id}", $"{planet.AverageMoonGravity}" }, columnSizes);
                }
                else
                {
                    ConsoleWriter.CreateText(new string[] { $"{planet.Id}", $"-" }, columnSizes);
                }
            }

            ConsoleWriter.CreateLine(columnSizes);
            ConsoleWriter.CreateEmptyLines(2);

            /*
                --------------------+--------------------------------------------------
                Planet's Number     |Planet's Average Moon Gravity
                --------------------+--------------------------------------------------
                1                   |0.0f
                --------------------+--------------------------------------------------
            */
        }

        public void OutputAllThePlanetsWhichHasMoonAndAvgTemToConsole()
        {
            var allPlanets = _planetService.GetAllPlanets().ToArray();
            var planets = allPlanets.Where(_ => _.HasMoons()).ToArray();

            //If the planets aren't found, then the function stops and tells that to the user via the console.
            if (!planets.Any())
            {
                System.Console.WriteLine(OutputString.NoPlanetsFound);
                return;
            }

            var columnSizesForPlanets = new[] { 20, 20, 20 };
            var columnLabelsForPlanets = new[] { OutputString.PlanetNumber, OutputString.PlanetId, OutputString.TotalMoons };

            var columnSizesForMoons = new[] { 20, 20, 20 };
            var columnLabelsForMoons = new[]
            { OutputString.MoonNumber, OutputString.MoonId, OutputString.MoonAverageTemperature
            };

            for (int i = 0, j = 1; i < planets.Length; i++, j++)
            {
                ConsoleWriter.CreateLine(columnSizesForPlanets);
                ConsoleWriter.CreateText(columnLabelsForPlanets, columnSizesForPlanets);
                ConsoleWriter.CreateText(
                    new[]
                    {
                j.ToString(),
                CultureInfoUtility.TextInfo.ToTitleCase(planets[i].Id),
                planets[i].Moons.Count.ToString()
                    },
                    columnSizesForPlanets);

                ConsoleWriter.CreateLine(columnSizesForPlanets);
                ConsoleWriter.CreateText(columnLabelsForMoons, columnSizesForMoons);

                for (int k = 0, l = 1; k < planets[i].Moons.Count; k++, l++)
                {
                    var moon = planets[i].Moons.ElementAt(k);
                    string avgTempStr = moon.AvgTemp.ToString();

                    ConsoleWriter.CreateText(
                        new[]
                        {
                    l.ToString(),
                    CultureInfoUtility.TextInfo.ToTitleCase(moon.Id),
                    avgTempStr
                        },
                        columnSizesForMoons);
                }

                ConsoleWriter.CreateLine(columnSizesForMoons);
                ConsoleWriter.CreateEmptyLines(2);

                /*
               This is an example of the output for the planet with moon and it's avg temp:
               --------------------+--------------------+------------------------------+--------------------
               Planet's Number     |Planet's Id         |Total Moons
               10                  |Terre               |1
               --------------------+--------------------+------------------------------+--------------------
               Moon's Number       |Moon's Id           |Moon's Avg Temp 
               1                   |La Lune             | 0 
               --------------------+------------------------------------------------------------------------
           */
            }
        }
    }
}
