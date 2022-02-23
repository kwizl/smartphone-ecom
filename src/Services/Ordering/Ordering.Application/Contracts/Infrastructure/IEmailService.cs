using System.Collections.Generic;
using System.Threading.Tasks;
using Ordering.Application.Models;
using Ordering.Domain.Entities;

namespace Ordering.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}