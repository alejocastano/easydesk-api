using System.Linq;
using System.Collections.Generic;

namespace EasyDesk.Domain;

public class TicketStateService : ITicketStateService
{
    private readonly Dictionary<TicketStatusType, List<TicketStatusType>> _transitions = new()
    {
        { TicketStatusType.Open, new List<TicketStatusType> { TicketStatusType.InProgress, TicketStatusType.Resolved } },
        { TicketStatusType.InProgress, new List<TicketStatusType> { TicketStatusType.Open, TicketStatusType.Resolved } },
        { TicketStatusType.Resolved, new List<TicketStatusType> { TicketStatusType.Closed, TicketStatusType.InProgress } },
        { TicketStatusType.Closed, new List<TicketStatusType> { TicketStatusType.Reopened } },
        { TicketStatusType.Reopened, new List<TicketStatusType> { TicketStatusType.InProgress, TicketStatusType.Resolved } }
    };

    private readonly Dictionary<TicketStatusType, HashSet<RoleType>> _allowedRolesForTransition = new()
    {
        { TicketStatusType.InProgress, new HashSet<RoleType> { RoleType.Agent, RoleType.Admin } },
        { TicketStatusType.Resolved, new HashSet<RoleType> { RoleType.Agent, RoleType.Admin } },
        { TicketStatusType.Closed, new HashSet<RoleType> { RoleType.Submitter, RoleType.Admin } },
        { TicketStatusType.Reopened, new HashSet<RoleType> { RoleType.Agent, RoleType.Submitter, RoleType.Admin } }
    };

    public bool CanTransition(TicketStatusType from, TicketStatusType to)
    {
        if (_transitions.TryGetValue(from, out var possibleTransitions))
        {
            return possibleTransitions.Contains(to);
        }

        return false;
    }

    public bool CanUserTransition(TicketStatusType from, TicketStatusType to, User user)
    {
        if (!CanTransition(from, to)) return false;

        if (!_allowedRolesForTransition.TryGetValue(to, out var allowedRoles))
        {
            return false;
        }

        if (allowedRoles.Count == 0) return false;

        if (user == null) return false;

        var userRoleTypes = new HashSet<RoleType>();

        try
        {
            if (user.UserRoles != null)
            {
                foreach (var ur in user.UserRoles)
                {
                    if (Enum.IsDefined(typeof(RoleType), ur.RoleId))
                    {
                        userRoleTypes.Add((RoleType)ur.RoleId);
                    }
                }
            }
        }
        catch { }

        return userRoleTypes.Overlaps(allowedRoles);
    }
}
    