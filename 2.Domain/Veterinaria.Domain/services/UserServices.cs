using NETCore.Encrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Common.Exceptions;
using Veterinaria.Common.Helper;
using Veterinaria.Common.Resources;
using Veterinaria.Core.UnitWork.interfacce;
using Veterinaria.Domain.services.Interface;
using Veterinaria.Entity.DTO.Users;
using Veterinaria.Entity.Models.Secure;
using static System.Object;

namespace Veterinaria.Domain.services
{
    public class UserServices : IUserServices
    {

        #region Attributes

        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Builder
        public UserServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        #endregion

        #region Methods

        //metodo para obtener todos los usuarios
        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            List<UserEntity> entities = await _unitOfWork.UserRepository.GetAllAsync();
            List<UserDto> user = entities.Select(x => new UserDto()
            {
                IdUser = x.IdUser,
                Name = x.Name,
                LastName = x.LastName,
                Email = x.Email
            }).ToList();
            return user;
        }

        //metodo para obtener el Email de un usuario por su id
        public async Task<UserEntity?> GetUserByEmail(string email) => await _unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Email == email);

        //metodo para agregar un usuario
        public async Task<bool> AddUser (UserAddDto userAdd)
        {
            await Validations(userAdd.Name, userAdd.LastName, userAdd.Email, userAdd.Password);
            UserEntity entity = new UserEntity()
            {
                Name = userAdd.Name,
                LastName = userAdd.LastName,
                Email = userAdd.Email,
                Password = EncryptProvider.Sha256(userAdd.Password),
            };

            _unitOfWork.UserRepository.Insert(entity);
            return await _unitOfWork.SaveAsync() > 0;
        }

        //metodo para actualizar un usuario
        public async Task<bool> UpdateUser(UserUpdateDto userUpdate)
        {
            UserEntity entity = await GetEntity(userUpdate.IdUser);
            await Validations(userUpdate.Name, userUpdate.LastName, userUpdate.Email, null, entity.Email);
            
            entity.Name = userUpdate.Name;
            entity.LastName = userUpdate.LastName;
            entity.Email = userUpdate.Email;

            _unitOfWork.UserRepository.Update(entity);
            return await _unitOfWork.SaveAsync() > 0;
        }

        //metodo para eliminar un usuario por su id
        public async Task<bool> DeleteUser(int idUser)
        {
            UserEntity entity = await GetEntity(idUser);
            _unitOfWork.UserRepository.Delete(entity);

            return await _unitOfWork.SaveAsync() > 0;
        }
        #endregion





        #region Privated

        // password: solo en alta. null en edición, porque no se cambia la contraseña.
        // currentEmail: email guardado. null en alta (siempre se valida unicidad).
        //               En edición, si coincide con el nuevo, no se consulta la BD.
        private async Task Validations(string name, string? lastName, string email, string? password = null, string? currentEmail = null)
        {
            // Validación de email
            if (!Helper.IsEmail(email))
                throw new BusinessException($"El email: {email} no es válido");

            // Alta: currentEmail es null y el correo no puede existir en BD.
            // Edición: solo se consulta si el correo cambió (sin distinguir mayúsculas).
            bool isNewUser = currentEmail == null;
            bool emailChanged = !isNewUser
                && !string.Equals(currentEmail!.Trim(), email.Trim(), StringComparison.OrdinalIgnoreCase);

            // Si es alta o si el correo cambió, se valida que no exista en BD.
            if (isNewUser || emailChanged)
                await ExistEmailInDB(email);

            // Validaciones de campos obligatorios
            if (string.IsNullOrWhiteSpace(name))
                throw new BusinessException($"El nombre es requerido");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new BusinessException($"El apellido es requerido");

            // La contraseña solo se exige al crear el usuario.
            if (password != null && string.IsNullOrWhiteSpace(password))
                throw new BusinessException($"La contraseña es requerida");
        }

       
        
        //metodo para obtener una entidad de usuario por su id
        private async Task<UserEntity> GetEntity(int idUser)
        {
            UserEntity? entity = await _unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.IdUser == idUser);
            if (entity == null)
            {
                string message = string.Format(GeneralMessages.ItemNoFound, "User");
                throw new BusinessException(message);
            }

            return entity;
        }

        //metodo para validar si el email ya existe en la base de datos
        private async Task ExistEmailInDB(string email)
        {
            UserEntity? entity = await _unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Email == email);
            if (entity != null)
            {
                //string message = string.Format(GeneralMessages.ItemNoFound, "User");
                throw new BusinessException($"Usuario con el email: {email} ya existe en el sistema.");
            }
        }



        #endregion
    }
}
