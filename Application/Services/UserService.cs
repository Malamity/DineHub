﻿using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync() => await _userRepository.GetAllAsync();

    public async Task<User> GetUserByIdAsync(int id) => await _userRepository.GetByIdAsync(id);

    public async Task<User> CreateUserAsync(User user) => await _userRepository.AddAsync(user);

    public async Task UpdateUserAsync(User user) => await _userRepository.UpdateAsync(user);

    public async Task DeleteUserAsync(int id) => await _userRepository.DeleteAsync(id);
}