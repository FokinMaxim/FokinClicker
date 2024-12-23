using FokinClicker.UseCases.Common;
using MediatR;

namespace FokinClicker.UseCases.BuySupports;

public record BuySupportCommand(int SupportId) : IRequest<ScoreSupportDto>;
