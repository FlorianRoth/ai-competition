module AICompetition.Classify

open AICompetition.Data
open AICompetition.Log
open AICompetition.NextNeighbor
open AICompetition.Timed
open FSharp.Collections.ParallelSeq

type ClassificationResult = {Hit: int; Miss: int; Rate: float}

let classify (classifier: DataRow->int) (testData: DataRow seq) =
    log "  Classifiying data..."

    let isHit datarow =
        datarow.Class = (classifier datarow)
    
    let hit = testData
              |> PSeq.map isHit
              |> PSeq.filter id
              |> PSeq.length
              
    let total = testData |> Seq.length
    let miss = total - hit
    let rate = (float hit) / (float total)

    { Hit = hit; Miss = miss; Rate = rate }

let runClassification (trainingData: DataRow seq) (testData: DataRow seq) =
    timed "  Classification" (fun () -> classify (nextNeighbor trainingData) testData)
