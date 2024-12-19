using FokinClicker.UseCases.Common;
using MediatR;

namespace FokinClicker.UseCases.BuyBoosts;

public record BuyBoostCommand(int BoostId) : IRequest<ScoreBoostDto>;
