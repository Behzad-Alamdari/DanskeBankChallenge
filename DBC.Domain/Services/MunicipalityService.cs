using DBC.Infrastructure.DataAccess;
using DBC.Infrastructure.DataAccess.Repositories;
using DBC.Infrastructure.Services;
using DBC.Infrastructure.Utilities;
using DBC.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBC.Domain.Services
{
    public class MunicipalityService : IMunicipalityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMunicipalityRepository _municipalityRepository;
        private readonly ITaxCanculator _taxCanculator;

        public MunicipalityService(IUnitOfWork unitOfWork,
            IMunicipalityRepository municipalityRepository, ITaxCanculator taxCanculator)
        {
            _unitOfWork = unitOfWork;
            _municipalityRepository = municipalityRepository;
            _taxCanculator = taxCanculator;
        }

        public async Task<string> AddAsync(string name)
        {
            if (await _municipalityRepository.Exist(name))
                return $"A municipality with the name of \"{name}\" already exists";

            var municipality = new Municipality { Name = name };
            _municipalityRepository.Add(municipality);
            await _unitOfWork.CommitAsync();

            return $"Municipality of \"{name}\" added";
        }

        public async Task<Municipality> FindAsync(string name)
        {
            return await _municipalityRepository.GetAsync(m => m.Name == name);
        }

        public async Task<string> AddTaxRule(string municipalityName, TaxRule taxRule)
        {
            if (taxRule.Periods?.Any() != true)
                return $"Tax rule must at least has one period";

            var municipality = await _municipalityRepository.GetWithDetails(municipalityName);
            if (municipality == null)
                return $"No municipality with the name of \"{municipalityName}\" exists";

            if (municipality.TaxRules == null)
                municipality.TaxRules = new List<TaxRule>();

            municipality.TaxRules.Add(taxRule);

            await _unitOfWork.CommitAsync();

            return $"A tax rule has been added for \"{municipalityName}\"";
        }

        public async Task<float> FindApplicableTax(string municipalityName, DateTime date)
        {
            var municipality = await _municipalityRepository.GetWithDetails(municipalityName);
            if (municipality == null)
                return -1; //$"No municipality with the name of \"{municipalityName}\" exists";

            if (municipality.TaxRules?.Any() != true)
                return -1; // No rules is registered for this municipality

            var tax = _taxCanculator.CalculateTaxFor(municipality, date);

            return tax;
        }

        public async Task<List<TaxRule>> FindMunicipalityTaxRules(string municipalityName)
        {
            var municipality = await _municipalityRepository.GetWithDetails(municipalityName);
            return municipality.TaxRules.OrderBy(t => t.Priority).ToList();
        }
    }
}
