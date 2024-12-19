using FokinClicker.UseCases.Common;
using MediatR;

namespace FokinClicker.UseCases.AddPoints;

public record AddPointsCommand(int Clicks, int Seconds) : IRequest<ScoreDto>;