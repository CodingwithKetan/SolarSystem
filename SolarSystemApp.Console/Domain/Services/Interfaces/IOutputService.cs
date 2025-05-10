using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarSystemApp.Console.Domain.Services.Interfaces
{
    ///<summary>
    /// An output service that can show data from the Solar System OpenData API<see href="https://api.le-systeme-solaire.net/"/> to a user via the console. 
    ///</summary>
    public interface IOutputService
    {
        void OutputAllPlanetsAndTheirMoonsToConsole();
        void OutputAllMoonsAndTheirMassToConsole();
        void OutputAllPlanetsAndTheirAverageMoonGravityToConsole();
        void OutputAllThePlanetsWhichHasMoonAndAvgTemToConsole();
    }
}
