using FastEndpoints;
using TbdFriends.WaterDrinkWater.Api.Infrastructure;
using TbdFriends.WaterDrinkWater.Application.Services;
using viewmodels;

namespace TbdFriends.WaterDrinkWater.Api.Endpoints.Groups;

public class Get(GroupService groupService) : EndpointWithoutRequest<IEnumerable<GroupViewModel>>
{
    public override void Configure()
    {
        Get("api/groups");
    }

    public override Task<IEnumerable<GroupViewModel>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        if (User.GetUserId() is not (var userId and > 0))
        {
            return Task.FromResult(Enumerable.Empty<GroupViewModel>());
        }

        var groups = groupService.GetGroups(userId);

        return Task.FromResult(groups);
    }
}