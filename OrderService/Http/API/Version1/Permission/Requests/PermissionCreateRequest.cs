using System.ComponentModel.DataAnnotations;
namespace DotNetService.Http.API.Version1.Permission
{
    public class PermissionCreateRequest
    {
        [Required]
        public string Name { get; set; }

        public static Models.Permission Assign(PermissionCreateRequest data)
        {
            Models.Permission res = new()
            {
                Name = data.Name,

            };

            return res;
        }
    }
}