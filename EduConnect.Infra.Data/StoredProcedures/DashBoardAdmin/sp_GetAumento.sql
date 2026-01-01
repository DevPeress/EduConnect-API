CREATE OR ALTER PROCEDURE sp_GetAumentoDashBoard
    @MesAtual INT,
    @MesAnterior INT
AS
BEGIN 
    SET NOCOUNT ON;

	-- Alunos
    DECLARE @AumentoAlunos INT;
    SELECT @AumentoAlunos = 
        ISNULL(COUNT(CASE WHEN MONTH(DataMatricula) = @MesAtual THEN 1 END), 0) -
        ISNULL(COUNT(CASE WHEN MONTH(DataMatricula) = @MesAnterior THEN 1 END), 0)
    FROM Alunos
    WHERE Deletado = 0;

    -- Professores
    DECLARE @AumentoProfessores INT;
    SELECT @AumentoProfessores = 
        ISNULL(COUNT(CASE WHEN MONTH(Contratacao) = @MesAtual THEN 1 END), 0) -
        ISNULL(COUNT(CASE WHEN MONTH(Contratacao) = @MesAnterior THEN 1 END), 0)
    FROM Professores
    WHERE Deletado = 0;

    -- Turmas
    DECLARE @AumentoTurmas INT;
    SELECT @AumentoTurmas = 
        ISNULL(COUNT(CASE WHEN MONTH(DataCriacao) = @MesAtual THEN 1 END), 0) -
        ISNULL(COUNT(CASE WHEN MONTH(DataCriacao) = @MesAnterior THEN 1 END), 0)
    FROM Turmas
    WHERE Deletado = 0;

    -- Presencas
    DECLARE @AumentoPresencas INT;

    -- Total presencas do mes atual válidas
    DECLARE @PresencasMesAtual INT;
    SELECT @PresencasMesAtual = ISNULL(COUNT(*), 0)
    FROM Presencas
    WHERE (Presente = 1 OR Justificada = 1) AND MONTH(Data) = @MesAtual;

    -- Total presencas do mes anterior que devem ser decrementadas
    DECLARE @DecrementoPresencas INT;
    SELECT @DecrementoPresencas = ISNULL(COUNT(*), 0)
    FROM Presencas
    WHERE (Presente = 0 AND Justificada = 0) AND MONTH(Data) = @MesAnterior;

    SET @AumentoPresencas = @PresencasMesAtual - @DecrementoPresencas;

    -- Retornar resultado
    SELECT 
        @AumentoAlunos AS AumentoAlunos,
        @AumentoProfessores AS AumentoProfessores,
        @AumentoTurmas AS AumentoTurmas,
        @AumentoPresencas AS AumentoPresencas;
END