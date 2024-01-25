module AICompetition.Timed

open AICompetition.Log
open System.Diagnostics

let timed<'T> (text: string) (fn: unit->'T) =
    let stopWatch = Stopwatch.StartNew()
    let result = fn()
    stopWatch.Stop()
    
    let time = (stopWatch.Elapsed.ToString("c"))
    log $"{text} took {time}"  
    result