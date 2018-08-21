using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebNotes.bl.DTO;
using WebNotes.bl.Infrustructure;
using WebNotes.bl.Interfaces;
using WebNotes.data.Abstract.Entities;
using WebNotes.data.Entities;

namespace WebNotes.bl.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork db;

        public UserService(IUnitOfWork db)
        {
            this.db = db;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            User user = await db.UserManager.FindByIdAsync(userDto.Id);
            if (user == null && db.RoleManager.RoleExists(userDto.Role))
            {
                user = new User { Email = userDto.Email, UserName = userDto.Login, PhoneNumber = userDto.Phone }; 
                var result = await db.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                // добавляем роль
                result = await db.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                // создаем профиль клиента
                UserProfile userProfile = new UserProfile { Id = user.Id, City = userDto.City, Name = userDto.Name, Surname = userDto.Surname};
                try
                {
                    db.UsersProfileManager.Add(userProfile);
                    await db.SaveAsync();
                    return new OperationDetails(true, "Регистрация успешно пройдена", "");
                }
                catch (Exception ex)
                {
                    return new OperationDetails(false, ex.Message, "");
                }

            }
            else
            {
                return new OperationDetails(false, "Такой пользователь уже существует", "Id");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            User user = await db.UserManager.FindAsync(userDto.Login, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await db.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await db.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new Role { Name = roleName };
                    await db.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public async Task<OperationDetails> Edit(UserDTO userDto)
        {
            var exists = await db.UserManager.FindByIdAsync(userDto.Id);

            if (exists != null && db.RoleManager.RoleExists(userDto.Role))
            {
                User user = new User {Id = userDto.Id, Email = userDto.Email, UserName = userDto.Login, PhoneNumber = userDto.Phone }; //mention id?

                var result = await db.UserManager.UpdateAsync(user);

                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                UserProfile userProfile = new UserProfile { Id = user.Id, City = userDto.City, Name = userDto.Name, Surname = userDto.Surname };

                try
                {
                    db.UsersProfileManager.Edit(userProfile);
                    await db.SaveAsync();
                    return new OperationDetails(true, "Изменения вступили в силу", "");
                }
                catch (Exception ex)
                {
                    return new OperationDetails(false, ex.Message, "");
                }
            }
            else return new OperationDetails(false, "Такого пользователя не существует", "Id");
        }

        public async Task<OperationDetails> ChangePassword(UserDTO userDto)
        {
            User user = await db.UserManager.FindByIdAsync(userDto.Id);

            if (user != null)
            {
                var result = await db.UserManager.ChangePasswordAsync(user.Id, user.PasswordHash, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                await db.SaveAsync();
                return new OperationDetails(true, "Пароль был успешно изменен", "");
            }
            else return new OperationDetails(false, "Такого пользователя не существует", "Id");
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            List<UserDTO> users = new List<UserDTO>();
            List<User> dbUsers = await db.UserManager.Users.ToListAsync();
            foreach (var dbUser in dbUsers)
            {
                users.Add(new UserDTO
                {
                    Id = dbUser.Id,
                    Login = dbUser.UserName,
                    Email = dbUser.Email,
                    Phone = dbUser.PhoneNumber,
                    Role = dbUser.Roles.FirstOrDefault().ToString(),
                    Name = dbUser.UserProfile.Name,
                    Surname = dbUser.UserProfile.Surname,
                    City = dbUser.UserProfile.City
                    // add notes
                });
            }

            return users;
        }

        public async Task<UserDTO> GetOne(string id)
        {
            User dbUser = await db.UserManager.FindByIdAsync(id);
            UserDTO returnUser = new UserDTO {
                Id = dbUser.Id,
                Login = dbUser.UserName,
                Email = dbUser.Email,
                Phone = dbUser.PhoneNumber,
                Role = dbUser.Roles.FirstOrDefault().ToString(),
                Name = dbUser.UserProfile.Name,
                Surname = dbUser.UserProfile.Surname,
                City = dbUser.UserProfile.City
            };
            return returnUser;
        }

        public async Task<OperationDetails> Remove(string id)
        {
            User dbUser = await db.UserManager.FindByIdAsync(id);
            if (dbUser != null)
            {
                var result = await db.UserManager.DeleteAsync(dbUser);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                return new OperationDetails(true, "Пользователь был успешно удален", "");
            }
            else return new OperationDetails(false, "Такого пользователя не существует", "Id");
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
