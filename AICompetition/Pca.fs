module AICompetition.Pca

open System.IO
open AICompetition.Config
open AICompetition.Data
open AICompetition.Log
open AICompetition.Timed
open MathNet.Numerics.Data.Text
open MathNet.Numerics.LinearAlgebra
open MathNet.Numerics.Statistics

let private matrix (factory: MatrixFactory) (rows: DataRow seq) =
    factory.ofRowSeq (rows |> Seq.map (_.Data))

let covarianceMatrix (factory: MatrixFactory) (m : Matrix<float>) =
    let cols = m.ColumnCount
    
    let c = factory.zero cols cols
        
    for c1 in 0 .. (cols - 1) do
        c.[c1, c1] <- Statistics.Variance (m.Column c1)
            
        for c2 in 0 .. (cols - 1) do
            let cov = Statistics.Covariance (m.Column c1, m.Column c2)
            c.[c1, c2] <- cov
            c.[c2, c1] <- cov
    c  

let pca (cfg: RunConfiguration) (data: DataRow seq) =
    log "Calulating PCA"
    
    let f = matrix cfg.matrixFactory data
    
    log "  Calculating covariance matrix..."
    let q = covarianceMatrix cfg.matrixFactory f
    
    log "  Calculating eigenvalues & eigenvectors..."
    let evd = q.Evd()

    // The MathNET docs state that the matrix returned by evd.EigenVectors
    // contains the eigenvectors.
    // Unfortunately the docs don't say if the vectors are contained in the
    // rows or columns of the matrix.
    // If I could dive deeper into how MathNET implemented this function,
    // I could figure it out, but nobody ain't got time for that...
    // Using the columns yields better results, so... 
    
    let createPcaMatrix n =
        log $"  Calculating PCA matrix for n = {n}"
        let lastIndex = evd.EigenValues.Count - 1
        seq { lastIndex..(-1)..0 }
        |> Seq.take n
        |> Seq.map evd.EigenVectors.Column
        |> cfg.matrixFactory.ofRowSeq
        
    let createResult n =
        let pca = createPcaMatrix n
        (n, pca)
    
    seq { 1..cfg.nMax } |> Seq.map createResult |> Seq.toList
    
let applyPca (v: Matrix<float>) (data: DataRow seq) =
    log "  Applying PCA to data..."
    
    data |> Seq.map (fun d -> { d with Data = v * d.Data } ) |> Seq.toList
    
let dirName cfg =
    $@"pca\{cfg.dataset}"

let fileName cfg n =
    $@"{dirName cfg}\pca-n%04d{n}.mtx"

let loadPca (cfg: RunConfiguration) (n: int) =
    let file = fileName cfg n
    log $"  Loading PCA from {file}"

    MatrixMarketReader.ReadMatrix<float>(file)

let computePcaMatrices (cfg: RunConfiguration) (trainingData: DataRow seq)  =
    let pcas = timed "  Calculating PCA matrices" (fun () -> pca cfg trainingData)
    
    Directory.CreateDirectory(dirName cfg) |> ignore
    
    let savePca (n, pca) =
        let file = fileName cfg n
        log $"  Saving PCA into {file}"
        MatrixMarketWriter.WriteMatrix(file, pca)
    
    pcas |> Seq.iter savePca
