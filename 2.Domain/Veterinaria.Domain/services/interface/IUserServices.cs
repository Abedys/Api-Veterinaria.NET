using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Entity.DTO.Users;
using Veterinaria.Entity.Models.Secure;

namespace Veterinaria.Domain.services.Interface
{
    public interface IUserServices
    {
        Task<List<UserDto>> GetAllUsersAsync();

        Task<UserEntity?> GetUserByEmail(string email);

        Task<bool> AddUser(UserAddDto userAdd);

        Task<bool> UpdateUser(UserUpdateDto userUpdate);

        Task<bool> DeleteUser(int idUser);
    }
}
