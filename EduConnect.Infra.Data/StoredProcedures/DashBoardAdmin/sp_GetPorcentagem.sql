CREATE OR ALTER PROCEDURE sp_GetPorcentagem
    @MesAtual INT,
    @MesAnterior INT,
    @Ano INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Alunos
    DECLARE @AumentoAluno INT;
    DECLARE @DecrementoAluno INT;
    SELECT 
        @AumentoAluno = COUNT(*) 
    FROM Alunos 
    WHERE Deletado = 0 AND MONTH(DataMatricula) = @MesAtual AND YEAR(DataMatricula) = @Ano;

    SELECT 
        @DecrementoAluno = COUNT(*) 
    FROM Alunos 
    WHERE Deletado = 0 AND MONTH(DataMatricula) = @MesAnterior AND YEAR(DataMatricula) = @Ano;

    -- Professores
    DECLARE @AumentoProfessor INT;
    DECLARE @DecrementoProfessor INT;
    SELECT @AumentoProfessor = COUNT(*) 
    FROM Professores 
    WHERE Deletado = 0 AND MONTH(Contratacao) = @MesAtual AND YEAR(Contratacao) = @Ano;

    SELECT @DecrementoProfessor = COUNT(*) 
    FROM Professores 
    WHERE Deletado = 0 AND MONTH(Contratacao) = @MesAnterior AND YEAR(Contratacao) = @Ano;

    -- Turmas
    DECLARE @AumentoTurma INT;
    DECLARE @DecrementoTurma INT;
    SELECT @AumentoTurma = COUNT(*) 
    FROM Turmas 
    WHERE MONTH(DataCriacao) = @MesAtual AND YEAR(DataCriacao) = @Ano;

    SELECT @DecrementoTurma = COUNT(*) 
    FROM Turmas 
    WHERE MONTH(DataCriacao) = @MesAnterior AND YEAR(DataCriacao) = @Ano;

    -- Presenças
    DECLARE @AumentoPresenca INT;
    DECLARE @DecrementoPresenca INT;
    SELECT @AumentoPresenca = COUNT(*) 
    FROM Presencas 
    WHERE (Presente = 1 OR Justificada = 1) AND MONTH(Data) = @MesAtual AND YEAR(Data) = @Ano;

    SELECT @DecrementoPresenca = COUNT(*) 
    FROM Presencas 
    WHERE (Presente = 0 AND Justificada = 0) AND MONTH(Data) = @MesAnterior AND YEAR(Data) = @Ano;

    -- Retornar todas as contagens em uma linha
    SELECT 
        @AumentoAluno AS AumentoAluno,
        @DecrementoAluno AS DecrementoAluno,
        @AumentoProfessor AS AumentoProfessor,
        @DecrementoProfessor AS DecrementoProfessor,
        @AumentoTurma AS AumentoTurma,
        @DecrementoTurma AS DecrementoTurma,
        @AumentoPresenca AS AumentoPresenca,
        @DecrementoPresenca AS DecrementoPresenca;
END
