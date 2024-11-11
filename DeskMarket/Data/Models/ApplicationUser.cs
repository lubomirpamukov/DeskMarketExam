using DeskMarket.Common;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DeskMarket.Data.Models;

public class ApplicationUser :IdentityUser
{
    public int MyProperty { get; set; }
}
