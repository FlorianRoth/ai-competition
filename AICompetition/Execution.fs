module AICompetition.Execution

open System
open AICompetition.Config
open AICompetition.Data
open AICompetition.Classify
open AICompetition.Log
open AICompetition.Pca

let private calculate (cfg: RunConfiguration) =
    let trainingData = loadTrainingData cfg
    log $"TRAINING DATASET SIZE : {trainingData.Length}"
    log "====================================="
    
    computePcaMatrices cfg trainingData

let private logSummary results =
    log ""
    log "=================================="
    log "|    CLASSIFICATION RESULTS      |"
    log "=================================="
    log "|    N |   HIT |  MISS | RATE    |"
    log "|------|-------|-------|---------|"
    
    let logResult (n, result) =
        log $"| %4d{n} | %5d{result.Hit} | %5d{result.Miss} | %.02f{result.Rate * 100.0} %% |"
    results |> Seq.iter logResult

    log "=================================="

let private classify (cfg: RunConfiguration) =    
    let trainingData = loadTrainingData cfg
    log $"TRAINING DATASET SIZE : {trainingData.Length}"
    
    let testData = loadTestData cfg
    log $"TEST DATASET SIZE : {testData.Length}"
    log "====================================="
    
    let classifyN n =
        log $"Running NN on PCA with n = {n}"
        let pcaMatrix = loadPca cfg n
        let result = runClassification (applyPca pcaMatrix trainingData) (applyPca pcaMatrix testData)
        log $"  HIT:  {result.Hit}"
        log $"  MISS: {result.Miss}"
        log $"  RATE: %.2f{result.Rate * 100.} %%" 
        log "====================================="
        log ""
        (n, result)
    
    seq { 1..cfg.nMax } |> Seq.map classifyN |> Seq.toList |> logSummary

let execute mode dataset =
    let cfg = match dataset with
              | "iris" -> irisConfig
              | "mnist" -> mnistConfig
              | _ -> invalidArg "dataset" dataset
    
    match mode with
    | "calculate" -> calculate cfg
    | "classify" -> classify cfg
    | _ -> invalidArg "mode" mode
    0

let printUsage =
    log "Usage: AICompetition <mode> <dataset>"
    log "  <mode>:    calculate | classify"
    log "  <dataset>: iris | mnist"
    1

let handleException (ex: Exception) =
    log ex.Message
    1
