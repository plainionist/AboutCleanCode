namespace AutoUnions.FSharp

open System

type Capacity = {
  Ramp : (DateTime * float) list
  Total : float
}

type DevelopmentTeam = {
    Name : string
    AreaPath : string list
    Capacity : Capacity
    Velocity : float
}

type NonDevelopmentTeam = {
    Name : string
    AreaPath : string list
}

type SupplierTeam = {
    Name : string
    AreaPath : string list
    TotalCapacity : float
}

type Team =
  | DevelopmentTeam of DevelopmentTeam
  | NonDevelopmentTeam of NonDevelopmentTeam
  | SupplierTeam of SupplierTeam

