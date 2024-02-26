using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TbdFriends.WaterDrinkWater.Api.Endpoints.TimeZones;

public class Get : EndpointWithoutRequest<Results<Ok<IEnumerable<TimeZoneInfo>>, BadRequest>>
{
    public override void Configure()
    {
        Get("api/timezones");

        AllowAnonymous();
    }

    public override Task<Results<Ok<IEnumerable<TimeZoneInfo>>, BadRequest>> ExecuteAsync(CancellationToken ct)
    {
        var timeZones = TimeZoneInfo.GetSystemTimeZones()
            .Select(tz => tz);

        return Task.FromResult<Results<Ok<IEnumerable<TimeZoneInfo>>, BadRequest>>(TypedResults.Ok(timeZones));
    }
}