open System

// DesignSpecification:	111111-REQ-01
// RequirementsSpecification:	111111-REQ-*
// UserManual: <emtpy>

type CellValue =
    | Id of number:int * kind:string * version:int
    | WildcardId of number:int * kind:string
    | EmptyCell
    | FreeText of string

let Print value = 
    match value with
    | Id(number,kind,version) -> printfn "%i-%s-%i" number kind version
    | WildcardId(number,kind) -> printfn "%i-%s-*" number kind
    | EmptyCell -> printfn "<empty>"
    

[<EntryPoint>]
let main argv =

    0 