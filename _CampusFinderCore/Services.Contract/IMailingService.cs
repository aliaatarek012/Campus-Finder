using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderCore.Services.Contract
{
    public interface IMailingService
    {
        Task SendEmailAsync(string mailTo, string subject, string body, IList<IFormFile> attachments = null, string mailfrom = null);
        public string GenerateCode();


    }
}
