module AICompetition.Data

open AICompetition.Config
open AICompetition.Log
open FSharp.Data
open MathNet.Numerics.LinearAlgebra

type DataRow = { Class: int; Data: Vector<float> }

let private loadCsv (cfg: RunConfiguration) (file: string) =
    let data = CsvFile.Load(file, hasHeaders = false)
    let createDataRow (row: CsvRow) =
        let cls = row[0].AsInteger()
        let data = row.Columns |> Seq.skip 1 |> Seq.map float |> cfg.vectorFactory.ofEnumerable
        { Class = cls; Data = data }
    
    let l = data.Rows |> Seq.length
    data.Rows
        |> Seq.map createDataRow
        |> Seq.toList

let loadTrainingData (cfg: RunConfiguration) =
    log "Loading training data..."
    loadCsv cfg $@"data\{cfg.dataset}\train.csv"

let loadTestData (cfg: RunConfiguration) =
    log "Loading test data..."
    loadCsv cfg $@"data\{cfg.dataset}\test.csv"
