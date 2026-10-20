using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Common.Exceptions;
using Veterinaria.Common.Resources;
using Veterinaria.Core.UnitWork.interfacce;
using Veterinaria.Domain.services.Interface;
using Veterinaria.Entity.DTO.Species;
using Veterinaria.Entity.Models.Veterinaria;

namespace Veterinaria.Domain.services
{
    public class SpeciesServices : ISpeciesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public SpeciesServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        #region Services


        //metodo para obtener uuan espcie por id
        private async Task<SpeciesEntity> GetEntityAsync(int specieId)
        {
            SpeciesEntity? entity = await _unitOfWork.SpeciesRepository.FirstOrDefaultAsync(x => x.Id == specieId);

            if (entity == null)
            {
                string message = string.Format(GeneralMessages.ItemNoFound, "Especie");
                throw new BusinessException(message);
            }
            return entity;
        }


        public async Task<List<SpeciesDto>> GetAllSpeciesAsync()
        {
            List<SpeciesEntity> entities = await _unitOfWork.SpeciesRepository.GetAllAsync();
            List<SpeciesDto> speciesDtos = entities.Select(x => new SpeciesDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            return speciesDtos;
        }


        //metodo para agregar una especie
        public async Task<bool> AddSpecie(string name)
        {
            SpeciesEntity entity = new SpeciesEntity
            {
                Name = name
            };

            _unitOfWork.SpeciesRepository.Insert(entity);


            return await _unitOfWork.SaveAsync() > 0;
        }

     

        //metodo para actualizar una especie
        public async Task<bool> UpdateSpecie(SpeciesDto species)
        {

            SpeciesEntity entity = await GetEntityAsync(species.Id);

            entity.Name = species.Name;

            _unitOfWork.SpeciesRepository.Update(entity);


            return await _unitOfWork.SaveAsync() > 0;
        }

        //metodo para eliminar una especie
        public async Task<bool> DeleteSpecie(int specieId)
        {
            SpeciesEntity entity = await GetEntityAsync(specieId);
            _unitOfWork.SpeciesRepository.Delete(entity);
            return await _unitOfWork.SaveAsync() > 0;
        }

        #endregion
    }
}
