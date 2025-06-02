using System.ComponentModel.DataAnnotations;

namespace Manutec.Core.Enums;
public enum MaintenanceType
{
    [Display(Name = "Troca de óleo")]
    OilChange,
    [Display(Name = "Pastilhas de freio")]
    BrakePads,
    [Display(Name = "Revisão")]
    Review,
    [Display(Name = "Troca de pneus")]
    TireChange,
    [Display(Name = "Reparo de motor")]
    EngineRepair,
    [Display(Name = "Reparo de transmissão")]
    TransmissionRepair
}
