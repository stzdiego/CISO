using System.Security.Claims;
using CISO.ManagementService.Shared.Entities;
using STZ.Frontend.Authorization;
using STZ.Shared.Bases;
using STZ.Shared.Entities;
using Action = CISO.ManagementService.Shared.Entities.Action;

namespace CISO.Frontend.Services;

public class PermissionService : IPermissionService
{
    private readonly ServiceBase<Permission> _permissionService;
    private readonly ServiceBase<Feature> _featureService;
    private readonly ServiceBase<User> _userService;
    private readonly ServiceBase<UserRole> _userRoleService;
    private readonly ServiceBase<Action> _actionService;
    
    public PermissionService(ServiceBase<Permission> permissionService, ServiceBase<Feature> featureService, 
        ServiceBase<User> userService, ServiceBase<UserRole> userRoleService, ServiceBase<Action> actionService)
    {
        _permissionService = permissionService;
        _featureService = featureService;
        _userService = userService;
        _userRoleService = userRoleService;
        _actionService = actionService;
    }
    
    public async Task<bool> HasAccessAsync(ClaimsPrincipal user, string feature, string action)
    {
        if (!user.Identity?.IsAuthenticated ?? true)
        {
            return false;
        }
        
        var userEmail = user.FindFirst(ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(userEmail)) return false;
        
        var userFilter = $"Email = \"{userEmail}\"";
        var userResponse = await _userService.FindAsync(userFilter);
        var userEntity = userResponse.FirstOrDefault();
        if (userEntity == null) return false;
        
        if (userEntity.IsAdmin) return true;
        
        var actionFilter = $"Name = \"{action}\"";
        var actionResponse = await _actionService.FindAsync(actionFilter);
        var actionEntity = actionResponse.FirstOrDefault();
        if (actionEntity == null) return false;

        var featureFilter = $"Name = \"{feature}\"";
        var featureResponse = await _featureService.FindAsync(featureFilter);
        var featureEntity = featureResponse.FirstOrDefault();
        if (featureEntity == null) return false;
        
        var userRoleFilter = $"UserId = \"{userEntity.Id}\"";
        var userRoleEntities = await _userRoleService.FindAsync(userRoleFilter);
        if (!userRoleEntities.Any()) return false;

        // Genero un filtro para buscar los permisos de los roles del usuario
        var roleIds = userRoleEntities.Select(ur => "Guid.Parse(\"" + ur.RoleId + "\")").ToList();
        var roleFilter = $"RoleId IN ({string.Join(",", roleIds)}) AND FeatureId = Guid.Parse(\"{featureEntity.Id}\")";
        var permissions = await _permissionService.FindAsync(roleFilter);
        
        if (!permissions.Any()) return false;
        if (permissions.Any(x => x.ActionId == actionEntity.Id)) return true;
        
        return false;
    }
}