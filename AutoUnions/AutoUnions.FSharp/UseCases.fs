namespace AutoUnions.FSharp

module BurnDown =

  let GetTotalCapacity team =
    match team with
    | DevelopmentTeam t -> t.Capacity.Total
    | NonDevelopmentTeam _ -> 0
    | SupplierTeam t -> t.TotalCapacity


