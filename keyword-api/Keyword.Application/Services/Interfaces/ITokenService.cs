﻿using Keyword.Domain.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyword.Application.Services.Interfaces
{
    public interface ITokenService
    {
        bool Authentication(string User, string Password);
        ResultOperation<Claims> GetRolByUser(string User);
    }
}
