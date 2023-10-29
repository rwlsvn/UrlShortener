using AuthService.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoleController : BaseController
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost("add/{rolename}")]
        public async Task<IdentityResult> Add(string roleName)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            return result;
        }

        [HttpPut("update")]
        public async Task<IdentityResult> Update(IdentityRole role)
        {
            var roleToUpdate = await _roleManager.FindByIdAsync(role.Id);
            if (roleToUpdate == null)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Description = $"Role {role.Name} was not found."
                });
            }

            var result = await _roleManager.UpdateAsync(role);
            return result;
        }

        [HttpDelete("remove/{rolename}")]
        public async Task<IdentityResult> Remove(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            var result = await _roleManager.DeleteAsync(role);
            return result;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<IdentityRole>> Get()
        {
            var result = _roleManager.Roles.AsEnumerable();
            return result;
        }
    }
}
