using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Common.Exceptions;
using Veterinaria.Common.Resources;
using Veterinaria.Core.UnitWork.interfacce;
using Veterinaria.Domain.services.Interface;
using Veterinaria.Entity.DTO.Breed;
using Veterinaria.Entity.DTO.Species;
using Veterinaria.Entity.Models.Veterinaria;

namespace Veterinaria.Domain.services
{
    public class BreedServices: IBreedServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public BreedServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        #region Services


        //metodo para obtener una raza por id
        private async Task<BreedEntity> GetBreedAsync(int breedId)
        {
            BreedEntity? entity = await _unitOfWork.BreedRepository.FirstOrDefaultAsync(x => x.Id == breedId);

            if (entity == null)
            {
                string message = string.Format(GeneralMessages.ItemNoFound, "Raza");
                throw new BusinessException(message);
            }
            return entity;
        }

        //obtener todas las razas
        public async Task<List<BreedDto>> GetAllBySpeciesId(int speciesId)
        {
            List<BreedEntity> entities = await _unitOfWork.BreedRepository.FindAllAsync(x => x.SpeciesId == speciesId,
                                                                                        s => s.SpeciesEntity);
            List<BreedDto> breeds = entities.Select(x => new BreedDto
            {
                BreedId = x.Id,
                Name = x.Name,
                SpeciesId = x.SpeciesId,
                Species = x.SpeciesEntity.Name
            }).ToList();
            return breeds;
        }


        //metodo para agregar una raza
        public async Task<bool> AddBreed(BreedAddDto breed)
        {
            BreedEntity entity = new BreedEntity
            {
                Name = breed.Name,
                SpeciesId = breed.SpeciesId
            };

            _unitOfWork.BreedRepository.Insert(entity);


            return await _unitOfWork.SaveAsync() > 0;
        }



        //metodo para actualizar una raza
        public async Task<bool> UpdateBreed(BreedUpdateDto breed)
        {

            BreedEntity entity = await GetBreedAsync(breed.BreedId);

            entity.Name = breed.Name;
            entity.SpeciesId = breed.SpeciesId;

            _unitOfWork.BreedRepository.Update(entity);


            return await _unitOfWork.SaveAsync() > 0;
        }

        //metodo para eliminar una raza
        public async Task<bool> DeleteBreed(int breedId)
        {
            BreedEntity entity = await GetBreedAsync(breedId);
            _unitOfWork.BreedRepository.Delete(entity);
            return await _unitOfWork.SaveAsync() > 0;
        }

        #endregion

    }
}
