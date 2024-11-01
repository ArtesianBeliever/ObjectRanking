using Microsoft.EntityFrameworkCore;
using ObjectRanking.Data;
using ObjectRanking.Models.Entities;

namespace ObjectRanking.Interfaces;

public interface ITestService
{
    string GetTime();
    Task<ApplicationUser?> GetUser();
}