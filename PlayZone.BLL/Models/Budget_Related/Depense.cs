﻿namespace PlayZone.BLL.Models.Budget_Related;

public class Depense
{
    public int IdDepense { get; set; }
    public int LibeleId { get; set; }
    public int ProjectId { get; set; }
    public int? OrganismeId {get; set;}
    public Decimal Montant { get; set; }
    public DateTime? DateIntervention { get; set; }
    public DateTime DateFacturation { get; set; }
    public string Motif { get; set; }
}

public class PreparedInOut
{
    public required string Date { get; set; }
    public required decimal Montant { get; set; }
}
