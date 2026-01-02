CREATE OR ALTER PROCEDURE sp_GetAumentoDashBoard
    @MesAtual INT,
    @MesAnterior INT,
    @Ano INT
AS
BEGIN 
    SET NOCOUNT ON;

    -- Alunos
    DECLARE @AumentoAlunos INT;
    SELECT @AumentoAlunos = 
        ISNULL(COUNT(CASE WHEN MONTH(DataMatricula) = @MesAtual AND YEAR(DataMatricula) = @Ano THEN 1 END), 0) -
        ISNULL(COUNT(CASE WHEN MONTH(DataMatricula) = @MesAnterior AND YEAR(DataMatricula) = @Ano THEN 1 END), 0)
    FROM Alunos
    WHERE Deletado = 0;

    -- Professores
    DECLARE @AumentoProfessores INT;
    SELECT @AumentoProfessores = 
        ISNULL(COUNT(CASE WHEN MONTH(Contratacao) = @MesAtual AND YEAR(Contratacao) = @Ano THEN 1 END), 0) -
        ISNULL(COUNT(CASE WHEN MONTH(Contratacao) = @MesAnterior AND YEAR(Contratacao) = @Ano THEN 1 END), 0)
    FROM Professores
    WHERE Deletado = 0;

    -- Turmas
    DECLARE @AumentoTurmas INT;
    SELECT @AumentoTurmas = 
        ISNULL(COUNT(CASE WHEN MONTH(DataCriacao) = @MesAtual AND YEAR(DataCriacao) = @Ano THEN 1 END), 0) -
        ISNULL(COUNT(CASE WHEN MONTH(DataCriacao) = @MesAnterior AND YEAR(DataCriacao) = @Ano THEN 1 END), 0)
    FROM Turmas
    WHERE Deletado = 0;

    -- Presencas
    DECLARE @AumentoPresencas INT;

    -- Total presencas do mes atual válidas
    DECLARE @PresencasMesAtual INT;
    SELECT @PresencasMesAtual = ISNULL(COUNT(*), 0)
    FROM Presencas
    WHERE (Presente = 1 OR Justificada = 1) AND MONTH(Data) = @MesAtual AND YEAR(Data) = @Ano;

    -- Total presencas do mes anterior que devem ser decrementadas
    DECLARE @DecrementoPresencas INT;
    SELECT @DecrementoPresencas = ISNULL(COUNT(*), 0)
    FROM Presencas
    WHERE (Presente = 0 AND Justificada = 0) AND MONTH(Data) = @MesAnterior AND YEAR(Data) = @Ano;

    SET @AumentoPresencas = @PresencasMesAtual - @DecrementoPresencas;

    -- Retornar resultado
    SELECT 
        @AumentoAlunos AS AumentoAlunos,
        @AumentoProfessores AS AumentoProfessores,
        @AumentoTurmas AS AumentoTurmas,
        @AumentoPresencas AS AumentoPresencas;
END
