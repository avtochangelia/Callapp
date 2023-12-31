﻿using Application.UserManagement.Dto;
using Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserManagement.Queries.GetUser;

public class GetUserHandler : IRequestHandler<GetUserRequest, GetUserResponse>
{
    private readonly IUserRepository _userRepository;

    public GetUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Query(x => x.Id == request.UserId)
                                        .Include(x => x.UserProfile)
                                        .FirstOrDefaultAsync();

        if (user == null)
        {
            throw new KeyNotFoundException($"User was not found for Id: {request.UserId}");
        }

        return new GetUserResponse
        {
            User = new UserDtoModel
            {
                Id = user.Id,
                FirstName = user.UserProfile.FirstName,
                LastName = user.UserProfile.LastName,
                PersonalNumber = user.UserProfile.PersonalNumber,
                UserName = user.UserName,
                Email = user.Email
            },
        };
    }
}