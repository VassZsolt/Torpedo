using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NationalInstruments.Torpedo.Model
{
    public record class Result(
        string FirstPlayer,
        string SecondPlayer,
        string Winner,
        int NumberOfRounds
        );
}
