﻿using FokinClicker.Domain;
using FokinClicker.UseCases.Register;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FokinClicker.UseCases.Login;
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
{
    private readonly UserManager<ApplicationUser> userManager;
    public RegisterCommandHandler(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }
    public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (userManager.Users.Any(u => u.UserName == request.UserName))
        {
            throw new ValidationException("Such user already exists.");
        }

        var user = new ApplicationUser
        {
            UserName = request.UserName,
            CurrentScore  = 0,
            RecordScore = 0
        };

        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            var errorString = string.Join(Environment.NewLine, result.Errors);
            throw new ValidationException(errorString);
        }
        return Unit.Value;
    }
}