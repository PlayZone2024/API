﻿using Microsoft.Extensions.Configuration;
using PlayZone.BLL.Exceptions;
using PlayZone.BLL.Helpers;
using PlayZone.BLL.Interfaces.User_Related;
using PlayZone.BLL.Mappers.User_Related;
using PlayZone.DAL.Interfaces.User_Related;
using User = PlayZone.BLL.Models.User_Related.User;

namespace PlayZone.BLL.Services.User_Related;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHelper _passwordHelper;

    public AuthService(
        IUserRepository userRepository,
        PasswordHelper passwordHelper
    )
    {
        this._userRepository = userRepository;
        this._passwordHelper = passwordHelper;
    }

    public User? Login(User user)
    {
        string emailInput = user.Email.ToLower();
        User? userDb = this._userRepository.Login(emailInput)?.ToModel();

        if (userDb != null && userDb.Password == this._passwordHelper.GenerateHash(emailInput, user.Password))
        {
            return userDb;
        }

        throw new LoginException();
    }
}


