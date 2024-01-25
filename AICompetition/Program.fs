module AICompetition.Main

open AICompetition.Execution
open AICompetition.Log

[<EntryPoint>]
let main (args: string[]) =    
    log ""
    log "PCA Calculcation"
    log ""
    
    try
        match args with
        | [| mode; dataset |] -> execute mode dataset
        | _ -> printUsage
    with
        | ex -> handleException ex
