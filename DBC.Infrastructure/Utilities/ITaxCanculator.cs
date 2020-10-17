using DBC.Models;
using System;

namespace DBC.Infrastructure.Utilities
{
    public interface ITaxCanculator
    {
        (float, string) CalculateTaxFor(Municipality municipality, DateTime date);
    }
}