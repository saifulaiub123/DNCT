﻿using Dnct.Domain.Entities.User;
using Microsoft.AspNetCore.Identity;

namespace Dnct.Application.Contracts.Identity;

public interface IAppUserManager
{
    Task<IdentityResult> CreateUser(User user);
    Task<IdentityResult> CreateUser(User user, string password);
    Task<bool> IsExistUser(string email);
    Task<bool> IsExistUserName(string userName);
    Task<string> GeneratePhoneNumberConfirmationToken(User user, string phoneNumber);
    Task<User> GetUserByCode(string code);
    Task<IdentityResult> ChangePhoneNumber(User user, string phoneNumber, string code);
    Task<IdentityResult> VerifyUserCode(User user,string code);
    Task<string> GenerateOtpCode(User user);
    Task<User> GetUserByPhoneNumber(string phoneNumber);
    Task<SignInResult> UserLogin(User user,string password);
    Task<User> GetByUserName(string userName);
    Task<User> GetUserByIdAsync(int userId);
    Task<List<User>> GetAllUsersAsync();
    Task<IdentityResult> CreateUserWithPasswordAsync(User user,string password);
    Task<IdentityResult> AddUserToRoleAsync(User user, Role role);
    Task<IdentityResult> IncrementAccessFailedCountAsync(User user);
    Task<bool> IsUserLockedOutAsync(User user);
    Task ResetUserLockoutAsync(User user);
    Task UpdateUserAsync(User user);
    Task UpdateSecurityStampAsync(User user);
}