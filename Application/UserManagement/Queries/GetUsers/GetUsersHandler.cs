using Application.UserManagement.Dto;
using Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;

namespace Application.UserManagement.Queries.GetUsers;

public class GetUsersHandler : IRequestHandler<GetUsersRequest, GetUsersResponse>
{
    private readonly IUserRepository _userRepository;

    public GetUsersHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetUsersResponse> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        var users = _userRepository.Query().Include(x => x.UserProfile);
        
        var total = await users.CountAsync();

        var usersList = await users.Pagination(request).ToListAsync();

        var response = new GetUsersResponse
        {
            Users = usersList.Select(x => new UserDtoModel
            {
                Id = x.Id,
                FirstName = x.UserProfile.FirstName,
                LastName = x.UserProfile.LastName,
                PersonalNumber = x.UserProfile.PersonalNumber,
                UserName = x.UserName,
                Email = x.Email,
                IsActive = x.IsActive
            }),
            Page = request.Page,
            PageSize = request.PageSize,
            Total = total,
        };

        return response;
    }
}