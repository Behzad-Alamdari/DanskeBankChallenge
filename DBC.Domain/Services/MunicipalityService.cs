using DBC.Infrastructure.DataAccess;
using DBC.Infrastructure.DataAccess.Repositories;
using DBC.Infrastructure.Domains;
using DBC.Infrastructure.Utilities;
using DBC.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace DBC.Domain.Services
{
    public class MunicipalityService : IMunicipalityDomainLogic
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

        public async Task<(Municipality addedMunicipality, string error)> AddAsync(string name)
        {
            if (await _municipalityRepository.Exist(name))
                return (null, Messages.SameMunicipalityExist);

            var municipality = new Municipality { Name = name };
            _municipalityRepository.Add(municipality);
            await _unitOfWork.CommitAsync();

            return (municipality, null);
        }

        public async Task<Municipality> GetAsync(Guid id)
        {
            return await _municipalityRepository.GetAsync(m => m.Id == id);
        }

        public async Task<(float percentage, string message)> FindApplicableTax(Guid municipalityId, DateTime date)
        {
            var municipality = await _municipalityRepository.GetWithDetails(municipalityId);
            if (municipality == null)
                return (int.MinValue, Messages.NoMunicipalityWithIdExist);

            if (municipality.TaxRules?.Any() != true)
                return (int.MinValue, Messages.NoRuleForMunicipality);

            var (tax, error) = _taxCanculator.CalculateTaxFor(municipality, date);

            return (tax, error);
        }

        public async Task<List<TaxRule>> FindMunicipalityTaxRules(Guid municipalityId)
        {
            var municipality = await _municipalityRepository.GetWithDetails(municipalityId);
            return municipality.TaxRules.OrderBy(t => t.Priority).ToList();
        }

        public async Task<(List<Municipality>, int)> GetMunicipalities(Pagination pagination = null)
        {
            return await _municipalityRepository.GetListAsync(pagination);
        }

        public async Task<bool> Exist(string name)
        {
            return await _municipalityRepository.Exist(name);
        }

        public async Task<(Municipality editedMunicipality, string error)> EditAsync(Guid id, string newName)
        {
            if (await _municipalityRepository.Exist(newName))
                return (null, Messages.SameMunicipalityExist);

            var municipality = await _municipalityRepository.GetAsync(id);
            municipality.Name = newName;

            await _unitOfWork.CommitAsync();


            return (municipality, null);
        }

        public async Task<string> DeleteAsync(Guid id)
        {
            var municipality =await _municipalityRepository.GetAsync(id);
            if (municipality == null)
                return Messages.NoMunicipalityWithIdExist;

            _municipalityRepository.Delete(municipality);

            await _unitOfWork.CommitAsync();

            return null;
        }
    }
}
