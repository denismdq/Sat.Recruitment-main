using FluentValidation;
using Sat.Recruitment.Application.DTO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Numerics;
using System.Text;
using System.Xml.Linq;

namespace Sat.Recruitment.Application.Validators
{
    public class RequestUserValidator : AbstractValidator<RequestUser>
    {
        public RequestUserValidator()
        {
            RuleFor(x => x.name).NotEmpty();
            RuleFor(x => x.email).NotEmpty().EmailAddress();
            RuleFor(x => x.address).NotEmpty();
            RuleFor(x => x.phone).NotEmpty();
        }

    }
}
