namespace FokinClicker.Infrastructure.Abstractions;

public interface ICurrentUserAccessor
{
    Guid GetCurrentUserId();
}