﻿using Manutec.Application.Models;
using MediatR;

namespace Manutec.Application.Commands.VehicleEntity;
public class DeleteVehicleCommand : IRequest<ResultViewModel>
{
    public int Id { get; set; }
    public int WorkShopId { get; set; }
    public int CustomerId { get; set; }
}
