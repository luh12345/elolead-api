using Elogroup.Api.Repository;
using Elogroup.Lead.Api.Common;
using Elogroup.Lead.Api.DTO;
using Elogroup.Lead.Api.Repository.Entities;
using Elogroup.Lead.Api.Services.Contract;
using Elogroup.Lead.Api.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elogroup.Lead.Api.Services
{
    public class UserService : IUserService
    {
        private readonly Context context;

        public UserService(Context context)
        {
            this.context = context;
        }

        public void CreateUser(CreateUserDTO user)
        {
            var createUserValidator = new CreateUserValidator();
            createUserValidator.ValidateAndThrow(user);

            if (context.Users.Any(x => x.Username == user.Username))
                throw new Exception("Já existe um usuário com este username, por favor user outro.");

            context.Users.Add(new User
            {
                Username = user.Username,
                Password = Cypher.StringToMD5(user.Password)
            });

            context.SaveChanges();
        }

        public UserDTO GetUser(UserDTO userDTO)
        {
            var passMD5 = Cypher.StringToMD5(userDTO.Password);
            var user = context.Users.SingleOrDefault(x => x.Username == userDTO.Username && x.Password == passMD5);
            if (user == null)
                throw new Exception("User não encontrado");

            return new UserDTO
            {
                Id = user.Id,
                Username = user.Username
            };
        }
    }
}
