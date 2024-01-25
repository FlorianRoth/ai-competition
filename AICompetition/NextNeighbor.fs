module AICompetition.NextNeighbor

open System

open AICompetition.Data
open MathNet.Numerics.LinearAlgebra

let distance (a: Vector<float>) (b: Vector<float>) =
    let state = 0.0
    let d = a - b
    (state, d)
    ||> Vector.foldSkipZeros (fun acc c -> (c * c) + acc)
    |> Math.Sqrt

let nextNeighbor<'c> (training: DataRow seq) (test: DataRow) =
    let result = Seq.minBy (fun dr -> distance test.Data dr.Data) training
    result.Class
    