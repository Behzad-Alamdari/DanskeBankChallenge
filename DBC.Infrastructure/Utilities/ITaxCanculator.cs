using DBC.Models;
using System;

namespace DBC.Infrastructure.Utilities
{
    public interface ITaxCanculator
    {
        float CalculateTaxFor(Municipality municipality, DateTime date);
    }
}