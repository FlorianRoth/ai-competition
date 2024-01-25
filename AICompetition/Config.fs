module AICompetition.Config

open MathNet.Numerics.LinearAlgebra

type VectorFactory = {
    ofEnumerable: float seq->Vector<float>;
}

let denseVectorFactory: VectorFactory = {
    ofEnumerable = Vector.Build.DenseOfEnumerable 
}

let sparseVectorFactory: VectorFactory = {
    ofEnumerable = Vector.Build.SparseOfEnumerable 
}

type MatrixFactory = {
    zero: int->int->Matrix<float>;
    ofRowSeq: Vector<float> seq->Matrix<float>;
}

let denseMatrixFactory: MatrixFactory = {
    zero = DenseMatrix.zero
    ofRowSeq = DenseMatrix.ofRowSeq
}

let sparseMatrixFactory: MatrixFactory = {
    zero = SparseMatrix.zero
    ofRowSeq = SparseMatrix.ofRowSeq
}


type RunConfiguration = {
    dataset: string
    nMax: int
    vectorFactory: VectorFactory
    matrixFactory: MatrixFactory
}

let irisConfig = {
    dataset = "iris"
    nMax = 3
    vectorFactory = denseVectorFactory 
    matrixFactory = denseMatrixFactory 
}

let mnistConfig = {
    dataset = "mnist"
    nMax = 784
    vectorFactory = sparseVectorFactory 
    matrixFactory = sparseMatrixFactory 
}